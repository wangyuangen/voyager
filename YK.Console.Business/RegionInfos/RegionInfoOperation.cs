using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using ToolGood.Words.Pinyin;
using YK.Console.Business.Abstractors;
using YK.Console.Core.Enums;

namespace YK.Console.Business.RegionInfos;

public class RegionInfoOperation(IRepository<RegionInfo> _repo) : IRegionInfoOperation
{
    //同步数据来源
    private static readonly string _dataSource = "http://www.stats.gov.cn/sj/tjbz/tjyqhdmhcxhfdm/2023/index.html";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="regionLevel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task SyncAsync(RegionLevel regionLevel = RegionLevel.City, CancellationToken cancellationToken = default)
    {
        var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
        var provinceDom = await context.OpenAsync(_dataSource);

        var provinces = provinceDom.QuerySelectorAll("table.provincetable tr.provincetr td a");

        if (provinces.IsNullOrEmpty()) throw ResultOutput.Exception("同步国家统计局数据异常，请稍后再试");

        //先删除全部
        await _repo.DeleteAsync(x => true, cancellationToken);

        string url = string.Empty;
        List<RegionInfo> entities;
        RegionInfo province, city, county, town, village;
        IDocument cityDom, countyDom, townDom, villageDom;
        IHtmlCollection<IElement> cities, counties, towns, villages;
        IHtmlAnchorElement citiItem, countyItem, townItem;
        foreach (IHtmlAnchorElement item in provinces)
        {
            entities = new List<RegionInfo>();

            url = item.Href.Substring(item.Href.LastIndexOf('/') + 1);
            //省份
            province = new RegionInfo
            {
                Name = item.TextContent,
                Level = RegionLevel.Province,
                Code = url.Replace(".html", string.Empty),
                Url = url,
                Pinyin = WordsHelper.GetPinyin(item.TextContent),
                PinyinFirst = WordsHelper.GetFirstPinyin(item.TextContent),
            };
            entities.Add(province);

            if (regionLevel != RegionLevel.Province && !string.IsNullOrWhiteSpace(item.Href))
            {
                //市
                cityDom = await context.OpenAsync(item.Href);
                cities = cityDom.QuerySelectorAll("table.citytable tr.citytr td a");
                for (int c = 0; c < cities.Length; c += 2)
                {
                    citiItem = (IHtmlAnchorElement)cities[c + 1];
                    url = citiItem.Href.Substring(citiItem.Href.LastIndexOf('/') + 1);
                    city = new RegionInfo
                    {
                        ParentCode = province.Code,
                        Name = citiItem.TextContent,
                        Level = RegionLevel.City,
                        Code = cities[c].TextContent,
                        Url = url,
                        Pinyin = WordsHelper.GetPinyin(citiItem.TextContent),
                        PinyinFirst = WordsHelper.GetFirstPinyin(citiItem.TextContent),
                    };
                    entities.Add(city);

                    if (regionLevel != RegionLevel.City && !string.IsNullOrWhiteSpace(citiItem.Href))
                    {
                        //区县
                        countyDom = await context.OpenAsync(citiItem.Href);
                        counties = countyDom.QuerySelectorAll("table.countytable tr.countytr td a");
                        for (int ct = 0; ct < counties.Length; ct += 2)
                        {
                            countyItem = (IHtmlAnchorElement)counties[ct + 1];
                            url = countyItem.Href.Substring(countyItem.Href.LastIndexOf('/') + 1);
                            county = new RegionInfo
                            {
                                ParentCode = city.Code,
                                Name = countyItem.TextContent,
                                Level = RegionLevel.County,
                                Code = counties[ct].TextContent,
                                Url = url,
                                Pinyin = WordsHelper.GetPinyin(countyItem.TextContent),
                                PinyinFirst = WordsHelper.GetFirstPinyin(countyItem.TextContent),
                            };
                            entities.Add(county);

                            if (regionLevel != RegionLevel.County && !string.IsNullOrWhiteSpace(countyItem.Href))
                            {
                                //乡镇街道
                                townDom = await context.OpenAsync(countyItem.Href);
                                towns = townDom.QuerySelectorAll("table.towntable tr.towntr td a");
                                for (int t = 0; t < towns.Length; t += 2)
                                {
                                    townItem = (IHtmlAnchorElement)towns[t + 1];
                                    url = townItem.Href.Substring(townItem.Href.LastIndexOf('/') + 1);
                                    town = new RegionInfo
                                    {
                                        ParentCode = county.Code,
                                        Name = townItem.TextContent,
                                        Level = RegionLevel.Town,
                                        Code = towns[t].TextContent,
                                        Url = url,
                                        Pinyin = WordsHelper.GetPinyin(townItem.TextContent),
                                        PinyinFirst = WordsHelper.GetFirstPinyin(townItem.TextContent),
                                    };
                                    entities.Add(town);

                                    if (regionLevel != RegionLevel.Town && !string.IsNullOrWhiteSpace(townItem.Href))
                                    {
                                        //村
                                        villageDom = await context.OpenAsync(townItem.Href);
                                        villages = villageDom.QuerySelectorAll("table.villagetable tr.villagetr td");
                                        for (int v = 0; v < villages.Length; v += 3)
                                        {
                                            village = new RegionInfo
                                            {
                                                ParentCode = town.Code,
                                                Name = villages[v + 2].TextContent,
                                                Level = RegionLevel.Vilage,
                                                Code = villages[v].TextContent,
                                                Url = string.Empty,
                                                Pinyin = WordsHelper.GetPinyin(villages[v + 2].TextContent),
                                                PinyinFirst = WordsHelper.GetFirstPinyin(villages[v + 2].TextContent),
                                            };
                                            entities.Add(village);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            await _repo.AddRangeAsync(entities, cancellationToken);
        }
    }
}
