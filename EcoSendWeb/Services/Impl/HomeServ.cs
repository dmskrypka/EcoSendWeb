using EcoSendWeb.App_Start;
using EcoSendWeb.Extensions;
using EcoSendWeb.Infrastructure;
using EcoSendWeb.Models.BO.Home;
using EcoSendWeb.Models.DAO;
using EcoSendWeb.Services.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static EcoSendWeb.Infrastructure.Defs;

namespace EcoSendWeb.Services.Impl
{
    public class HomeServ : IHomeServ
    {
        //public AirInfo FindAirInfoById(Guid id)
        //{
        //    using (AirDbEntities db = new AirDbEntities())
        //    {
        //        tblAirInfo tblAi = db.tblAirInfoes.FirstOrDefault(x => x.pk_id == id);
        //        return tblAi is null ? null : MappingProfilesConfig.Mapper.Map<AirInfo>(tblAi);
        //    }
        //}

        //public AirInfo FindAirInfoByPn(string pn)
        //{
        //    using (AirDbEntities db = new AirDbEntities())
        //    {
        //        tblAirInfo tblAi = db.tblAirInfoes.FirstOrDefault(x => x.part_number == pn);
        //        return tblAi is null ? null : MappingProfilesConfig.Mapper.Map<AirInfo>(tblAi);
        //    }
        //}

        //public IEnumerable<AirInfo> FindAirInfosByABC(string[] abc)
        //{
        //    using (AirDbEntities db = new AirDbEntities())
        //    {
        //        StringBuilder sb = new StringBuilder();

        //        foreach(string s in abc)
        //        {
        //            sb.Append($"'{s}',");
        //        }

        //        string strABC = sb.ToString();

        //        string query = @"SELECT [pk_id]
        //                               ,[segment]
        //                               ,[part_number]
        //                               ,[description]
        //                               ,[ata]
        //                               ,[abc]
        //                               ,[mfg_name]
        //                               ,[twlv_months_total_sales]
        //                               ,[twlv_months_total_quotes]
        //                               ,[twlv_months_total_no_bids]
        //                               ,[repare_price]
        //                               ,[suggested_purchase_price]
        //                               ,[mod]
        //                               ,[aircraft]
        //                     FROM [AirDb].[dbo].[tblAirInfo]
        //                     WHERE [abc] in ( " + strABC.Remove(strABC.Length - 1) + " )";

        //        IEnumerable<tblAirInfo> list = db.Database.SqlQuery<tblAirInfo>(query).ToList();

        //        return MappingProfilesConfig.Mapper.Map<IEnumerable<AirInfo>>(list);
        //    }
        //}

        //public IEnumerable<ConditionHistory> GetCompleteLastConditionChanges(Guid airInfoId)
        //{
        //    Array array = Enum.GetValues(typeof(ConditionType));

        //    List<ConditionHistory> conditions = new List<ConditionHistory>(GetLastConditionsChanges(airInfoId));
        //    if (conditions.Count != array.Length)
        //    {
        //        foreach (ConditionType ct in array)
        //        {
        //            if (!conditions.Any(x => x.ConditionType == ct))
        //            {
        //                conditions.Add(new ConditionHistory
        //                {
        //                    ConditionType = ct
        //                });
        //            }
        //        }
        //    }

        //    return conditions.OrderBy(x => x.ConditionType);
        //}


        //public IEnumerable<ConditionHistory> GetLastConditionsChanges(Guid airInfoId)
        //{
        //    List<ConditionHistory> changes = new List<ConditionHistory>();
        //    using (AirDbEntities db = new AirDbEntities())
        //    {
        //        IEnumerable<tblConditionsHistory> conditionChanges = db.tblConditionsHistories.Where(x => x.fk_airinfo_id == airInfoId).OrderByDescending(x => x.updated);
        //        foreach (ConditionType ct in Enum.GetValues(typeof(ConditionType)).Cast<ConditionType>())
        //        {
        //            tblConditionsHistory tch = conditionChanges.FirstOrDefault(x => x.condition_type == (int)ct);
        //            if (tch != null)
        //            {
        //                changes.Add(MappingProfilesConfig.Mapper.Map<ConditionHistory>(tch));
        //            }
        //            else
        //            {
        //                changes.Add(new ConditionHistory
        //                {
        //                    AirInfoId = airInfoId,
        //                    ConditionType = ct
        //                });
        //            }
        //        }
        //    }

        //    return changes;
        //}

