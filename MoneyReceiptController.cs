using BusinessLayer;
using BusinessLayer.DAL;
using BusinessLayer.Entity;
using eARC.CustomHelpers;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Net.Mail;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.parser;
using System.Text;
using iTextSharp.tool.xml.html;
using System.Text.RegularExpressions;
using System.Globalization;
using eWayGST;
using BusinessLayer.DAL.iFMS;
using BusinessLayer.Entity.iFMS;

namespace eARC.Controllers
{
    public class MoneyReceiptController : BaseController
    {
        #region MR Sheet View
        public ActionResult MRSheet()
        {

            ViewBag.Header = "Money Receipt Sheet";
            MR_SHEET mr = new MR_SHEET();
            List<MST_BRANCH> Branch_list = new DAL_Branch().GET_BRANCH_LIST_FOR_CN_BOOKING(emp.UserTypeShort, emp.USER_ID, emp.USER_BR_SCOPE, emp.USER_BR_TYPE, Convert.ToInt32(emp.USER_LOC_ID));
            mr.BR_LIST = new SelectList(Branch_list, "BR_ID", "BR_NAME", (Branch_list.Count() == 1) ? Branch_list.First().BR_ID : emp.USER_LOC_ID);
            return View(mr);
        }

        [Authorize]
        public ActionResult MRSheet_View(string mrId)
        {
            MR_SHEET mrDetails = new MR_SHEET();
            try
            {
                mrDetails = new DAL_MR().MR_VIEW(Convert.ToDecimal(mrId), "");
                if (ConfigurationManager.AppSettings["Sitename"] != null)
                {
                    mrDetails.SITE_NAME = Convert.ToString(ConfigurationManager.AppSettings["Sitename"]);
                }
                if (ConfigurationManager.AppSettings["SiteISO"] != null)
                {
                    mrDetails.SITE_ISO = Convert.ToString(ConfigurationManager.AppSettings["SiteISO"]);
                }
                if (ConfigurationManager.AppSettings["Regaddress"] != null)
                {
                    mrDetails.SITE_REGISTERED_ADDRESS = Convert.ToString(ConfigurationManager.AppSettings["Regaddress"]);
                    string[] ArrayRegAddr = mrDetails.SITE_REGISTERED_ADDRESS.Split(',');
                    string regAddr = ArrayRegAddr[0] + ", " + ArrayRegAddr[1] + ", " + ArrayRegAddr[2] + ", " + ArrayRegAddr[3] + ", " + ArrayRegAddr[4];


                    mrDetails.SITE_REGISTERED_ADDRESS = regAddr;
                    mrDetails.SITE_REGISTERED_PHONE_NO_EMAILID = ArrayRegAddr[5];
                    //regPhnoEmail = ArrayRegAddr[5];
                }
                if (ConfigurationManager.AppSettings["Regdphone"] != null)
                {
                    mrDetails.SITE_REGISTERED_PHONE_NO = Convert.ToString(ConfigurationManager.AppSettings["Regdphone"]);
                }
                if (ConfigurationManager.AppSettings["Regdemail"] != null)
                {
                    mrDetails.SITE_REGISTERED_EMAILID = Convert.ToString(ConfigurationManager.AppSettings["Regdemail"]);
                }
                if (ConfigurationManager.AppSettings["HOaddress"] != null)
                {
                    mrDetails.SITE_HEADOFFICE_ADDRESS = Convert.ToString(ConfigurationManager.AppSettings["HOaddress"]);
                    string[] ArrayHoAddr = mrDetails.SITE_HEADOFFICE_ADDRESS.Split(',');
                    string hoAddr = ArrayHoAddr[0] + ", " + ArrayHoAddr[1] + ", " + ArrayHoAddr[2] + ", " + ArrayHoAddr[3] + ", " + ArrayHoAddr[4];

                    mrDetails.SITE_HEADOFFICE_ADDRESS = hoAddr;

                }
                if (ConfigurationManager.AppSettings["HOphone"] != null)
                {
                    mrDetails.SITE_HEADOFFICE_PHONE_NO = Convert.ToString(ConfigurationManager.AppSettings["HOphone"]);
                }
                if (ConfigurationManager.AppSettings["HOemail"] != null)
                {
                    mrDetails.SITE_HEADOFFICE_EMAILID = Convert.ToString(ConfigurationManager.AppSettings["HOemail"]);
                }
                return PartialView("MRSheet_View", mrDetails);
            }
            catch (Exception)
            {
                return Content("Try again!!");
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GET_MR_INFO(string mrNo)
        {
            MR_SHEET mr = new DAL_MR().GET_MR_INFO(mrNo);
            return Json(mr, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region JSON
        // Added by Pramesh kumar Vishwakarma, Date:10-09-2020
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SELECT_CBS_PERIOD(int brId)
        {
            int pd = new DAL_MR().SELECT_CBS_PERIOD(brId);
            return Json(pd, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SELECT_LAST_CBS_DATE(int brId)
        {
            object lstcsbDate = new DAL_MR().SELECT_LAST_CBS_DATE(brId);
            return Json(lstcsbDate, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GET_CBS_STN(int brId)
        {

            CBS_BR_DTLS cbsstn = new DAL_MR().GET_CBS_STN(brId);
            return Json(cbsstn, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SELECT_CNS_LIST_BY_BOOKING_BR_ID(int brId, string DATE, int? partyId, string searchtext)
        {
            List<ddlClass> l = new DAL_MR().SELECT_CNS_LIST_BY_BOOKING_BR_ID(brId, DATE, partyId, searchtext);
            return Json(l, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SELECT_CN_DETAILS_BY_CN_ID(decimal cnId, decimal paId, int mrBrId, string mrdate)
        {
            CN_OR_BILL_DTL dtl = new DAL_MR().SELECT_CN_DETAILS_BY_CN_ID(cnId, paId, mrBrId, mrdate);
            return Json(dtl, JsonRequestBehavior.AllowGet);
        }

        // Added: Sunil Kumar Singh Date: 21/12/2020
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SELECT_CN_ID(int brId, string cnno)
        {
            CN_BILL_IDS dtl = new DAL_MR().SELECT_CN_ID(brId, cnno);
            return Json(dtl, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SELECT_BILL_ID(int brId, string billno)
        {
            CN_BILL_IDS dtl = new DAL_MR().SELECT_BILL_ID(brId, billno);
            return Json(dtl, JsonRequestBehavior.AllowGet);
        }
        // Added: Sunil Kumar Singh Date: 21/12/2020

        //23-07-2021
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SELECT_POS_ALLOW(int brId)
        {
            int allow = new DAL_MR().SELECT_POS_ALLOW(brId);
            return Json(allow, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SELECT_BILL_LIST_BY_BR_ID(int brId, string DATE, int? partyId, string searchtext)
        {
            List<ddlClass> l = new DAL_MR().SELECT_BILL_LIST_BY_BR_ID(brId, DATE, partyId, searchtext);
            return Json(l, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SELECT_BILL_DETAILS_BY_BILL_ID(decimal billId, string mrdate)
        {
            CN_OR_BILL_DTL dtl = new DAL_MR().SELECT_BILL_DETAILS_BY_BILL_ID(billId, mrdate);
            return Json(dtl, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetLastEntryRecord(int brId)
        {
            MR_Receipt MR = new DAL_MR().GET_LAST_ENTRY_RECORD(brId);
            return Json(MR, JsonRequestBehavior.AllowGet);
        }

        //  Get MR Branch GST No - Sunil Kumar Singh Date:12/01/2022
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetMRStnGstRecord(int brId)
        {
            MR_BR_GST GST_NO = new DAL_MR().GetMRStnGstRecord(brId);
            return Json(GST_NO, JsonRequestBehavior.AllowGet);
        }
        // End

        // Check MR no exist or not
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CHECK_EXISTING_MR_NO(string mrNo, int mrbrid)
        {
            MR_CHECKING br = new DAL_MR().CHECK_EXISTING_MR_NO(mrNo, mrbrid);
            return Json(br, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SELECT_MR_EMD_LIST(int brId, decimal paId, string cbsdate)
        {
            List<MR_EMD_DATA> l = new DAL_MR().SELECT_MR_EMD_LIST(brId, paId, cbsdate);
            return Json(l, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SELECT_MR_CREDIT_ADVICE_LIST(decimal? paId)
        {
            List<ddlClass> l = new DAL_MR().SELECT_MR_CREDIT_ADVICE_LIST(paId);
            return Json(l, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SELECT_MR_CREDIT_ADVICE_DTLS(int craId)
        {
            MR_CREDIT_ADVICE_DTLS cra = new DAL_MR().SELECT_MR_CREDIT_ADVICE_DTLS(craId);
            return Json(cra, JsonRequestBehavior.AllowGet);
        }

        // Added By : Sunil Kumar Singh date: 18/11/2020
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SELECT_LAST_MR_CBS_DATE(int brId)
        {
            object lmrcbsdt = new DAL_MR().SELECT_LAST_MR_CBS_DATE(brId);
            return Json(lmrcbsdt, JsonRequestBehavior.AllowGet);
        }
        // Added By: Sunil Kumar Singh date: 18/11/2020


        // Added By : Sunil Kumar Singh date: 17/11/2020

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPartyGSTValidate(string paid, string pGST)
        {
            string errmsg = "";

            // ARC GST NO 24 AACCA4861C 2Z4
            string arcPAN = "AACCA4861C";
            bool res = pGST.Contains(arcPAN);

            if (res)
            {
                pGST = "";
            }

            Party_Gst pgst = new DAL_Consignment().SELECT_PARTY_GST_DTLS(paid, pGST);

            if (pgst.PA_GST_VAL_DAYS >= 1 && pGST != "" && pgst.PA_GST_VALID == 1)
            {
                // Get GST Data from NIC Server
                eWaybillServices _eWaybillServices = new eWaybillServices();
                /*
                // For Authentication
                AuthResponse objresp;
                byte[] _aeskey;
                _eWaybillServices.GetAuthToken(out objresp, out _aeskey, out errmsg);
                */

                // For GSTIN Details
                // 29AACCA4861C2ZU ARC Karnatka GST
                Gstin_Info gstinObj;

                string six_char = string.Empty;
                //_eWaybillServices.GetGSTINDetails(objresp, pGST, _aeskey, out gstinObj, out errmsg);
                //_eWaybillServices.GetGSTINDetails(pGST, out gstinObj, out errmsg);

                //Added by Ashok Date : 26-09-2022
                string connStr = new DAL_Common().GetConnectionString();
                _eWaybillServices.GetGSTINDetails(pGST, out gstinObj, out errmsg, connStr);

                if (errmsg.Trim() == "")
                {
                    if (gstinObj.gstin != null)
                    {
                        string GSTSTATUS = Convert.ToString(gstinObj.status) == "ACT" ? "Active" : "Cancelled";
                        pgst.PA_TRADE_NAME = Convert.ToString(gstinObj.tradeName);
                        pgst.P_LEGAL_NAME = Convert.ToString(gstinObj.legalName);
                        pgst.PA_GST_STATUS = GSTSTATUS;

                        if (Convert.ToString(pGST).Length > 6)
                        {
                            six_char = Convert.ToString(pGST).Substring(5, 1);
                        }

                        if (six_char != "P")
                        {
                            List<MST_EXCEPTION_BYPASS> excList = new DAL_Party().SELECT_EXCEPTION_BYPASS();

                            string partyName = pgst.P_NAME_ONLY.ToUpper().Trim();
                            string tradeName = (pgst.PA_TRADE_NAME ?? string.Empty).ToUpper().Trim();
                            string legalName = (pgst.P_LEGAL_NAME ?? string.Empty).ToUpper().Trim();

                            System.Text.StringBuilder _partyName = new System.Text.StringBuilder().Append(pgst.P_NAME_ONLY.ToUpper().Trim());
                            System.Text.StringBuilder _tradeName = new System.Text.StringBuilder().Append((pgst.PA_TRADE_NAME ?? string.Empty).ToUpper().Trim());
                            System.Text.StringBuilder _legalName = new System.Text.StringBuilder().Append((pgst.P_LEGAL_NAME ?? string.Empty).ToUpper().Trim());

                            foreach (MST_EXCEPTION_BYPASS exc in excList)
                            {
                                _partyName = _partyName.Replace(exc.BYPASS_CHAR_A, exc.BYPASS_EQUI_CHAR);
                                _tradeName = _tradeName.Replace(exc.BYPASS_CHAR_A, exc.BYPASS_EQUI_CHAR);
                                _legalName = _legalName.Replace(exc.BYPASS_CHAR_A, exc.BYPASS_EQUI_CHAR);
                            }

                            if ((_tradeName.Equals(_partyName) || _legalName.Equals(_partyName))) { }
                            else if ((tradeName == partyName || legalName == partyName)) { }
                            else
                            {
                                pgst.PA_GST_STATUS = "GSTIN Unmatched";
                            }
                        }
                    }

                    else
                    {
                        pgst.PA_GST_STATUS = "Invalid";
                    }
                }
                else
                {
                    pgst.PA_GST_STATUS = "GSTIN Server not working";
                }
            }
            else
            {
                if (pGST != "")
                {
                    if (pgst.PA_GST_VALID == 1)
                    {
                        pgst.PA_GST_STATUS = "Active";
                    }
                    else
                    {
                        pgst.PA_GST_STATUS = "GSTIN belongs to other party";
                    }
                }
                else
                {
                    pgst.PA_GST_STATUS = "Invalid";
                }
            }
            return Json(pgst, JsonRequestBehavior.AllowGet);
        }


        // Added By: Sunil Kumar Singh date: 17/11/2020

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SELECT_BILL_CNS_REF_DTL_FOR_MR_BFD(decimal billId)
        {
            List<MR_CNS_BFD> l = new DAL_MR().SELECT_BILL_CNS_REF_DTL_FOR_MR_BFD(billId);
            return Json(l, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region MR Preparation
        public ActionResult MoneyRealisationReceipt()
        {
            ViewBag.Header = "Money Realisation Receipt";
            MR_Receipt mr = new MR_Receipt();
            try
            {
                mr.MR_DATE = DateTime.Now;
                mr.MR_DATE1 = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                // Bind Accounting Branch list  
                mr.USER_LOC_ID = emp.USER_LOC_ID;
                //List<MST_BRANCH> br_acc = new DAL_Branch().GET_ACCOUNTING_BRANCH_LIST_BY_SCOPE(Convert.ToString(emp.UserTypeShort), Convert.ToDecimal(emp.USER_ID), Convert.ToString(emp.USER_BR_SCOPE), Convert.ToString(emp.USER_BR_TYPE), Convert.ToInt32(emp.USER_LOC_ID));
                //mr.CBS_BR_LIST = new SelectList(br_acc, "BR_ID", "BR_NAME", (br_acc.Count() == 1) ? br_acc.First().BR_ID : emp.USER_LOC_ID);

                List<MST_BRANCH> bookingList = new DAL_Branch().GET_BRANCH_LIST_FOR_CN_BOOKING(emp.UserTypeShort, emp.USER_ID, emp.USER_BR_SCOPE, emp.USER_BR_TYPE, emp.USER_LOC_ID ?? 0);

                mr.MR_TYPE_LIST = new SelectList(new DAL_ddList().GetDropdownList_Dtl("MR_TYPE"), "ddlValue", "ddlText");

                //cn/billing Details
                List<MST_BRANCH> branchList = new DAL_Branch().GET_BRANCH_LIST();
                mr.BILL_CN_BOOKING_BR_LIST = new SelectList(branchList, "BR_ID", "BR_NAME");

                mr.MR_IBT_STN_LIST = new SelectList(branchList, "BR_ID", "BR_NAME");

                mr.EMP_LIST = new SelectList(new DAL_ddList().GET_EMPLOYEE_LIST(emp.USER_LOC_ID ?? 0, 0), "ddlValue", "ddlText", emp.USER_ID);

                mr.BANK_LIST = new SelectList(new DAL_ddList().GetDropdownList_Dtl("BANK"), "ddlValue", "ddlText");
                //For NR Code
                mr.BFD_REASON_LIST = new SelectList(new DAL_ddList().GetDropdownList_Dtl("BFD_REASON"), "ddlValue", "ddlText");

                if (TempData["IS_CBS_INFO_SET"] != null)
                {
                    mr.IS_CBS_INFO_SET = Convert.ToInt32(TempData["IS_CBS_INFO_SET"]);
                    if (Convert.ToInt32(TempData["IS_CBS_INFO_SET"]) == 1)
                    {
                        mr.FROM_CBS_DATE1 = Convert.ToString(TempData["FROM_CBS_DATE1"]);
                        mr.TO_CBS_DATE1 = Convert.ToString(TempData["TO_CBS_DATE1"]);
                        mr.CBS_BR_CODE = Convert.ToString(TempData["CBS_BR_CODE"]);
                        mr.CBS_BR_NAME = Convert.ToString(TempData["CBS_BR_NAME"]);
                        mr.MR_CBS_BR_ID = Convert.ToInt32(TempData["MR_CBS_BR_ID"]);

                        mr.CBS_BR_LIST = new SelectList(bookingList, "BR_ID", "BR_NAME", (bookingList.Count() == 1) ? bookingList.First().BR_ID : mr.MR_CBS_BR_ID);
                        mr.MR_STN_LIST = new SelectList(bookingList, "BR_ID", "BR_NAME", (bookingList.Count() == 1) ? bookingList.First().BR_ID : mr.MR_CBS_BR_ID);
                    }
                }
                else
                {
                    mr.CBS_BR_LIST = new SelectList(bookingList, "BR_ID", "BR_NAME", (bookingList.Count() == 1) ? bookingList.First().BR_ID : emp.USER_LOC_ID);
                    mr.MR_STN_LIST = new SelectList(bookingList, "BR_ID", "BR_NAME", (bookingList.Count() == 1) ? bookingList.First().BR_ID : emp.USER_LOC_ID);
                }

            }
            catch (Exception)
            {
                Danger(string.Format("<b>Exception occured.</b>"), true);
            }
            return View(mr);
        }

        [HttpPost]
        [SubmitButtonSelector(Name = "Save")]
        [ActionName("MoneyRealisationReceipt")]
        public ActionResult MoneyRealisationReceipt_Save(MR_Receipt mr)
        {
            ViewBag.Header = "Money Realisation Receipt";

            if (mr.REC_PAY_MODE_POS != true)
            {
                ModelState["CHQ_RTGS_DD_NO"] = new ModelState();
                ModelState["CHQ_RTGS_DD_DATE"] = new ModelState();
                ModelState["CHQ_RTGS_DD_DATE1"] = new ModelState();
                ModelState["CHQ_RTGS_DD_BANK"] = new ModelState();
                ModelState["CHQ_RTGS_DD_AMT"] = new ModelState();
            }

            if (mr.REC_PAY_MODE_POS == true)
            {
                ModelState["CHQ_RTGS_DD_NO"] = new ModelState();
                ModelState["CHQ_RTGS_DD_DATE"] = new ModelState();
                ModelState["CHQ_RTGS_DD_DATE1"] = new ModelState();
                ModelState["CHQ_RTGS_DD_AMT"] = new ModelState();

                ModelState["POS_TRAN_NO"] = new ModelState();
                ModelState["CHQ_RTGS_DD_BANK"] = new ModelState();
            }

            if (mr.REC_PAY_MODE_POS != true)
            {
                ModelState["POS_TRAN_NO"] = new ModelState();
                ModelState["POS_AMT"] = new ModelState();
            }

            if (mr.REC_PAY_MODE_CASH != true)
            {
                ModelState["CASH_AMT"] = new ModelState();
            }

            if (mr.IBT_TYPE_ID != true)
            {
                ModelState["MRIBT_CRA_NO"] = new ModelState();
            }
            if (mr.MR_DATE1 != null)
            {
                if (mr.MR_DATE1 != "")
                {
                    ModelState["MR_DATE"] = new ModelState();
                    DateTime dt = DateTime.ParseExact(mr.MR_DATE1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    mr.MR_DATE = dt;
                }
            }

            if (mr.CHQ_RTGS_DD_DATE1 != null)
            {
                if (mr.CHQ_RTGS_DD_DATE1 != "")
                {
                    ModelState["CHQ_RTGS_DD_DATE"] = new ModelState();
                    DateTime dt = DateTime.ParseExact(mr.CHQ_RTGS_DD_DATE1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    mr.CHQ_RTGS_DD_DATE = dt;
                }
            }

            //17-11-2020
            if (!(mr.MR_TYPE_CODE == "10" || mr.MR_TYPE_CODE == "12" || mr.MR_TYPE_CODE == "13" || mr.MR_TYPE_CODE == "14" || mr.MR_TYPE_CODE == "16" || mr.MR_TYPE_CODE == "17" || mr.MR_TYPE_CODE == "18" || mr.MR_TYPE_CODE == "30" || mr.MR_TYPE_CODE == "99"))
            {
                if (mr.BILL_CNS_DTL_LIST != null)
                {
                    if (mr.BILL_CNS_DTL_LIST.Count() == 0)
                    {
                        ModelState.AddModelError("MR_NET_RECEIVED", "Please enter CN/Bill Details.");
                    }
                }
                else if (Convert.ToDecimal(mr.MR_NET_RECEIVED ?? 0) <= 0)
                {
                    ModelState.AddModelError("MR_NET_RECEIVED", "Net received amount must be greater than zero.");
                }
            }

            // 24/08/2022 
            if (Convert.ToDecimal(mr.MR_NET_RECEIVED ?? 0) > 0)
            {
                if (Convert.ToDecimal(mr.MR_TOTAL_AMT ?? 0) > 0)
                {
                    if (mr.MR_NET_RECEIVED != mr.MR_TOTAL_AMT)
                    {
                        ModelState.AddModelError("MR_NET_RECEIVED", "Net received amount must be equal to mr amount.");
                    }
                    else if (mr.BILL_CNS_DTL_LIST != null)
                    {
                        if (mr.BILL_CNS_DTL_LIST.Sum(s => (s.NET_RECD_AMT ?? 0)) != mr.MR_TOTAL_AMT)
                        {
                            ModelState.AddModelError("MR_NET_RECEIVED", "Sum of NET RECD in CN/Bill Details List must be equal to mr amount.");
                        }
                    }
                }
            }

            if (mr.MR_TYPE_CODE == "99")
            {
                ModelState["MR_P_NAME"] = new ModelState();
            }

            // 04/11/2022
            if (mr.MR_TYPE_CODE == "01" || mr.MR_TYPE_CODE == "03" || mr.MR_TYPE_CODE == "11" || mr.MR_TYPE_CODE == "15" || mr.MR_TYPE_CODE == "20" || mr.MR_TYPE_CODE == "21" || mr.MR_TYPE_CODE == "22" || mr.MR_TYPE_CODE == "95") {
                if (mr.MR_DOC_TYPE != 1)
                {
                    ModelState.AddModelError("MR_DOC_TYPE", "Some issue in document type selection.");
                }
            } 

            if (mr.MR_TYPE_CODE == "02")
            {
                if (mr.MR_DOC_TYPE != 2)
                {
                    ModelState.AddModelError("MR_DOC_TYPE", "Some issue in document type selection.");
                }
            }

            if (mr.MR_TYPE_CODE == "10" || mr.MR_TYPE_CODE == "12" || mr.MR_TYPE_CODE == "13" || mr.MR_TYPE_CODE == "14" || mr.MR_TYPE_CODE == "16" || mr.MR_TYPE_CODE == "17" || mr.MR_TYPE_CODE == "18" || mr.MR_TYPE_CODE == "30" || mr.MR_TYPE_CODE == "99") {
                if (mr.MR_DOC_TYPE != 3)
                {
                    ModelState.AddModelError("MR_DOC_TYPE", "Some issue in document type selection.");
                }
            }

            // End 04/11/2022

            //Added By : Pramesh Kumar Vishwakarma,  08-09-2022
            string tocbsDate1 = "";
            if ((mr.TO_CBS_DATE1 ?? "") != "")
            {
                tocbsDate1 = mr.TO_CBS_DATE1;
            }
            else
            {
                tocbsDate1 = mr.MR_DATE1;
            }
            if ((tocbsDate1 ?? "") != "")
            {
                string errMsg = new DAL_Common().CHECK_CBS_SRP_CLOSSED(mr.MR_BR_ID ?? 0, tocbsDate1);
                if (errMsg != "")
                {
                    ModelState.AddModelError("CBS_ISSUE", errMsg);
                }
            }

            //End

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                try
                {
                    mr.MR_SUFFIX = ".";
                    mr.MR_USER_TYPE = emp.UserTypeShort;
                    mr.MR_ADD_BY = emp.USER_ID;
                    mr.MRIBT_AMT = mr.MR_TOTAL_AMT;
                    mr.MR_P_GSTNO = mr.GST_NO;

                    mr.MR_CBS_BR_ID = mr.MR_BR_ID;
                    mr.MR_CBS_DATE = mr.MR_DATE;
                    mr.CHQ_COLL_DATE = mr.MR_DATE;

                    mr.CHQ_COLL_BY = Convert.ToInt32(emp.USER_ID);

                    if (mr.REC_PAY_MODE_CASH != null)
                    {
                        if (mr.REC_PAY_MODE_CASH == true)
                        {
                            mr.MR_PAY_MODE = 1; // 1 - cash
                        }
                        else
                        {
                            mr.MR_PAY_MODE = 2; // 2 - bank
                        }
                    }

                    if (mr.REC_PAY_MODE_CASH == null)
                    {
                        mr.MR_PAY_MODE = 2; // 2 - bank
                    }

                    if (mr.REC_PAY_MODE_CASH == true && (mr.REC_PAY_MODE_POS == true || mr.REC_PAY_MODE_CHQ == true))
                    {
                        mr.MR_PAY_MODE = 3; // 3 - both
                    }

                    if (mr.TO_CBS_DATE1 != null)
                    {
                        DateTime dt = DateTime.ParseExact(mr.TO_CBS_DATE1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        mr.MR_CBS_DATE = dt;
                    }

                    ModelState["FROM_CBS_DATE"] = new ModelState();
                    ModelState["FROM_CBS_DATE1"] = new ModelState();
                    ModelState["CBS_OPN_DATE"] = new ModelState();
                    ModelState["CBS_OPN_DATE1"] = new ModelState();

                    if (mr.FROM_CBS_DATE1 != null)
                    {
                        DateTime dt = DateTime.ParseExact(mr.FROM_CBS_DATE1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        mr.FROM_CBS_DATE = dt;
                    }

                    if (mr.CBS_OPN_DATE1 != null)
                    {
                        DateTime dt = DateTime.ParseExact(mr.CBS_OPN_DATE1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        mr.CBS_OPN_DATE = dt;
                    }
                    else
                    {
                        mr.CBS_OPN_DATE = mr.MR_CBS_DATE;
                    }

                    mr.MR_ADD_BY_CODE = emp.USER_CODE;

                    string err = "";

                    if (mr.MR_CBS_DATE == null)
                    {
                        err = "CBS date issue.";
                    }

                    if (mr.MR_CBS_BR_ID == null && err == "")
                    {
                        err = "CBS branch issue.";
                    }

                    if (err != "")
                    {
                        ModelState.AddModelError("CBS_ISSUE", err);
                    } 

                    string result = "";
                    
                    if (err == "")
                    {
                        result = new DAL_MR().INSERT_MR(mr);
                    } 

                    if (result == "")
                    {
                        //Success(string.Format("<b>MR Generated Successfully...</b>"), true);
                        TempData["IS_CBS_INFO_SET"] = mr.IS_CBS_INFO_SET;
                        TempData["FROM_CBS_DATE1"] = mr.FROM_CBS_DATE1;
                        TempData["TO_CBS_DATE1"] = mr.TO_CBS_DATE1;
                        TempData["CBS_BR_CODE"] = mr.CBS_BR_CODE;
                        TempData["CBS_BR_NAME"] = mr.CBS_BR_NAME;
                        TempData["MR_CBS_BR_ID"] = mr.MR_CBS_BR_ID;

                        //return RedirectToAction("MoneyRealisationReceipt", "MoneyReceipt");

                        MR_Result mrResult = new MR_Result();
                        mrResult.MR_NO = mr.MR_NO;
                        mrResult.MR_ID = mr.MR_ID;
                        mrResult.MR_DATE = mr.MR_DATE;
                        mrResult.MR_MANUAL_NO = Convert.ToString(mr.MR_MANUAL_NO).ToUpper();
                        mrResult.MR_DATE1 = mr.MR_DATE1;

                        mrResult.MR_POS_TRAS_NO = mr.POS_TRAN_NO;

                        if (mr.MR_TYPE == 1 && mr.DPR_NO_LIST.Count() > 0) // mr type delivery
                        {
                            mrResult.DPR_NO_LIST = mr.DPR_NO_LIST;
                        }
                        else
                        {
                            mrResult.DPR_NO_LIST = null;
                        }

                        mrResult.MRIBT_CRA_NO = mr.MRIBT_CRA_NO;

                        // Added By  - Sunil Kumar Singh Date: 25/11/2020
                        if (mr.MRIBT_CRA_NO != "" && mr.MRIBT_CRA_NO != null)
                        {
                            DataTable mailRecDt = new DAL_Master().GET_AUTOMAIL_RECEIPENT_RECORD(Convert.ToInt32(mr.MR_BR_ID), 6);

                            if (mailRecDt.Rows.Count > 0)
                            {
                                string emailId = Convert.ToString(mailRecDt.Rows[0]["MAIL_TO"]);
                                string empid = Convert.ToString(mailRecDt.Rows[0]["EMP_ID"]);

                                // It a Temporary Setting

                                if (String.IsNullOrEmpty(emailId))
                                {
                                    emailId = ConfigurationManager.AppSettings["DefaultEmailId"].ToString();
                                    empid = ConfigurationManager.AppSettings["DefaultEmpId"].ToString();
                                }

                                if (String.IsNullOrEmpty(emailId))
                                {

                                }
                                else
                                {
                                    CommonFunction cf = new CommonFunction();

                                    emailId = "sunil.singh@calyxterminal.com,devendra.kumar@calyxterminal.com";
                                    empid = "1";

                                    string _cc = "";
                                    string _bcc = "";
                                    string _subject = "";

                                    _subject = "Advice Intimation - Advice No " + Convert.ToString(mr.MRIBT_CRA_NO).ToUpper() + " from " + Convert.ToString(mr.MR_BR_NAME).ToUpper() + " to " + Convert.ToString(mr.MR_IBT_STN_NAME).ToUpper() + " on " + Convert.ToString(mr.MR_DATE1);

                                    string _emailBody = string.Empty;
                                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Email_Templates/IBTAdvice_Intimation.html")))
                                    {
                                        _emailBody = reader.ReadToEnd();
                                    }
                                    string paymode = "";
                                    if (mr.REC_PAY_MODE_CASH == true)
                                    {
                                        paymode = "CASH";
                                    }

                                    if (mr.REC_PAY_MODE_CHQ == true)
                                    {
                                        paymode = "CHEQUE";
                                    }

                                    if (mr.REC_PAY_MODE_CHQ == true && mr.REC_PAY_MODE_CASH == true)
                                    {
                                        paymode = "CASH & CHEQUE";
                                    }

                                    if (mr.REC_PAY_MODE_POS == true)
                                    {
                                        paymode = "POS";
                                    }

                                    _emailBody = _emailBody.Replace("[SUBJECT]", _subject);
                                    _emailBody = _emailBody.Replace("[CRA_ADD_BY]", mr.EMP_NAME);
                                    _emailBody = _emailBody.Replace("[CRA_TO_BR]", mr.MR_IBT_STN_NAME);
                                    _emailBody = _emailBody.Replace("[CUR_DATE]", DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                                    _emailBody = _emailBody.Replace("[CRA_NO]", Convert.ToString(mr.MRIBT_CRA_NO).ToUpper());
                                    _emailBody = _emailBody.Replace("[CRA_DATE]", mr.MR_DATE1);
                                    _emailBody = _emailBody.Replace("[CRA_AMT]", Convert.ToString(mr.MR_TOTAL_AMT));
                                    _emailBody = _emailBody.Replace("[CRA_PARTYCODE]", mr.MR_P_CODE);
                                    _emailBody = _emailBody.Replace("[CRA_PARTY]", mr.MR_P_NAME);
                                    _emailBody = _emailBody.Replace("[CRA_PAYMODE]", paymode);

                                    _emailBody = _emailBody.Replace("[FROM_BR_CODE]", Convert.ToString(mr.MR_IBT_STN_NAME).ToUpper());
                                    _emailBody = _emailBody.Replace("[TO_BR_CODE]", Convert.ToString(mr.MR_BR_NAME).ToUpper());

                                    //Convert.ToString(mr.MR_BR_NAME).ToUpper()

                                    Thread email = new Thread(delegate()
                                    {
                                        cf.MailSend(emailId, _cc, _bcc, _subject, _emailBody);
                                    });

                                    email.IsBackground = true;
                                    email.Start();

                                    // give it no more than 30 seconds to execute
                                    //if (!email.Join(new TimeSpan(0, 0, 30)))
                                    //{
                                    //    email.Abort();
                                    //}
                                }
                            }
                        }
                        // Added By - Sunil Kumar Singh Date: 25/11/2020

                        TempData["mrResult"] = mrResult;

                        //return RedirectToAction("MRResult", mrResult);
                        return RedirectToAction("MRResult");
                    }
                    else
                    {
                        Danger(string.Format("<b>" + result + "</b>"), true);
                    }
                }
                catch (Exception ex)
                {
                    Danger(string.Format("<b>Error:</b>" + ex.Message), true);
                }
            }
            else
            {
                Danger(string.Format("<b>Error:102 :</b>" + string.Join("; ", ModelState.Values.SelectMany(z => z.Errors).Select(z => z.ErrorMessage))), true);
            }

            // Bind Accounting Branch list 
            mr.USER_LOC_ID = emp.USER_LOC_ID;

            //List<MST_BRANCH> br_acc = new DAL_Branch().GET_ACCOUNTING_BRANCH_LIST_BY_SCOPE(Convert.ToString(emp.UserTypeShort), Convert.ToDecimal(emp.USER_ID), Convert.ToString(emp.USER_BR_SCOPE), Convert.ToString(emp.USER_BR_TYPE), Convert.ToInt32(emp.USER_LOC_ID));
            //mr.CBS_BR_LIST = new SelectList(br_acc, "BR_ID", "BR_NAME", (br_acc.Count() == 1) ? br_acc.First().BR_ID : emp.USER_LOC_ID);

            List<MST_BRANCH> bookingList = new DAL_Branch().GET_BRANCH_LIST_FOR_CN_BOOKING(emp.UserTypeShort, emp.USER_ID, emp.USER_BR_SCOPE, emp.USER_BR_TYPE, emp.USER_LOC_ID ?? 0);
            mr.CBS_BR_LIST = new SelectList(bookingList, "BR_ID", "BR_NAME", (bookingList.Count() == 1) ? bookingList.First().BR_ID : emp.USER_LOC_ID);

            mr.MR_STN_LIST = new SelectList(bookingList, "BR_ID", "BR_NAME", (bookingList.Count() == 1) ? bookingList.First().BR_ID : emp.USER_LOC_ID);
            mr.EMP_LIST = new SelectList(new DAL_ddList().GET_EMPLOYEE_LIST(emp.USER_LOC_ID ?? 0, 0), "ddlValue", "ddlText", emp.USER_ID);

            mr.MR_TYPE_LIST = new SelectList(new DAL_ddList().GetDropdownList_Dtl("MR_TYPE"), "ddlValue", "ddlText");

            //cn/billing Details
            List<MST_BRANCH> branchList = new DAL_Branch().GET_BRANCH_LIST();
            mr.BILL_CN_BOOKING_BR_LIST = new SelectList(branchList, "BR_ID", "BR_NAME");

            mr.MR_IBT_STN_LIST = new SelectList(branchList, "BR_ID", "BR_NAME");

            mr.BANK_LIST = new SelectList(new DAL_ddList().GetDropdownList_Dtl("BANK"), "ddlValue", "ddlText");
            mr.BFD_REASON_LIST = new SelectList(new DAL_ddList().GetDropdownList_Dtl("BFD_REASON"), "ddlValue", "ddlText");
            return View(mr);
        }

        public ActionResult MRResult()//MR_Result mrResult
        {
            ViewBag.Header = "MR Result";
            MR_Result mrResult = new MR_Result();
            if (TempData["mrResult"] != null)
            {
                mrResult = (MR_Result)TempData["mrResult"];
            }

            //MR_Result mrResult = new MR_Result();

            //mrResult.MR_MANUAL_NO = "BM4000604836";
            //mrResult.MR_NO = "BMRP00000160";
            //mrResult.MR_ID = 7244038;
            //mrResult.MR_DATE = DateTime.Now;

            //List<string> dprList = new List<string>();
            //dprList.Add("BD4001136002");
            //mrResult.DPR_NO_LIST = dprList;
            //mrResult.MRIBT_CRA_NO = "IBT000001324";

            if (System.Web.HttpContext.Current.Session["winprint"] != null)
            {
                mrResult.pg_winprint = Convert.ToString(System.Web.HttpContext.Current.Session["winprint"]);
            }
            else
            {
                mrResult.pg_winprint = "";
            }

            if (System.Web.HttpContext.Current.Session["dosprint"] != null)
            {
                mrResult.pg_dosprint = Convert.ToString(System.Web.HttpContext.Current.Session["dosprint"]);
            }
            else
            {
                mrResult.pg_dosprint = "";
            }

            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme), Url.Action("PrintCommands", "DemoPrintCommands", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);

            return View(mrResult);
        }
        #endregion

        #region MR List View
        public ActionResult MR_List()
        {
            ViewBag.Header = "MR List";
            Mr_List mrList = new Mr_List();
            try
            {
                List<MST_BRANCH> branchList = new DAL_Branch().GET_BRANCH_LIST_FOR_CN_BOOKING(emp.UserTypeShort, emp.USER_ID, emp.USER_BR_SCOPE, emp.USER_BR_TYPE, emp.USER_LOC_ID ?? 0);
                mrList.SEARCH_MR_BR_LIST = new SelectList(branchList, "BR_ID", "BR_NAME", (branchList.Count() == 1) ? branchList.First().BR_ID : emp.USER_LOC_ID);

                mrList.FROM_MR_DATE = DateTime.Now.AddDays(-3);
                mrList.FROM_MR_DATE1 = DateTime.Now.AddDays(-3).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                mrList.TO_MR_DATE = DateTime.Now;
                mrList.TO_MR_DATE1 = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                mrList.SEARCH_MR_TYPE_LIST = new SelectList(new DAL_ddList().GetDropdownList_Dtl("MR_TYPE"), "ddlValue", "ddlText");
            }
            catch (Exception)
            {
                Danger(string.Format("<b>Exception occured.</b>"), true);
            }
            return View(mrList);
        }

        [AjaxOnly]
        public ActionResult _MR_List()
        {
            return PartialView("_MR_List");
        }

        [HttpPost]
        public ActionResult MR_Data_List(int? brId, string mrNo, string fDate, string tDate, int? mrTypeId, decimal? partyAddrId)
        {
            // Server Side Processing
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];
            int totalRow = 0;

            Mr_List mrDatalist = new Mr_List();
            List<Mr_Data_List> mrData = new List<Mr_Data_List>();
            try
            {
                mrDatalist.SEARCH_MR_BR_ID = brId;
                mrDatalist.SEARCH_MR_NO = mrNo;
                mrDatalist.FROM_MR_DATE1 = fDate;
                mrDatalist.TO_MR_DATE1 = tDate;
                mrDatalist.SEARCH_MR_TYPE_ID = mrTypeId;
                mrDatalist.SEARCH_PARTY_ADD_ID = partyAddrId;

                mrData = new DAL_MR().SELECT_MR_DATA_LIST(mrDatalist);
                totalRow = mrData.Count();
            }
            catch (Exception)
            {
                Danger(string.Format("<b>Exception occured.</b>"), true);
            }

            if (!string.IsNullOrEmpty(searchValue)) // Filter Operation
            {
                mrData = mrData.
                    Where(x => x.MR_NO.ToLower().Contains(searchValue.ToLower())
                        || x.MR_DATE1.ToLower().Contains(searchValue.ToLower())
                        || x.MR_BR_NAME.ToLower().Contains(searchValue.ToLower())
                        || x.MR_PARTY_CODE.ToLower().Contains(searchValue.ToLower())
                        || x.MR_PARTY_NAME.ToLower().Contains(searchValue.ToLower())
                        || x.MR_TYPE_TXT.ToLower().Contains(searchValue.ToLower())
                        || x.MR_STATUS.ToLower().Contains(searchValue.ToLower())
                       ).ToList<Mr_Data_List>();
            }
            int totalRowFilter = mrData.Count();
            // sorting
            mrData = mrData.OrderBy(sortColumnName + " " + sortDirection).ToList<Mr_Data_List>();
            // Paging
            if (length == -1)
            {
                length = totalRow;
            }
            mrData = mrData.Skip(start).Take(length).ToList<Mr_Data_List>();

            var jsonResult = Json(new { data = mrData, draw = Request["draw"], recordsTotal = totalRow, recordsFiltered = totalRowFilter }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        #endregion

        #region On-Account Adjustment

        public ActionResult OnAccountAdjustment()
        {
            ViewBag.Header = "On-Account Adjustment";
            On_Account_Adjustment onAcc = new On_Account_Adjustment();
            try
            {
                // Bind Accounting Branch list  
                List<MST_BRANCH> br_acc = new DAL_Branch().GET_ACCOUNTING_BRANCH_LIST_BY_SCOPE(Convert.ToString(emp.UserTypeShort), Convert.ToDecimal(emp.USER_ID), Convert.ToString(emp.USER_BR_SCOPE), Convert.ToString(emp.USER_BR_TYPE), Convert.ToInt32(emp.USER_LOC_ID));
                onAcc.ADJ_BR_LIST = new SelectList(br_acc, "BR_ID", "BR_NAME", (br_acc.Count() == 1) ? br_acc.First().BR_ID : emp.USER_LOC_ID);

                onAcc.MR_ADJ_DATE = DateTime.Now;
                onAcc.MR_ADJ_DATE1 = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                //cn/billing Details
                List<MST_BRANCH> branchList = new DAL_Branch().GET_BRANCH_LIST();
                onAcc.BILL_CN_BOOKING_BR_LIST = new SelectList(branchList, "BR_ID", "BR_NAME");

                //For NR Code
                onAcc.BFD_REASON_LIST = new SelectList(new DAL_ddList().GetDropdownList_Dtl("BFD_REASON"), "ddlValue", "ddlText");
                onAcc.EMP_LIST = new SelectList(new DAL_ddList().GET_EMPLOYEE_LIST(emp.USER_LOC_ID ?? 0, 0), "ddlValue", "ddlText", emp.USER_ID);
            }
            catch (Exception)
            {
                Danger(string.Format("<b>Exception occured.</b>"), true);
            }
            return View(onAcc);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SELECT_MR_OR_ADV_LIST(int adjThrough, int brId, decimal paId, string cbsdate)
        {
            List<MR_OR_ADV_DTLS> l = new DAL_MR().SELECT_MR_OR_ADV_LIST(adjThrough, brId, paId, cbsdate);
            return Json(l, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SELECT_MR_OR_ADV_EXTENDER(int adjThrough, int brId, decimal paId, string searchText)
        {
            List<ddlClass> l = new DAL_MR().SELECT_MR_OR_ADV_EXTENDER(adjThrough, brId, paId, searchText);
            return Json(l, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SELECT_MR_OR_ADV_DATA(int adjThrough, decimal docId, int brId, decimal paId)
        {
            MR_OR_ADV_DTLS l = new DAL_MR().SELECT_MR_OR_ADV_DATA(adjThrough, docId, brId, paId);
            return Json(l, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SELECT_BILL_OR_CN_LIST_FOR_ONACC_ADJ_EXTENDER(int brId, string date, decimal paId, int docType, string searchText)
        {
            List<ddlClass> l = new DAL_MR().SELECT_BILL_OR_CN_LIST_FOR_ONACC_ADJ_EXTENDER(brId, date, paId, docType, searchText);
            return Json(l, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SELECT_BILL_CNS_REF_DTL_FOR_MR_BFD_BY_CN_NO(decimal billId, string cnNo)
        {
            MR_CNS_BFD l = new DAL_MR().SELECT_BILL_CNS_REF_DTL_FOR_MR_BFD_BY_CN_NO(billId, cnNo);
            return Json(l, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [SubmitButtonSelector(Name = "Save")]
        [ActionName("OnAccountAdjustment")]
        public ActionResult OnAccountAdjustment_Submit(On_Account_Adjustment onAcc)
        {
            ViewBag.Header = "On-Account Adjustment";

            if (onAcc.MR_DATE1 != null)
            {
                if (onAcc.MR_DATE1 != "")
                {
                    ModelState["MR_DATE"] = new ModelState();
                    DateTime dt = DateTime.ParseExact(onAcc.MR_DATE1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    onAcc.MR_DATE = dt;
                }
            }

            if (onAcc.TO_CBS_DATE1 != null)
            {
                DateTime dt = DateTime.ParseExact(onAcc.TO_CBS_DATE1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                onAcc.MR_CBS_DATE = dt;
            }
            else
            {
                onAcc.MR_CBS_DATE = onAcc.MR_DATE;
            }

            ModelState["FROM_CBS_DATE"] = new ModelState();
            ModelState["FROM_CBS_DATE1"] = new ModelState();
            ModelState["CBS_OPN_DATE"] = new ModelState();
            ModelState["CBS_OPN_DATE1"] = new ModelState();

            if (onAcc.FROM_CBS_DATE1 != null)
            {
                DateTime dt = DateTime.ParseExact(onAcc.FROM_CBS_DATE1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                onAcc.FROM_CBS_DATE = dt;
            }

            if (onAcc.CBS_OPN_DATE1 != null)
            {
                DateTime dt = DateTime.ParseExact(onAcc.CBS_OPN_DATE1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                onAcc.CBS_OPN_DATE = dt;
            }
            else
            {
                onAcc.CBS_OPN_DATE = onAcc.MR_CBS_DATE;
            }

            if (onAcc.ADJ_DOC_TYPE != 3)//3 for Misc,31-12-2020
            {
                ModelState["MISC_MR_TYPE_ID"] = new ModelState();
            }

            //Added By : Pramesh Kumar Vishwakarma,  08-09-2022
            string tocbsDate1 = "";
            if ((onAcc.TO_CBS_DATE1 ?? "") != "")
            {
                tocbsDate1 = onAcc.TO_CBS_DATE1;
            }
            else
            {
                tocbsDate1 = onAcc.MR_DATE1;
            }
            if ((tocbsDate1 ?? "") != "")
            {
                string errMsg = new DAL_Common().CHECK_CBS_SRP_CLOSSED(Convert.ToInt32(onAcc.MR_BR_ID), tocbsDate1);
                if (errMsg != "")
                {
                    ModelState.AddModelError("CBS_ISSUE", errMsg);
                }
            }

            //End
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                try
                {
                    onAcc.MR_ADJ_ADD_BY_TYPE = emp.UserTypeShort;
                    onAcc.MR_ADJ_ADD_BY = emp.USER_ID;
                    onAcc.MR_CBS_BR_ID = Convert.ToInt32(onAcc.MR_BR_ID);
                    //Added By Pramesh Kumar Vishwakarma, Dated: 31-12-2020
                    if (onAcc.ADJ_DOC_TYPE == 1)// 1 for CN
                    {
                        onAcc.MR_TYPE = 5; //5 for others
                    }
                    else if (onAcc.ADJ_DOC_TYPE == 2) // 2 for Bill
                    {
                        onAcc.MR_TYPE = 5;//5 for others

                    }
                    else if (onAcc.ADJ_DOC_TYPE == 3)//3 for Misc
                    {
                        onAcc.MR_TYPE = onAcc.MISC_MR_TYPE_ID;
                    }
                    //End
                    string result = "";

                    // On-Account IBT MR
                    if (onAcc.MR_TYPE != null && onAcc.MR_OR_ADV_LIST != null)
                    {
                        if (onAcc.MR_TYPE == 4)
                        {
                            if (onAcc.MR_OR_ADV_LIST.Count > 1)
                            {
                                result = "At the time one IBT adjust in On-Account.";
                                onAcc.MR_IBT_NO = null;
                            }
                            else
                            {
                                onAcc.MR_IBT_NO = Convert.ToString(onAcc.MR_OR_ADV_LIST.First().MR_OR_CRA_NO);
                            }

                            if ((onAcc.ADJ_THROUGH ?? 0) != 2)
                            {
                                result = "On-Account IBT adjustment not allowed.";
                                onAcc.MR_IBT_NO = null;
                            }
                        }
                    }

                    if (result == "")
                    {
                        result = new DAL_MR().INSERT_MR_ADJ(onAcc);
                    }

                    if (result == "")
                    {
                        TempData["IS_CBS_INFO_SET"] = onAcc.IS_CBS_INFO_SET;
                        onAcc.IS_CBS_INFO_SET = 1;
                        //Success(string.Format("<b>On Account Adjustment is done successfully...</b>"), true);
                        TempData["MR_NO"] = onAcc.MR_NO;
                        if ((onAcc.ADJ_THROUGH ?? 0) == 2)
                        {
                            TempData["MR_OR_CRA_NO"] = onAcc.MR_OR_ADV_LIST[0].MR_OR_CRA_NO;
                        }

                        //return RedirectToAction("OnAccountAdjustment", "MoneyReceipt");
                    }
                    else
                    {
                        Danger(string.Format("<b>" + result + "</b>"), true);
                    }
                }
                catch (Exception ex)
                {
                    Danger(string.Format("<b>Error:</b>" + ex.Message), true);
                }
            }
            else
            {
                Danger(string.Format("<b>Error:102 :</b>" + string.Join("; ", ModelState.Values.SelectMany(z => z.Errors).Select(z => z.ErrorMessage))), true);
            }

            // Bind Accounting Branch list  
            List<MST_BRANCH> br_acc = new DAL_Branch().GET_ACCOUNTING_BRANCH_LIST_BY_SCOPE(Convert.ToString(emp.UserTypeShort), Convert.ToDecimal(emp.USER_ID), Convert.ToString(emp.USER_BR_SCOPE), Convert.ToString(emp.USER_BR_TYPE), Convert.ToInt32(emp.USER_LOC_ID));
            onAcc.ADJ_BR_LIST = new SelectList(br_acc, "BR_ID", "BR_NAME", (br_acc.Count() == 1) ? br_acc.First().BR_ID : emp.USER_LOC_ID);

            onAcc.MR_ADJ_DATE = DateTime.Now;
            onAcc.MR_ADJ_DATE1 = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            //cn/billing Details
            List<MST_BRANCH> branchList = new DAL_Branch().GET_BRANCH_LIST();
            onAcc.BILL_CN_BOOKING_BR_LIST = new SelectList(branchList, "BR_ID", "BR_NAME");

            //For NR Code
            onAcc.BFD_REASON_LIST = new SelectList(new DAL_ddList().GetDropdownList_Dtl("BFD_REASON"), "ddlValue", "ddlText");
            onAcc.EMP_LIST = new SelectList(new DAL_ddList().GET_EMPLOYEE_LIST(emp.USER_LOC_ID ?? 0, 0), "ddlValue", "ddlText", emp.USER_ID);

            return View(onAcc);
        }

        #endregion

        //--- Added By : Ashish Kalsarpe--- Date : 27/10/2020

        public ActionResult MoneyRealisationReceipt_View(string mrId)
        {
            MR_Receipt mr = new MR_Receipt();
            mr = new DAL_MR().MONEY_REALISATION_RECEIPT_VIEW(Convert.ToDecimal(mrId ?? "0"));
            return PartialView("MoneyRealisationReceipt_View", mr);
        }

        #region  MR Cancellation

        public ActionResult MR_Cancellation()
        {
            ViewBag.Header = "MR Cancellation";
            MR_Cancellation _objMR_Cancellation = new MR_Cancellation();
            // Bind Branch Login Based
            List<MST_BRANCH> Branch_list = new DAL_Branch().GET_BRANCH_LIST_FOR_CN_BOOKING(emp.UserTypeShort, emp.USER_ID, emp.USER_BR_SCOPE, emp.USER_BR_TYPE, emp.USER_LOC_ID ?? 0);
            _objMR_Cancellation.Branch_list = new SelectList(Branch_list, "BR_ID", "BR_NAME", (Branch_list.Count() == 1) ? Branch_list.First().BR_ID : (Branch_list.Count() > 1) ? emp.USER_LOC_ID : 0);
            return View(_objMR_Cancellation);
        }


        public ActionResult MR_Cancellation_Data(int brId, string mrNo)
        {
            MR_Cancellation mr = new MR_Cancellation();
            try
            {
                mr.MR_Search_List = new DAL_MR().SELECT_MR_Cancellation_Data(brId, mrNo);
            }
            catch (Exception ex)
            {
                Danger(string.Format("<b>Exception occured.</b>" + ex.Message), true);
            }
            return PartialView("MR_Cancellation_Data", mr);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult MR_MaintAction(MR_Cancellation mr)
        {
            ViewBag.Header = "MR Cancellation";
            try
            {
                int MR_ID = int.Parse(mr.MR_Search_List[0]._strMR_ID);
                if (MR_ID > 0)
                {
                    mr.MR_ID = MR_ID;
                    mr.MR_ADDBY = emp.USER_ID;
                    mr.MR_USER_TYPE = emp.UserTypeShort;
                    //Added By : Pramesh Kumar Vishwakarma,  09-09-2022
                    string errMsg = "";
                    if ((mr.MR_Search_List[0]._strMR_CBS_DATE ?? "") != "")
                    {
                        errMsg = new DAL_Common().CHECK_CBS_SRP_CLOSSED((mr.MR_Search_List[0]._strMR_CBS_BR_ID ?? 0), mr.MR_Search_List[0]._strMR_CBS_DATE);
                    }
                    //End
                    if (errMsg == "")
                    {
                        string result = new DAL_MR().UPDATE_MR_STATUS(mr);

                        if (result.Contains("Success") == true)
                        {
                            Success(string.Format("<b>" + result + "</b>"), true);
                            TempData["brId"] = mr.BR_ID;
                            TempData["mrNo"] = mr.MR_NO;
                        }
                        else
                        {
                            Danger(string.Format("<b>" + result + "</b>"), true);
                        }
                    }
                    else
                    {
                        Danger(string.Format("<b>Error:</b>" + errMsg), true);
                    }
                }

            }
            catch (Exception ex)
            {
                Danger(string.Format("<b>Error:101-Exception occured. </b>" + ex.Message), true);
            }
            return Json("DONE", JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CancelMRDoc(string mrNo, int mrBrId, string rmk, HttpPostedFileBase flMRCancelCopy)
        {
            string result = "";

            try
            {
                bool copyUpload = false;
                string copyName = string.Empty;
                string copyPath = string.Empty;


                if (flMRCancelCopy != null)
                {
                    copyUpload = true;
                    string ext = new System.IO.FileInfo(flMRCancelCopy.FileName).Extension;
                    copyName = mrNo + ext;
                }

                if (mrNo != "")
                { 
                    result = new DAL_MR().CancelMRDoc(mrNo, mrBrId, rmk, emp.USER_ID, emp.UserTypeShort, copyUpload, copyName, flMRCancelCopy);
                    if (result.Contains("Success") == true)
                    {
                        Success(string.Format("<b>" + result + "</b>"), true);
                    }
                    else
                    {
                        Danger(string.Format("<b>" + result + "</b>"), true);
                    }
                }
            }
            catch (Exception ex)
            {
                Danger(string.Format("<b>Error:105-Exception occured.</b>" + ex.Message), true);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public string GetFileExtension(string base64String)
        {
            var data = base64String.Substring(0, 5);

            switch (data.ToUpper())
            {
                case "IVBOR":
                    return ".png";
                case "/9J/4":
                    return ".jpg";
                case "AAAAF":
                    return ".mp4";
                case "JVBER":
                    return ".pdf";
                case "AAABA":
                    return ".ico";
                case "UMFYI":
                    return ".rar";
                case "E1XYD":
                    return ".rtf";
                case "U1PKC":
                    return ".txt";
                case "MQOWM":
                case "77U/M":
                    return ".srt";
                default:
                    return string.Empty;
            }
        }

        #endregion

        #region OnAccount Adjustment List & View
        public ActionResult onAccountAdjustment_List()
        {
            ViewBag.Header = "On-Account Adjustment";
            On_Account_Adjustment_Search onAcc = new On_Account_Adjustment_Search();
            try
            {
                // Bind Accounting Branch list  
                List<MST_BRANCH> br_acc = new DAL_Branch().GET_ACCOUNTING_BRANCH_LIST_BY_SCOPE(Convert.ToString(emp.UserTypeShort), Convert.ToDecimal(emp.USER_ID), Convert.ToString(emp.USER_BR_SCOPE), Convert.ToString(emp.USER_BR_TYPE), Convert.ToInt32(emp.USER_LOC_ID));
                onAcc.SEARCH_BR_LIST = new SelectList(br_acc, "BR_ID", "BR_NAME", (br_acc.Count() == 1) ? br_acc.First().BR_ID : emp.USER_LOC_ID);

                onAcc.FROM_DATE = DateTime.Now.AddDays(-3);
                onAcc.FROM_DATE1 = DateTime.Now.AddDays(-3).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                onAcc.TO_DATE = DateTime.Now;
                onAcc.TO_DATE1 = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            }
            catch (Exception)
            {
                Danger(string.Format("<b>Exception occured.</b>"), true);
            }
            return View(onAcc);
        }
        [AjaxOnly]
        public ActionResult _OnAccountAdjusted_Data_List()
        {
            return PartialView("_OnAccountAdjusted_Data_List");
        }
        [HttpPost]
        public ActionResult _Get_OnAccountAdjusted_Data_List(int? brId, string fDate, string tDate, decimal? partyAddrId, int? docTypeId, string docNo)
        {
            // Server Side Processing
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];
            int totalRow = 0;

            On_Account_Adjustment_Search mrDatalist = new On_Account_Adjustment_Search();
            List<MR_ADJUST_DATA_LIST> mrData = new List<MR_ADJUST_DATA_LIST>();
            try
            {
                mrDatalist.SEARCH_BR_ID = brId;
                mrDatalist.SEARCH_DOC_NO = docNo;
                mrDatalist.FROM_DATE1 = fDate;
                mrDatalist.TO_DATE1 = tDate;
                mrDatalist.ADJ_DOC_TYPE = docTypeId;
                mrDatalist.SEARCH_PARTY_ADD_ID = partyAddrId;

                mrData = new DAL_MR().SELECT_ONACC_ADJ_DATA_LIST(mrDatalist);
                totalRow = mrData.Count();
            }
            catch (Exception)
            {
                Danger(string.Format("<b>Exception occured.</b>"), true);
            }

            if (!string.IsNullOrEmpty(searchValue)) // Filter Operation
            {
                mrData = mrData.
                    Where(x => x.MR_ADJ_ID.ToString().ToLower().Contains(searchValue.ToLower())
                        || x.MR_ADJ_DATE.ToLower().Contains(searchValue.ToLower())
                        || x.MR_ADJ_BR_NAME.ToLower().Contains(searchValue.ToLower())
                        || x.MR_NO.ToLower().Contains(searchValue.ToLower())
                        || x.MR_DATE.ToLower().Contains(searchValue.ToLower())
                        || x.P_NAME.ToLower().Contains(searchValue.ToLower())
                        || x.MR_ADJ_AMT.ToString().ToLower().Contains(searchValue.ToLower())
                       ).ToList<MR_ADJUST_DATA_LIST>();
            }
            int totalRowFilter = mrData.Count();
            // sorting
            mrData = mrData.OrderBy(sortColumnName + " " + sortDirection).ToList<MR_ADJUST_DATA_LIST>();
            // Paging
            if (length == -1)
            {
                length = totalRow;
            }
            mrData = mrData.Skip(start).Take(length).ToList<MR_ADJUST_DATA_LIST>();

            var jsonResult = Json(new { data = mrData, draw = Request["draw"], recordsTotal = totalRow, recordsFiltered = totalRowFilter }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        #endregion

        #region MR View & Print


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetMR_Status(int? br_id, string MR_NO)
        {
            TBL_MR_STATUS _mrStatus = new DAL_MR().GET_MR_STATUS(br_id, MR_NO);

            return Json(_mrStatus, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MR_VIEW_PRINT()
        {
            ViewBag.Header = "Money Receipt View & Print";

            Mr_List _mrlist = new Mr_List();

            // Bind Branch Login Based
            List<MST_BRANCH> Branch_list = new DAL_Branch().GET_BRANCH_LIST_FOR_CN_BOOKING(emp.UserTypeShort, emp.USER_ID, emp.USER_BR_SCOPE, emp.USER_BR_TYPE, emp.USER_LOC_ID ?? 0);
            _mrlist.SEARCH_MR_BR_LIST = new SelectList(Branch_list, "BR_ID", "BR_NAME", (Branch_list.Count() == 1) ? Branch_list.First().BR_ID : (Branch_list.Count() > 1) ? emp.USER_LOC_ID : 0);


            if (System.Web.HttpContext.Current.Session["winprint"] != null)
            {
                _mrlist.pg_winprint = Convert.ToString(System.Web.HttpContext.Current.Session["winprint"]);
            }
            else
            {
                _mrlist.pg_winprint = "";
            }

            if (System.Web.HttpContext.Current.Session["dosprint"] != null)
            {
                _mrlist.pg_dosprint = Convert.ToString(System.Web.HttpContext.Current.Session["dosprint"]);
            }
            else
            {
                _mrlist.pg_dosprint = "";
            }

            //03/08/2020
            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme), Url.Action("PrintCommands", "DemoPrintCommands", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);

            return View(_mrlist);
        }

        #endregion


        public FileResult ShowDocument(string FilePath)
        {
            return File(FilePath, GetMimeType(FilePath));
        }

        private string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }

        public ActionResult onAccountAdjustment_View(string mrId)
        {
            MR_OnAccount_Adjust mr = new MR_OnAccount_Adjust();
            mr = new DAL_MR().MR_ONACCOUNT_ADJUSTMENT_VIEW(Convert.ToDecimal(mrId ?? "0"));
            return PartialView("onAccountAdjustment_View", mr);
        }

        #region PAID MR CBS DATE ADJUSTMENT
        public ActionResult MR_CBS_Date_Adjust()
        {
            ViewBag.Header = "MR CBS Date Adjustment";
            CBS_DATE_ADJUSTMENT objCBS = new CBS_DATE_ADJUSTMENT();
            List<MST_BRANCH> Branch_list = new DAL_Branch().GET_BRANCH_LIST_FOR_CN_BOOKING(emp.UserTypeShort, emp.USER_ID, emp.USER_BR_SCOPE, emp.USER_BR_TYPE, emp.USER_LOC_ID ?? 0);
            objCBS.DOC_BR_LIST = new SelectList(Branch_list, "BR_ID", "BR_NAME", (Branch_list.Count() == 1) ? Branch_list.First().BR_ID : (Branch_list.Count() > 1) ? emp.USER_LOC_ID : 0);
            return View(objCBS);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GET_MR_CBS_INFO(string docType, string docNo)
        {
            CBS_DATE_ADJUSTMENT mr = new DAL_MR().GET_MR_CBS_INFO(Convert.ToInt32(docType), Convert.ToString(docNo));
            return Json(mr, JsonRequestBehavior.AllowGet);
        }


        public ActionResult MR_CBS_Data(string docId)
        {
            CBS_DATE_ADJUSTMENT objCBS = new CBS_DATE_ADJUSTMENT();
            objCBS = new DAL_MR().GET_PAID_MR_DTLS(Convert.ToDecimal(docId ?? "0"));
            List<MST_BRANCH> bookingList = new DAL_Branch().GET_BRANCH_LIST_FOR_CN_BOOKING(emp.UserTypeShort, emp.USER_ID, emp.USER_BR_SCOPE, emp.USER_BR_TYPE, emp.USER_LOC_ID ?? 0);
            objCBS.CBS_BR_LIST = new SelectList(bookingList, "BR_ID", "BR_NAME", (bookingList.Count() == 1) ? bookingList.First().BR_ID : objCBS.MR_BR_ID);
            return PartialView("MR_CBS_Data", objCBS);

        }

        [HttpPost]
        [SubmitButtonSelector(Name = "Save")]
        [ActionName("MR_CBS_Date_Adjust")]
        public ActionResult MR_CBS_Date_Adjust_Submit(CBS_DATE_ADJUSTMENT objCBS)
        {
            ViewBag.Header = "MR CBS Date Adjustment";
            if (objCBS.MR_DATE_1 != null)
            {
                if (objCBS.MR_DATE_1 != "")
                {
                    ModelState["MR_DATE"] = new ModelState();
                    DateTime dt = DateTime.ParseExact(objCBS.MR_DATE_1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    objCBS.MR_DATE = dt;
                }
            }
            if (objCBS.NEW_CBS_DATE_1 != null)
            {
                string[] date = objCBS.NEW_CBS_DATE_1.Split('/');
                objCBS.NEW_CBS_DATE = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]), 0, 0, 0);
                ModelState["NEW_CBS_DATE"] = new ModelState();
            }
            if (objCBS.NEW_CBS_DATE != null)
            {
                ModelState["NEW_CBS_DATE_1"] = new ModelState();
            }
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                try
                {
                    objCBS.MR_ADD_BY_TYPE = emp.UserTypeShort;
                    objCBS.MR_ADD_BY = emp.USER_ID;
                    if (objCBS.CBS_SHIFT == "0")
                    {
                        objCBS.NEW_CBS_BR_ID = Convert.ToInt32(objCBS.MR_BR_ID);

                    }
                    string result = new DAL_MR().UPDATE_PAID_MR_CBS_DATE(objCBS);
                    if (result == "")
                    {
                        Success(string.Format("<b>MR CBS date Adjustment is done successfully...</b>"), true);
                        return RedirectToAction("MR_CBS_Date_Adjust", "MoneyReceipt");
                    }
                    else
                    {
                        Danger(string.Format("<b>" + result + "</b>"), true);
                    }
                }
                catch (Exception ex)
                {
                    Danger(string.Format("<b>Error:</b>" + ex.Message), true);
                }
            }
            else
            {
                Danger(string.Format("<b>Error:102 :</b>" + string.Join("; ", ModelState.Values.SelectMany(z => z.Errors).Select(z => z.ErrorMessage))), true);
            }

            List<MST_BRANCH> Branch_list = new DAL_Branch().GET_BRANCH_LIST_FOR_CN_BOOKING(emp.UserTypeShort, emp.USER_ID, emp.USER_BR_SCOPE, emp.USER_BR_TYPE, emp.USER_LOC_ID ?? 0);
            objCBS.DOC_BR_LIST = new SelectList(Branch_list, "BR_ID", "BR_NAME", (Branch_list.Count() == 1) ? Branch_list.First().BR_ID : (Branch_list.Count() > 1) ? emp.USER_LOC_ID : 0);
            return View(objCBS);
        }
        #endregion

        #region MR Delete

        public ActionResult MR_DELETE()
        {
            ViewBag.Header = "MR DELETE";

            MR_DELETE mrDelete = new MR_DELETE();
            try
            {
                List<MST_BRANCH> branchList = new DAL_Branch().GET_BRANCH_LIST_FOR_CN_BOOKING(emp.UserTypeShort, emp.USER_ID, emp.USER_BR_SCOPE, emp.USER_BR_TYPE, emp.USER_LOC_ID ?? 0);
                mrDelete.SEARCH_MR_BR_LIST = new SelectList(branchList, "BR_ID", "BR_NAME", (branchList.Count() == 1) ? branchList.First().BR_ID : emp.USER_LOC_ID);

            }
            catch (Exception)
            {
                Danger(string.Format("<b>Exception occured.</b>"), true);
            }
            return View(mrDelete);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Get_MR_Details(int BR_ID, string MR_NO)
        {
            MR_DELETE _mrDel = new DAL_MR().GET_MR_DTLS_BY_MR_NO_FOR_DELETE(BR_ID, MR_NO);
            return Json(_mrDel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [SubmitButtonSelector(Name = "Submit")]
        [ActionName("MR_DELETE")]
        public ActionResult MR_DELETE_Submit(MR_DELETE _mrDel)
        {
            ViewBag.Header = "MR DELETE";

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    _mrDel.MR_DELETE_BY = (int)emp.USER_ID;
                    _mrDel.MR_DELETE_BY_TYPE = emp.UserTypeShort;
                    //Added By : Pramesh Kumar Vishwakarma,  09-09-2022
                    string errMsg = "";
                    if ((_mrDel.MR_CBS_DATE ?? "") != "")
                    {
                        errMsg = new DAL_Common().CHECK_CBS_SRP_CLOSSED((_mrDel.MR_CBS_BR_ID ?? 0), _mrDel.MR_CBS_DATE);
                    }
                    //End
                    if (errMsg == "")
                    {
                        result = new DAL_MR().DELETE_TBL_MR_HDR(_mrDel);
                        if (result == "")
                        {
                            Success(string.Format("<b>MR No : " + _mrDel.MR_NO.ToUpper() + " is deleted successfully...</b>"), true);
                            return Redirect("~/MoneyReceipt/MR_DELETE");
                        }
                        else
                        {
                            Danger(string.Format("<b>" + result + "</b>"), true);
                        }
                    }
                    else
                    {
                        Danger(string.Format("<b>Error:</b>" + errMsg), true);
                    }
                }
                catch (Exception ex)
                {
                    Danger(string.Format("<b>Error:</b>" + ex.Message), true);
                }
            }
            else
            {
                Danger(string.Format("<b>Error:102 :</b>" + string.Join("; ", ModelState.Values.SelectMany(z => z.Errors).Select(z => z.ErrorMessage))), true);
            }

            List<MST_BRANCH> branchList = new DAL_Branch().GET_BRANCH_LIST_FOR_CN_BOOKING(emp.UserTypeShort, emp.USER_ID, emp.USER_BR_SCOPE, emp.USER_BR_TYPE, emp.USER_LOC_ID ?? 0);
            _mrDel.SEARCH_MR_BR_LIST = new SelectList(branchList, "BR_ID", "BR_NAME", (branchList.Count() == 1) ? branchList.First().BR_ID : emp.USER_LOC_ID);


            return View(_mrDel);
        }

        #endregion

        // Added by Pramesh kumar Vishwakarma, Date:17-08-2022
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CHECK_HO_APPROVAL_FOR_MISC_PARTY(int brId, int mrType, string mrNo)
        {
            string msg = new DAL_MR().CHECK_HO_APPROVAL_FOR_MISC_PARTY(brId, mrType, mrNo);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        // Added by Pramesh kumar Vishwakarma, Date:18-08-2022
        #region Update from misc party to valid party code in MR

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GET_MR_INFO_FOR_PARTY_UPDATE(string mrNo)
        {
            MR_INFO_FOR_PARTY_UPDATE mr = new DAL_MR().GET_MR_INFO_FOR_PARTY_UPDATE(mrNo);
            return Json(mr, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Update_From_Misc_To_Valid_Party()
        {
            ViewBag.Header = "Update From Misc To Valid Party";
            MR_INFO_FOR_PARTY_UPDATE mr = new MR_INFO_FOR_PARTY_UPDATE();
            List<MST_BRANCH> bookingList = new DAL_Branch().GET_BRANCH_LIST_FOR_CN_BOOKING(emp.UserTypeShort, emp.USER_ID, emp.USER_BR_SCOPE, emp.USER_BR_TYPE, emp.USER_LOC_ID ?? 0);
            mr.BR_LIST = new SelectList(bookingList, "BR_ID", "BR_NAME", (bookingList.Count() == 1) ? bookingList.First().BR_ID : emp.USER_LOC_ID);
            return View(mr);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update_From_Misc_To_Valid_Party(MR_INFO_FOR_PARTY_UPDATE mr)
        {
            ViewBag.Header = "Update From Misc To Valid Party";

            if (ModelState.IsValid)
            {
                try
                {
                    string result = "";
                    if ((mr.MR_ID ?? 0) == 0)
                    {
                        result = "Please enter valid mr no.";
                    }
                    else if ((mr.MR_PA_ID ?? 0) == 0)
                    {
                        result = "Please enter valid party.";
                    }

                    mr.MR_UPDATED_BY_TYPE = emp.UserTypeShort;
                    mr.MR_UPDATED_BY = emp.USER_ID;

                    if (result == "")
                    {
                        result = new DAL_MR().Update_From_Misc_To_Valid_Party_In_MR(mr);
                    }

                    if (result == "")
                    {
                        Success(string.Format("<b>Party is updated successfully...</b>"), true);
                        return RedirectToAction("Update_From_Misc_To_Valid_Party", "MoneyReceipt");
                    }
                    else
                    {
                        Danger(string.Format("<b>" + result + "</b>"), true);
                    }
                }
                catch (Exception ex)
                {
                    Danger(string.Format("<b>Error:</b>" + ex.Message), true);
                }
            }
            else
            {
                Danger(string.Format("<b>Error:102 :</b>" + string.Join("; ", ModelState.Values.SelectMany(z => z.Errors).Select(z => z.ErrorMessage))), true);
            }

            List<MST_BRANCH> bookingList = new DAL_Branch().GET_BRANCH_LIST_FOR_CN_BOOKING(emp.UserTypeShort, emp.USER_ID, emp.USER_BR_SCOPE, emp.USER_BR_TYPE, emp.USER_LOC_ID ?? 0);
            mr.BR_LIST = new SelectList(bookingList, "BR_ID", "BR_NAME", (bookingList.Count() == 1) ? bookingList.First().BR_ID : emp.USER_LOC_ID);
            return View(mr);
        }

        #endregion

        #region Request Miscellaneous Party

        public ActionResult MR_RequestMiscellaneousParty()
        {
            ViewBag.Header = "Request Miscellaneous Party For MR";

            TBL_MR_REQ_MISC_PRTY objTBL_MR_REQ_MISC_PRTY = new TBL_MR_REQ_MISC_PRTY();

            List<MST_BRANCH> bookingList = new DAL_Branch().GET_BRANCH_LIST_FOR_CN_BOOKING(emp.UserTypeShort, emp.USER_ID, emp.USER_BR_SCOPE, emp.USER_BR_TYPE, emp.USER_LOC_ID ?? 0);
            objTBL_MR_REQ_MISC_PRTY.BR_LIST = new SelectList(bookingList, "BR_ID", "BR_NAME", (bookingList.Count() == 1) ? bookingList.First().BR_ID : emp.USER_LOC_ID);


            return View(objTBL_MR_REQ_MISC_PRTY);
        }


        [Authorize]
        [HttpPost]
        [SubmitButtonSelector(Name = "Submit")]
        [ActionName("MR_RequestMiscellaneousParty")]
        public ActionResult MR_RequestMiscellaneousParty_Submit(TBL_MR_REQ_MISC_PRTY objTBL_MR_REQ_MISC_PRTY)
        {
            ViewBag.Header = "Request Miscellaneous Party";
            if (ModelState.IsValid)
            {
                try
                {
                    LogInUser user = (LogInUser)Session["LogInUser"];
                    if (Session["LogInUser"] != null)
                    {
                        string[] result = new string[2];
                        objTBL_MR_REQ_MISC_PRTY.RQMSPY_ADD_BY = Convert.ToInt32(user.USER_ID);
                        objTBL_MR_REQ_MISC_PRTY.RQMSPY_ADD_BY_TYPE = Convert.ToString(user.UserType.Substring(0, 1));

                        result = new DAL_MR().INSERT_TBL_MR_REQ_MISC_PRTY(objTBL_MR_REQ_MISC_PRTY);

                        if (result[0] == "")
                        {
                            Success(string.Format("<b>Request successfully submitted.</b>"), true);
                            return RedirectToAction("MR_RequestMiscellaneousParty");
                        }
                        else
                        {
                            Danger(string.Format("<b>" + result[0] + "</b>"), true);
                        }
                    }
                }
                catch
                {
                    Danger(string.Format("<b>Exception occured.</b>"), true);
                }
            }

            List<MST_BRANCH> bookingList = new DAL_Branch().GET_BRANCH_LIST_FOR_CN_BOOKING(emp.UserTypeShort, emp.USER_ID, emp.USER_BR_SCOPE, emp.USER_BR_TYPE, emp.USER_LOC_ID ?? 0);
            objTBL_MR_REQ_MISC_PRTY.BR_LIST = new SelectList(bookingList, "BR_ID", "BR_NAME", (bookingList.Count() == 1) ? bookingList.First().BR_ID : emp.USER_LOC_ID);


            return View(objTBL_MR_REQ_MISC_PRTY);

        }


        #endregion

        #region Request Miscellaneous Party Approval

        public ActionResult MR_RequestMiscellaneousPartyApproval()
        {
            ViewBag.Header = "Approval Miscellaneous Party For MR";

            TBL_MR_REQ_MISC_PRTY objTBL_MR_REQ_MISC_PRTY = new TBL_MR_REQ_MISC_PRTY();

            List<MST_BRANCH> Branch_list = new DAL_Branch().GET_ALL_BRANCH_LIST();
            objTBL_MR_REQ_MISC_PRTY.BR_LIST = new SelectList(Branch_list, "BR_ID", "BR_NAME", (Branch_list.Count() == 1) ? Branch_list.First().BR_ID : emp.USER_LOC_ID);

            return View(objTBL_MR_REQ_MISC_PRTY);
        }

        public ActionResult _MR_RequestMiscellaneousPartyApproval()
        {
            return PartialView("_MR_RequestMiscellaneousPartyApproval");
        }

        [HttpPost]
        public ActionResult _MR_RequestMiscellaneousPartyApproval(int? BR_ID, string MR_NO, int APPRV_STATUS)
        {
            // Server Side Processing
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];
            int totalRow = 0;

            TBL_MR_REQ_MISC_PRTY objTBL_MR_REQ_MISC_PRTY = new TBL_MR_REQ_MISC_PRTY();

            List<MR_REQ_MISC_PRTY_LIST> objMR_REQ_MISC_PRTY_LIST = new List<MR_REQ_MISC_PRTY_LIST>();

            try
            {
                objTBL_MR_REQ_MISC_PRTY.RQMSPY_MR_BR_ID = (int)(BR_ID == null ? 0 : BR_ID);
                objTBL_MR_REQ_MISC_PRTY.RQMSPY_MR_NO = MR_NO;
                objTBL_MR_REQ_MISC_PRTY.RQMSPY_APPRV_STATUS = APPRV_STATUS;

                objMR_REQ_MISC_PRTY_LIST = new DAL_MR().SELECT_TBL_MR_REQ_MISC_PRTY_LIST(objTBL_MR_REQ_MISC_PRTY);

                totalRow = objMR_REQ_MISC_PRTY_LIST.Count();

            }
            catch (Exception ex)
            {
                Danger(string.Format("<b>Exception occured.</b>"), true);
            }

            if (!string.IsNullOrEmpty(searchValue)) // Filter Operation
            {

                objMR_REQ_MISC_PRTY_LIST = objMR_REQ_MISC_PRTY_LIST.
                                    Where(x => x.RQMSPY_MR_TYPE_NM.ToLower().Contains(searchValue.ToLower())
                                        || x.RQMSPY_MR_BR_NM.ToLower().Contains(searchValue.ToLower())
                                        || x.RQMSPY_MR_NO.ToLower().Contains(searchValue.ToLower())
                                        || x.RQMSPY_APPRV_STATUS_NM.ToLower().Contains(searchValue.ToLower())).ToList<MR_REQ_MISC_PRTY_LIST>();
            }
            int totalRowFilter = objMR_REQ_MISC_PRTY_LIST.Count();
            // sorting        
            objMR_REQ_MISC_PRTY_LIST = objMR_REQ_MISC_PRTY_LIST.OrderBy(sortColumnName + " " + sortDirection).ToList<MR_REQ_MISC_PRTY_LIST>();
            // Paging
            if (length == -1)
            {
                length = totalRow;
            }
            objMR_REQ_MISC_PRTY_LIST = objMR_REQ_MISC_PRTY_LIST.Skip(start).Take(length).ToList<MR_REQ_MISC_PRTY_LIST>();

            var jsonResult = Json(new { data = objMR_REQ_MISC_PRTY_LIST, draw = Request["draw"], recordsTotal = totalRow, recordsFiltered = totalRowFilter }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult APPROVE_REJECT_MR_REQ_MISC_PRTY(string strRQMSPY_ID, string strRQMSPY_APPRV_STATUS, string strRQMSPY_APPRV_REMARKS)
        {
            TBL_MR_REQ_MISC_PRTY objTBL_MR_REQ_MISC_PRTY = new TBL_MR_REQ_MISC_PRTY();


            objTBL_MR_REQ_MISC_PRTY.RQMSPY_ID = Convert.ToInt32(strRQMSPY_ID);
            objTBL_MR_REQ_MISC_PRTY.RQMSPY_APPRV_STATUS = Convert.ToInt32(strRQMSPY_APPRV_STATUS);
            objTBL_MR_REQ_MISC_PRTY.RQMSPY_APPRV_REMARKS = strRQMSPY_APPRV_REMARKS;
            objTBL_MR_REQ_MISC_PRTY.RQMSPY_APPRV_ADD_BY = Convert.ToInt32(emp.USER_ID);

            string strResult = new DAL_MR().UPDATE_APPROVE_REJECT_MR_REQ_MISC_PRTY(objTBL_MR_REQ_MISC_PRTY);
            return Json(strResult, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Cash Pay MR Pay Mode Change

        public ActionResult MR_PayModeChange()
        {
            ViewBag.Header = "Cash Pay MR Pay Mode Change";

            MR_PAY_MODE_CHNG objMR_PAY_MODE_CHNG = new MR_PAY_MODE_CHNG();

            // Bind Branch Login Based
            List<MST_BRANCH> Branch_list = new DAL_Branch().GET_BRANCH_LIST_FOR_CN_BOOKING(emp.UserTypeShort, emp.USER_ID, emp.USER_BR_SCOPE, emp.USER_BR_TYPE, emp.USER_LOC_ID ?? 0);
            objMR_PAY_MODE_CHNG.SEARCH_MR_BR_LIST = new SelectList(Branch_list, "BR_ID", "BR_NAME", (Branch_list.Count() == 1) ? Branch_list.First().BR_ID : (Branch_list.Count() > 1) ? emp.USER_LOC_ID : 0);

            objMR_PAY_MODE_CHNG.BANK_LIST = new SelectList(new DAL_ddList().GetDropdownList_Dtl("BANK"), "ddlValue", "ddlText");

            return View(objMR_PAY_MODE_CHNG);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GET_MR_DTL_FOR_PAYMODE_CHANGE(int MR_BR_ID, string MR_NO)
        {
            MR_PAY_MODE_CHNG objMR_PAY_MODE_CHNG = new MR_PAY_MODE_CHNG();

            objMR_PAY_MODE_CHNG = new DAL_MR().GET_MR_DTL_FOR_PAYMODE_CHANGE(MR_BR_ID, MR_NO);

            return Json(objMR_PAY_MODE_CHNG, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [SubmitButtonSelector(Name = "Submit")]
        [ActionName("MR_PayModeChange")]
        public ActionResult MR_PayModeChange_Submit(MR_PAY_MODE_CHNG objMR_PAY_MODE_CHNG)
        {
            try
            {

                string result = string.Empty;

                LogInUser user = (LogInUser)Session["LogInUser"];
                objMR_PAY_MODE_CHNG.MODIFIED_BY = Convert.ToInt32(user.USER_ID);
                objMR_PAY_MODE_CHNG.MODIFIED_TYPE = Convert.ToString(user.UserType.Substring(0, 1));

                result = new DAL_MR().UPDATE_MR_DTL_FOR_PAYMODE_CHANGE(objMR_PAY_MODE_CHNG);

                if (result == "")
                {
                    Success(string.Format("<b>Pay Mode Changed successfully.</b>"), true);

                    return RedirectToAction("MR_PayModeChange", "MoneyReceipt");
                }
                else
                {
                    Danger(string.Format("<b>" + result + "</b>"), true);
                }


            }
            catch (Exception ex)
            {
                Danger(string.Format("<b>Exception occured : " + ex.Message + "</b>"), true);
            }


            // Bind Branch Login Based
            List<MST_BRANCH> Branch_list = new DAL_Branch().GET_BRANCH_LIST_FOR_CN_BOOKING(emp.UserTypeShort, emp.USER_ID, emp.USER_BR_SCOPE, emp.USER_BR_TYPE, emp.USER_LOC_ID ?? 0);
            objMR_PAY_MODE_CHNG.SEARCH_MR_BR_LIST = new SelectList(Branch_list, "BR_ID", "BR_NAME", (Branch_list.Count() == 1) ? Branch_list.First().BR_ID : (Branch_list.Count() > 1) ? emp.USER_LOC_ID : 0);

            objMR_PAY_MODE_CHNG.BANK_LIST = new SelectList(new DAL_ddList().GetDropdownList_Dtl("BANK"), "ddlValue", "ddlText");



            return View(objMR_PAY_MODE_CHNG);
        }

        #endregion

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetFleetDetailsByCode(string fl_code, int CTRL_BR_ID)
        {
            object fl = new DAL_MR().GET_FLEET_DTL_BY_CODE(fl_code, CTRL_BR_ID);
            return Json(fl, JsonRequestBehavior.AllowGet);
        }

        //Added by Pramesh kumar Vishwakarma, Date:09-01-2023
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult MiscPartyGSTValidate(string paName, string pGST)
        {
            string errmsg = "";
            // ARC GST NO 24 AACCA4861C 2Z4
            string arcPAN = "AACCA4861C";
            bool res = pGST.Contains(arcPAN);
            if (res)
            {
                pGST = "";
            }

            string PA_GST_STATUS = "";

            if (pGST != "")
            {
                // Get GST Data from NIC Server
                eWaybillServices _eWaybillServices = new eWaybillServices();
                /*
                // For Authentication
                AuthResponse objresp;
                byte[] _aeskey;
                _eWaybillServices.GetAuthToken(out objresp, out _aeskey, out errmsg);
                */

                // For GSTIN Details
                // 29AACCA4861C2ZU ARC Karnatka GST
                Gstin_Info gstinObj;

                string six_char = string.Empty;
                //_eWaybillServices.GetGSTINDetails(objresp, pGST, _aeskey, out gstinObj, out errmsg);
                //_eWaybillServices.GetGSTINDetails(pGST, out gstinObj, out errmsg);

                //Added by Ashok Date : 26-09-2022
                string connStr = new DAL_Common().GetConnectionString();
                _eWaybillServices.GetGSTINDetails(pGST, out gstinObj, out errmsg, connStr);

                if (errmsg.Trim() == "")
                {
                    if (gstinObj.gstin != null)
                    {
                        string GSTSTATUS = Convert.ToString(gstinObj.status) == "ACT" ? "Active" : "Cancelled";
                        string PA_TRADE_NAME = Convert.ToString(gstinObj.tradeName);
                        string P_LEGAL_NAME = Convert.ToString(gstinObj.legalName);
                        PA_GST_STATUS = GSTSTATUS;

                        if (Convert.ToString(pGST).Length > 6)
                        {
                            six_char = Convert.ToString(pGST).Substring(5, 1);
                        }

                        // MISC PARTY NOT CHECK
                        //if (six_char != "P")
                        //{
                        List<MST_EXCEPTION_BYPASS> excList = new DAL_Party().SELECT_EXCEPTION_BYPASS();

                        string partyName = paName.ToUpper().Trim();
                        string tradeName = (PA_TRADE_NAME ?? string.Empty).ToUpper().Trim();
                        string legalName = (P_LEGAL_NAME ?? string.Empty).ToUpper().Trim();

                        System.Text.StringBuilder _partyName = new System.Text.StringBuilder().Append(paName.ToUpper().Trim());
                        System.Text.StringBuilder _tradeName = new System.Text.StringBuilder().Append((PA_TRADE_NAME ?? string.Empty).ToUpper().Trim());
                        System.Text.StringBuilder _legalName = new System.Text.StringBuilder().Append((P_LEGAL_NAME ?? string.Empty).ToUpper().Trim());

                        foreach (MST_EXCEPTION_BYPASS exc in excList)
                        {
                            _partyName = _partyName.Replace(exc.BYPASS_CHAR_A, exc.BYPASS_EQUI_CHAR);
                            _tradeName = _tradeName.Replace(exc.BYPASS_CHAR_A, exc.BYPASS_EQUI_CHAR);
                            _legalName = _legalName.Replace(exc.BYPASS_CHAR_A, exc.BYPASS_EQUI_CHAR);
                        }

                        if ((_tradeName.Equals(_partyName) || _legalName.Equals(_partyName))) { }
                        else if ((tradeName == partyName || legalName == partyName)) { }
                        else
                        {
                            PA_GST_STATUS = "GSTIN Unmatched";
                        }
                        // }
                    }

                    else
                    {
                        PA_GST_STATUS = "Invalid";
                    }
                }
                else
                {
                    if (errmsg.Trim() == "Invalid GSTIN / UID")
                    {
                        PA_GST_STATUS = errmsg.Trim();
                    }
                    else
                    {
                        PA_GST_STATUS = "GSTIN Server not working";
                    } 
                }
            }
            else
            {
                if (res == true)
                {
                    PA_GST_STATUS = "ARC GSTIN not allowed";
                } 
            }

            return Json(PA_GST_STATUS, JsonRequestBehavior.AllowGet);
        }

        // Added by Pramesh kumar Vishwakarma, Date:12-01-2023
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CHECK_MR_NO_FOR_MISC_PARTY(int brId, int mrType, string mrNo)
        {
            string msg = new DAL_MR().CHECK_MR_NO_FOR_MISC_PARTY(brId, mrType, mrNo);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #region OnAccount MR Freight Refund to Party.


        public ActionResult OnAcc_MR_FRT_Refund()
        {
            ViewBag.Header = "MR On-Account Freight Refund";
            OnAcc_MR_FRT_Refund_Req objRR = new OnAcc_MR_FRT_Refund_Req();
            List<MST_BRANCH> bookingList = new DAL_Branch().GET_BRANCH_LIST_FOR_CN_BOOKING(emp.UserTypeShort, emp.USER_ID, emp.USER_BR_SCOPE, emp.USER_BR_TYPE, emp.USER_LOC_ID ?? 0);
            objRR.BR_LIST = new SelectList(bookingList, "BR_ID", "BR_NAME", (bookingList.Count() == 1) ? bookingList.First().BR_ID : emp.USER_LOC_ID);
            return View(objRR);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SELECT_ONACCOUNT_MR_DATA(string mrNo, int brId)
        {
            MR_OR_ADV_DTLS l = new DAL_MR().SELECT_ONACCOUNT_MR_DATA(mrNo, brId);
            return Json(l, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        [SubmitButtonSelector(Name = "Save")]
        [ActionName("OnAcc_MR_FRT_Refund")]
        public ActionResult OnAcc_MR_FRT_Refund_Submit(OnAcc_MR_FRT_Refund_Req objRR)
        {
            ViewBag.Header = "Request Miscellaneous Party";
            if (ModelState.IsValid)
            {
                try
                {
                    string result = "";

                    LogInUser user = (LogInUser)Session["LogInUser"];
                    if (Session["LogInUser"] != null)
                    {
                        objRR.MR_REF_REQ_ADDBY = Convert.ToInt32(user.USER_ID);
                        objRR.MR_REF_REQ_ADDBY_TYPE = Convert.ToString(user.UserType.Substring(0, 1));
                        result = new DAL_MR().INSERT_TBL_MR_FRT_REFUND_REQ(objRR);
                        if (result == "")
                        {
                            Success(string.Format("<b>Request successfully submitted.</b>"), true);
                            return RedirectToAction("OnAcc_MR_FRT_Refund");
                        }
                        else
                        {
                            Danger(string.Format("<b>" + result + "</b>"), true);
                        }
                    }
                }
                catch
                {
                    Danger(string.Format("<b>Exception occured.</b>"), true);
                }
            }
            List<MST_BRANCH> bookingList = new DAL_Branch().GET_BRANCH_LIST_FOR_CN_BOOKING(emp.UserTypeShort, emp.USER_ID, emp.USER_BR_SCOPE, emp.USER_BR_TYPE, emp.USER_LOC_ID ?? 0);
            objRR.BR_LIST = new SelectList(bookingList, "BR_ID", "BR_NAME", (bookingList.Count() == 1) ? bookingList.First().BR_ID : emp.USER_LOC_ID);

            return View(objRR);
        }


        public ActionResult OnAcc_MR_FRT_Refund_Approval()
        {
            ViewBag.Header = "MR On-Account Freight Refund Approval";
            OnAcc_MR_FRT_Refund_Req objRR = new OnAcc_MR_FRT_Refund_Req();
            List<MST_BRANCH> bookingList = new DAL_Branch().GET_BRANCH_LIST_FOR_CN_BOOKING(emp.UserTypeShort, emp.USER_ID, emp.USER_BR_SCOPE, emp.USER_BR_TYPE, emp.USER_LOC_ID ?? 0);
            objRR.BR_LIST = new SelectList(bookingList, "BR_ID", "BR_NAME", (bookingList.Count() == 1) ? bookingList.First().BR_ID : emp.USER_LOC_ID);
            return View(objRR);
        }

        public ActionResult _OnAcc_MR_FRT_Refund_Approval()
        {
            return PartialView("_OnAcc_MR_FRT_Refund_Approval");
        }


        [HttpPost]
        public ActionResult OnAcc_MR_FRT_Refund_Approval_list(int? BR_ID, string MR_NO, int? APPRV_STATUS)
        {
            // Server Side Processing
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];
            int totalRow = 0;
            OnAcc_MR_FRT_Refund_Req objMR_FRT_Refund_Req = new OnAcc_MR_FRT_Refund_Req();
            List<MR_FRT_REF_LIST> objMR_REQ_LIST = new List<MR_FRT_REF_LIST>();
            try
            {
                objMR_FRT_Refund_Req.RR_MR_BR_ID = (int)(BR_ID == null ? 0 : BR_ID);
                objMR_FRT_Refund_Req.RR_MR_NO = MR_NO;
                objMR_FRT_Refund_Req.APPRV_STATUS = APPRV_STATUS;
                objMR_REQ_LIST = new DAL_MR().SELECT_MR_FRT_REF_LIST(objMR_FRT_Refund_Req);
                totalRow = objMR_REQ_LIST.Count();
            }
            catch (Exception ex)
            {
                Danger(string.Format("<b>Exception occured.</b>"), true);
            }

            if (!string.IsNullOrEmpty(searchValue)) // Filter Operation
            {
                objMR_REQ_LIST = objMR_REQ_LIST.
                                    Where(x => x.RF_MR_NO.ToLower().Contains(searchValue.ToLower())
                                        || x.RF_MR_BR_NAME.ToLower().Contains(searchValue.ToLower())
                                        || x.RF_MR_TRANSFER_AMT.ToString().ToLower().Contains(searchValue.ToLower())
                                        || x.RF_REMARKS.ToLower().Contains(searchValue.ToLower())
                                        ).ToList<MR_FRT_REF_LIST>();
            }
            int totalRowFilter = objMR_REQ_LIST.Count();
            // sorting        
            objMR_REQ_LIST = objMR_REQ_LIST.OrderBy(sortColumnName + " " + sortDirection).ToList<MR_FRT_REF_LIST>();
            // Paging
            if (length == -1)
            {
                length = totalRow;
            }
            objMR_REQ_LIST = objMR_REQ_LIST.Skip(start).Take(length).ToList<MR_FRT_REF_LIST>();

            var jsonResult = Json(new { data = objMR_REQ_LIST, draw = Request["draw"], recordsTotal = totalRow, recordsFiltered = totalRowFilter }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        } 

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult APPROVE_REJECT_MR_FRT_REFUND_REQ_STATUS(string strMR_REF_REQ_ID, string strAPPRV_STATUS, string strAPPRV_REMARKS)
        {
            LogInUser user = (LogInUser)Session["LogInUser"];

            OnAcc_MR_FRT_Refund_Req objTBL_MR_REQ = new OnAcc_MR_FRT_Refund_Req();
            objTBL_MR_REQ.MR_REF_REQ_ID = Convert.ToDecimal(strMR_REF_REQ_ID);
            objTBL_MR_REQ.APPRV_STATUS = Convert.ToInt32(strAPPRV_STATUS);
            objTBL_MR_REQ.APPRV_REMARKS = Convert.ToString(strAPPRV_REMARKS);
            objTBL_MR_REQ.MR_REF_REQ_ADDBY = Convert.ToInt32(emp.USER_ID);
            objTBL_MR_REQ.MR_REF_REQ_ADDBY_TYPE = Convert.ToString(user.UserType.Substring(0, 1));

            string strResult = new DAL_MR().UPDATE_APPROVE_REJECT_MR_FRT_REFUND_REQ_STATUS(objTBL_MR_REQ);
            return Json(strResult, JsonRequestBehavior.AllowGet);

        }

        #endregion


        #region on Account MR Party change

        public ActionResult OnAcc_MR_Party_Change() 
        {
            ViewBag.Header = "On-Account MR Party Change";
            OnAcc_MR_Party_Change objMPC = new OnAcc_MR_Party_Change();

            List<MST_BRANCH> bookingList = new DAL_Branch().GET_BRANCH_LIST_FOR_CN_BOOKING(emp.UserTypeShort, emp.USER_ID, emp.USER_BR_SCOPE, emp.USER_BR_TYPE, emp.USER_LOC_ID ?? 0);
            objMPC.BR_LIST = new SelectList(bookingList, "BR_ID", "BR_NAME", (bookingList.Count() == 1) ? bookingList.First().BR_ID : emp.USER_LOC_ID);
            return View(objMPC);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SELECT_OnAcc_MR_Party_Change(string mrNo, int brId)
        {
            OnAcc_MR_Party_Change l = new DAL_MR().SELECT_OnAcc_MR_Party_Change(mrNo, brId);
            return Json(l, JsonRequestBehavior.AllowGet);
        }



        [Authorize]
        [HttpPost]
        [SubmitButtonSelector(Name = "Save")]
        [ActionName("OnAcc_MR_Party_Change")]
        public ActionResult INSERT_TBL_MR_ONACC_PARTY_CHANGE(OnAcc_MR_Party_Change objMR_PARTY_CHNAGE)
        {
            ViewBag.Header = "On-Account MR Party Change";
            if (ModelState.IsValid)
            {
                try
                {
                    string result = "";
                    LogInUser user = (LogInUser)Session["LogInUser"];
                    if (Session["LogInUser"] != null)
                    {
                        objMR_PARTY_CHNAGE.MR_ADDBY = Convert.ToInt32(user.USER_ID);
                        objMR_PARTY_CHNAGE.MR_ADDBY_TYPE = Convert.ToString(user.UserType.Substring(0, 1));
                        result = new DAL_MR().INSERT_TBL_MR_ONACC_PARTY_CHANGE(objMR_PARTY_CHNAGE);
                        if (result == "")
                        {
                            Success(string.Format("<b>MR Party Changed Successfully .!!.</b>"), true);
                            return RedirectToAction("OnAcc_MR_Party_Change");
                        }
                        else
                        {
                           
                            Danger(string.Format("<b>" + result + "</b>"), true);
                        }
                    }
                }
                catch
                {
                    Danger(string.Format("<b>Exception occured.</b>"), true);
                }
            }
            List<MST_BRANCH> bookingList = new DAL_Branch().GET_BRANCH_LIST_FOR_CN_BOOKING(emp.UserTypeShort, emp.USER_ID, emp.USER_BR_SCOPE, emp.USER_BR_TYPE, emp.USER_LOC_ID ?? 0);
            objMR_PARTY_CHNAGE.BR_LIST = new SelectList(bookingList, "BR_ID", "BR_NAME", (bookingList.Count() == 1) ? bookingList.First().BR_ID : emp.USER_LOC_ID);

            return View(objMR_PARTY_CHNAGE);
        }

       
      

        #endregion

    }
}