        //public IEnumerable<AirInfo> GetAirInfos()
        //{
        //    using (AirDbEntities db = new AirDbEntities())
        //    {
        //        return MappingProfilesConfig.Mapper.Map<IEnumerable<AirInfo>>(db.tblAirInfoes.ToList());
        //    }
        //}

        //public void SaveAirInfo(AirInfo ai, IEnumerable<ConditionHistory> conditions)
        //{
        //    using (AirDbEntities db = new AirDbEntities())
        //    {
        //        tblAirInfo tblAi = db.tblAirInfoes.FirstOrDefault(x => x.part_number == ai.PartNumber);
        //        if (tblAi != null)
        //        {
        //            tblAi.segment = ai.Segment;
        //            tblAi.description = ai.Description;
        //            tblAi.ata = ai.ATA;
        //            tblAi.abc = ai.ABC;
        //            tblAi.mfg_name = ai.MfgName;
        //            tblAi.twlv_months_total_sales = ai.TwlvMonthsTotalSales;
        //            tblAi.twlv_months_total_quotes = ai.TwlvMonthsTotalQuotes;
        //            tblAi.twlv_months_total_no_bids = ai.TwlvMonthsTotalNoBids;
        //            tblAi.repare_price = ai.ReparePrice;
        //            tblAi.suggested_purchase_price = ai.SuggestedPurchasePrice;
        //            tblAi.mod = ai.Mod;
        //            tblAi.aircraft = ai.Aircraft;

        //            ai.Id = tblAi.pk_id;
        //        }
        //        else
        //        {
        //            if (ai.Id == default(Guid))
        //            {
        //                ai.Id = Guid.NewGuid();
        //            }

        //            db.tblAirInfoes.Add(MappingProfilesConfig.Mapper.Map<tblAirInfo>(ai));
        //            db.SaveChanges();
        //        }

        //        if(!db.tblABCs.Any(x=>x.abc_name == ai.ABC))
        //        {
        //            if (db.tblABCs.Count() > 0)
        //            {
        //                int id = db.tblABCs.OrderBy(x => x.pk_id).ToArray().Last().pk_id + 1;
        //                db.tblABCs.Add(new tblABC
        //                {
        //                    pk_id = id,
        //                    abc_name = ai.ABC
        //                });
        //            }
        //            else
        //            {
        //                db.tblABCs.Add(new tblABC
        //                {
        //                    pk_id = 1,
        //                    abc_name = ai.ABC
        //                });
        //            }
        //        }

        //        IEnumerable<tblConditionsHistory> tblCHs = db.tblConditionsHistories.Where(x => x.fk_airinfo_id == ai.Id).OrderByDescending(x => x.updated);
        //        foreach (ConditionHistory ch in conditions)
        //        {
        //            bool update = true;

        //            tblConditionsHistory oldRec = tblCHs.FirstOrDefault(x => x.condition_type == (int)ch.ConditionType);
        //            if (oldRec != null)
        //            {
        //                update = (ch.MinPrice != oldRec.min_price || ch.MaxPrice != oldRec.max_price || ch.Comment != oldRec.comment);
        //            }

        //            if (update)
        //            {
        //                ch.Id = Guid.NewGuid();
        //                ch.AirInfoId = ai.Id;
        //                if (!ch.Updated.HasValue)
        //                {
        //                    ch.Updated = DateTime.Now;
        //                }

        //                db.tblConditionsHistories.Add(MappingProfilesConfig.Mapper.Map<tblConditionsHistory>(ch));
        //            }
        //        }

        //        db.SaveChanges();
        //    }
        //}

        //public void SaveAirInfoRange(IDictionary<AirInfo, List<ConditionHistory>> data)
        //{
        //    IEnumerable<tblAirInfo> tblAis;
        //    IEnumerable<tblConditionsHistory> tblChs;
        //    IEnumerable<tblABC> tblABCs;
        //    using (AirDbEntities db = new AirDbEntities())
        //    {
        //        tblAis = db.tblAirInfoes.ToList();
        //        tblChs = db.tblConditionsHistories.ToList();
        //        tblABCs = db.tblABCs.ToList();
        //    }

        //    List<tblAirInfo> tblNewAis = new List<tblAirInfo>();
        //    List<tblAirInfo> tblUpdateAis = new List<tblAirInfo>();
        //    List<tblABC> tblNewABS = new List<tblABC>();

        //    List<tblConditionsHistory> tblNewChs = new List<tblConditionsHistory>();

        //    foreach (KeyValuePair<AirInfo, List<ConditionHistory>> kvp in data)
        //    {
        //        AirInfo ai = kvp.Key;

        //        tblAirInfo tblAi = tblAis.FirstOrDefault(x => x.part_number == ai.PartNumber);
        //        if (tblAi != null)
        //        {
        //            ai.Id = tblAi.pk_id;
        //            if (!AirInfoCompare(tblAi, ai))
        //            {
        //                tblUpdateAis.Add(MappingProfilesConfig.Mapper.Map<tblAirInfo>(ai));
                        
        //                if(tblAi.abc != ai.ABC && !tblABCs.Any(x => x.abc_name == ai.ABC))
        //                {
        //                    if(tblABCs.Count() > 0)
        //                    {
        //                        tblNewABS.Add(new tblABC
        //                        {
        //                            pk_id = tblABCs.OrderBy(x => x.pk_id).Last().pk_id + 1,
        //                            abc_name = ai.ABC
        //                        });
        //                    }
        //                    else
        //                    {
        //                        tblNewABS.Add(new tblABC
        //                        {
        //                            pk_id = 1,
        //                            abc_name = ai.ABC
        //                        });
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (ai.Id == default(Guid))
        //            {
        //                ai.Id = Guid.NewGuid();
        //            }

        //            tblNewAis.Add(MappingProfilesConfig.Mapper.Map<tblAirInfo>(ai));

        //            if (!tblABCs.Any(x => x.abc_name == ai.ABC))
        //            {
        //                if (tblABCs.Count() > 0)
        //                {
        //                    tblNewABS.Add(new tblABC
        //                    {
        //                        pk_id = tblABCs.OrderBy(x => x.pk_id).Last().pk_id + 1,
        //                        abc_name = ai.ABC
        //                    });
        //                }
        //                else
        //                {
        //                    tblNewABS.Add(new tblABC
        //                    {
        //                        pk_id = 1,
        //                        abc_name = ai.ABC
        //                    });
        //                }
        //            }
        //        }

        //        IEnumerable<tblConditionsHistory> tblExistingChs = tblChs.Where(x => x.fk_airinfo_id == ai.Id).OrderByDescending(x => x.updated);
        //        foreach (ConditionHistory ch in kvp.Value)
        //        {
        //            bool update = true;

        //            tblConditionsHistory oldRec = tblExistingChs.FirstOrDefault(x => x.condition_type == (int)ch.ConditionType);
        //            if (oldRec != null)
        //            {
        //                update = (ch.MinPrice != oldRec.min_price || ch.MaxPrice != oldRec.max_price || ch.Comment != oldRec.comment);
        //            }

        //            if (update)
        //            {
        //                ch.Id = Guid.NewGuid();
        //                ch.AirInfoId = ai.Id;
        //                if (!ch.Updated.HasValue)
        //                {
        //                    ch.Updated = DateTime.Now;
        //                }

        //                tblNewChs.Add(MappingProfilesConfig.Mapper.Map<tblConditionsHistory>(ch));
        //            }
        //        }
        //    }

        //    using (AirDbEntities db = new AirDbEntities())
        //    {
        //        int counter = tblUpdateAis.Count();
        //        foreach (tblAirInfo tblAi in db.tblAirInfoes.ToList())
        //        {
        //            tblAirInfo tblUpdateAi = tblUpdateAis.FirstOrDefault(x => x.pk_id == tblAi.pk_id);
        //            if (tblUpdateAi != null)
        //            {
        //                tblAi.segment = tblUpdateAi.segment;
        //                tblAi.description = tblUpdateAi.description;
        //                tblAi.ata = tblUpdateAi.ata;
        //                tblAi.abc = tblUpdateAi.abc;
        //                tblAi.mfg_name = tblUpdateAi.mfg_name;
        //                tblAi.twlv_months_total_sales = tblUpdateAi.twlv_months_total_sales;
        //                tblAi.twlv_months_total_quotes = tblUpdateAi.twlv_months_total_quotes;
        //                tblAi.twlv_months_total_no_bids = tblUpdateAi.twlv_months_total_no_bids;
        //                tblAi.repare_price = tblUpdateAi.repare_price;
        //                tblAi.suggested_purchase_price = tblUpdateAi.suggested_purchase_price;
        //                tblAi.mod = tblUpdateAi.mod;
        //                tblAi.aircraft = tblUpdateAi.aircraft;

        //                counter--;
        //                if (counter == 0) break;
        //            }
        //        }

        //        db.tblAirInfoes.AddRange(tblNewAis);
        //        db.tblConditionsHistories.AddRange(tblNewChs);
        //        db.tblABCs.AddRange(tblNewABS);

        //        db.SaveChanges();
        //    }
        //}

        //public void SaveCategoryInfo(CategoryInfo ci)
        //{
        //    using (AirDbEntities db = new AirDbEntities())
        //    {
        //        tblCategoryInfo tblCatInfo = db.tblCategoryInfoes.FirstOrDefault(x => x.pk_id == ci.Id);
        //        if (tblCatInfo != null)
        //        {
        //            tblCatInfo.company = ci.Company;
        //            tblCatInfo.condition_type = (int)ci.ConditionType;
        //            tblCatInfo.date = ci.Date;
        //            tblCatInfo.qty = ci.Qty;
        //            tblCatInfo.lt_days = ci.LtDays;
        //            tblCatInfo.unit_price = ci.UnitPrice;
        //            tblCatInfo.serial_number = ci.SerialNumber;
        //            tblCatInfo.comment = ci.Comment;
        //        }
        //        else
        //        {
        //            tblCatInfo = MappingProfilesConfig.Mapper.Map<tblCategoryInfo>(ci);
        //            tblCatInfo.pk_id = Guid.NewGuid();

        //            db.tblCategoryInfoes.Add(tblCatInfo);
        //        }

        //        db.SaveChanges();
        //    }
        //}

        //public bool DeleteCategoryInfo(Guid categoryId, out string errorMsg)
        //{
        //    using (AirDbEntities db = new AirDbEntities())
        //    {
        //        tblCategoryInfo tblCatInfo = db.tblCategoryInfoes.FirstOrDefault(x => x.pk_id == categoryId);
        //        if (tblCatInfo != null)
        //        {
        //            db.tblCategoryInfoes.Remove(tblCatInfo);
        //            errorMsg = "";
        //        }
        //        else
        //        {
        //            errorMsg = "Not found";
        //            return false;
        //        }

        //        db.SaveChanges();
        //    }

        //    return true;
        //}

        //public void DeleteAirInfo(Guid id)
        //{
        //    using (AirDbEntities db = new AirDbEntities())
        //    {
        //        IEnumerable<tblConditionsHistory> tblCHs = db.tblConditionsHistories.Where(x => x.fk_airinfo_id == id);
        //        db.tblConditionsHistories.RemoveRange(tblCHs);

        //        IEnumerable<tblCategoryInfo> tblCatInfos = db.tblCategoryInfoes.Where(x => x.fk_airinfo_id == id);
        //        db.tblCategoryInfoes.RemoveRange(tblCatInfos);

        //        tblAirInfo tblAi = db.tblAirInfoes.FirstOrDefault(x => x.pk_id == id);
        //        if (tblAi != null)
        //        {
        //            db.tblAirInfoes.Remove(tblAi);
        //        }

        //        db.SaveChanges();
        //    }
        //}

        //public IEnumerable<ConditionHistory> GetConditionChanges(Guid airInfoId, ConditionType conditionType)
        //{
        //    using (AirDbEntities db = new AirDbEntities())
        //    {
        //        IEnumerable<tblConditionsHistory> chs = db.tblConditionsHistories.Where(x => x.fk_airinfo_id == airInfoId && x.condition_type == (int)conditionType).OrderByDescending(x => x.updated).Take(50);
        //        return MappingProfilesConfig.Mapper.Map<IEnumerable<ConditionHistory>>(chs);
        //    }
        //}

        //public IEnumerable<string> GetABCs()
        //{
        //    using (AirDbEntities db = new AirDbEntities())
        //    {
        //        return db.tblABCs.OrderBy(x => x.abc_name).Select(x => x.abc_name).ToArray();
        //    }
        //}

        //private bool AirInfoCompare(tblAirInfo tblAir, AirInfo ai)
        //{
        //    if (tblAir.segment != ai.Segment) return false;
        //    if (tblAir.description != ai.Description) return false;
        //    if (tblAir.ata != ai.ATA) return false;
        //    if (tblAir.abc != ai.ABC) return false;
        //    if (tblAir.mfg_name != ai.MfgName) return false;
        //    if (tblAir.twlv_months_total_sales != ai.TwlvMonthsTotalSales) return false;
        //    if (tblAir.twlv_months_total_quotes != ai.TwlvMonthsTotalQuotes) return false;
        //    if (tblAir.twlv_months_total_no_bids != ai.TwlvMonthsTotalNoBids) return false;
        //    if (tblAir.repare_price != ai.ReparePrice) return false;
        //    if (tblAir.suggested_purchase_price != ai.SuggestedPurchasePrice) return false;
        //    if (tblAir.mod != ai.Mod) return false;
        //    if (tblAir.aircraft != ai.Aircraft) return false;

        //    return true;
        //}
    }
}