using BusinessLayer.Entity;
using BusinessLayer.Entity.iFMS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace BusinessLayer.DAL.iFMS
{
    public class DAL_MR
    {
        string ftp_Path;
        string ftp_User;
        string ftp_Pwd;
        public DAL_MR()
        {
            ftp_Path = ConfigurationManager.AppSettings["iTMSFTPHOST"].ToString();
            ftp_User = ConfigurationManager.AppSettings["iTMSFTPUSER"].ToString();
            ftp_Pwd = ConfigurationManager.AppSettings["iTMSFTPPWD"].ToString();
        }
        //------------ ASHISH END------------------------------------

        public PRINT_MR PAID_MR_PRINT(decimal MR_ID, string MR_NO)
        {
            PRINT_MR mr = new PRINT_MR();
            clsPrintSettings obj = new clsPrintSettings();
            SqlParameter[] param = { new SqlParameter("@MR_ID", MR_ID), new SqlParameter("@MR_NO", MR_NO) };
            DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iTMS].[USP_MR_PRINT_PAID]", CommandType.StoredProcedure, param);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    #region Basic Information-1

                    mr.MR_ID = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_ID"]);
                    mr.MR_NO = Convert.ToString(ds.Tables[0].Rows[0]["MR_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_NO"]);
                    mr.MR_DATE = Convert.ToString(ds.Tables[0].Rows[0]["MR_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_DATE"]);
                    mr.MR_TYPE = Convert.ToString(ds.Tables[0].Rows[0]["MR_TYPE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_TYPE"]);
                    mr.MR_TYPE_NAME = Convert.ToString(ds.Tables[0].Rows[0]["MR_TYPE_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_TYPE_NAME"]);
                    mr.BR_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["BR_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["BR_ID"]);
                    mr.BR_CODE = Convert.ToString(ds.Tables[0].Rows[0]["BR_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["BR_CODE"]);
                    mr.BR_NAME = Convert.ToString(ds.Tables[0].Rows[0]["BR_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["BR_NAME"]);
                    mr.BR_ADDRESS = Convert.ToString(ds.Tables[0].Rows[0]["BR_ADDRESS"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["BR_ADDRESS"]);
                    mr.BR_GSTN = Convert.ToString(ds.Tables[0].Rows[0]["BR_GSTN"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["BR_GSTN"]);
                    mr.BR_SAC = Convert.ToString(ds.Tables[0].Rows[0]["BR_SAC"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["BR_SAC"]);
                    mr.PAYMENT_REC_FROM = Convert.ToString(ds.Tables[0].Rows[0]["PAYMENT_REC_FROM"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["PAYMENT_REC_FROM"]);

                    mr.CNOR_CODE = Convert.ToString(ds.Tables[0].Rows[0]["CNOR_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CNOR_CODE"]);
                    mr.CNOR_NAME = Convert.ToString(ds.Tables[0].Rows[0]["CNOR_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CNOR_NAME"]);
                    mr.CNOR_ADDRESS = Convert.ToString(ds.Tables[0].Rows[0]["CNOR_ADDRESS"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CNOR_ADDRESS"]);
                    mr.CNOR_GSTN = Convert.ToString(ds.Tables[0].Rows[0]["CNOR_GSTN"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CNOR_GSTN"]);
                    mr.CNEE_CODE = Convert.ToString(ds.Tables[0].Rows[0]["CNEE_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CNEE_CODE"]);
                    mr.CNEE_NAME = Convert.ToString(ds.Tables[0].Rows[0]["CNEE_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CNEE_NAME"]);
                    mr.CNEE_ADDRESS = Convert.ToString(ds.Tables[0].Rows[0]["CNEE_ADDRESS"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CNEE_ADDRESS"]);
                    mr.CNEE_GSTN = Convert.ToString(ds.Tables[0].Rows[0]["CNEE_GSTN"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CNEE_GSTN"]);

                    mr.OTHER_CODE = Convert.ToString(ds.Tables[0].Rows[0]["OTHER_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["OTHER_CODE"]);
                    mr.OTHER_NAME = Convert.ToString(ds.Tables[0].Rows[0]["OTHER_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["OTHER_NAME"]);
                    mr.OTHER_ADDRESS = Convert.ToString(ds.Tables[0].Rows[0]["OTHER_ADDRESS"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["OTHER_ADDRESS"]);
                    mr.OTHER_GSTN = Convert.ToString(ds.Tables[0].Rows[0]["PAYMENT_REC_FROM"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["OTHER_GSTN"]);

                    mr.CN_BILL_BR_CODE = Convert.ToString(ds.Tables[0].Rows[0]["CN_BILL_BR_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CN_BILL_BR_CODE"]);
                    mr.CN_BILL_NO = Convert.ToString(ds.Tables[0].Rows[0]["CN_BILL_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CN_BILL_NO"]);
                    mr.CN_BILL_DATE = Convert.ToString(ds.Tables[0].Rows[0]["CN_BILL_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CN_BILL_DATE"]);
                    mr.NO_OF_CN_BILL = Convert.ToInt32(ds.Tables[0].Rows[0]["NO_OF_CN_BILL"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["NO_OF_CN_BILL"]);

                    mr.HSN_CODE = Convert.ToString(ds.Tables[0].Rows[0]["HSN_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["HSN_CODE"]);
                    mr.NO_OF_PKG = Convert.ToInt32(ds.Tables[0].Rows[0]["NO_OF_PKG"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["NO_OF_PKG"]);
                    mr.CHARGE_WT = Convert.ToDecimal(ds.Tables[0].Rows[0]["CHARGE_WT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["CHARGE_WT"]);

                    mr.ARRIVAL_DATE = Convert.ToString(ds.Tables[0].Rows[0]["ARRIVAL_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["ARRIVAL_DATE"]);
                    mr.DELIVERY_DATE = Convert.ToString(ds.Tables[0].Rows[0]["DELIVERY_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["DELIVERY_DATE"]);
                    mr.TOTAL_DELAY_DEL_DAYS = Convert.ToInt32(ds.Tables[0].Rows[0]["TOTAL_DELAY_DEL_DAYS"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["TOTAL_DELAY_DEL_DAYS"]);
                    mr.FREE_DAYS_ALLOW = Convert.ToInt32(ds.Tables[0].Rows[0]["FREE_DAYS_ALLOW"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["FREE_DAYS_ALLOW"]);
                    mr.DC_CHARGABLE_DAYS = Convert.ToInt32(ds.Tables[0].Rows[0]["DC_CHARGABLE_DAYS"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["DC_CHARGABLE_DAYS"]);

                    mr.DCC_RATE_PER_KG = Convert.ToDecimal(ds.Tables[0].Rows[0]["DCC_RATE_PER_KG"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["DCC_RATE_PER_KG"]);
                    mr.TOTAL_DCC_CHARGABLE_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_DCC_CHARGABLE_AMT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["TOTAL_DCC_CHARGABLE_AMT"]);
                    mr.FURTHER_DCC_GIVEN_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["FURTHER_DCC_GIVEN_AMT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["FURTHER_DCC_GIVEN_AMT"]);
                    mr.NET_DCC_AMT_CHARGE = Convert.ToDecimal(ds.Tables[0].Rows[0]["NET_DCC_AMT_CHARGE"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["NET_DCC_AMT_CHARGE"]);

                    mr.RTGS_CHQ_AMOUNT = Convert.ToDecimal(ds.Tables[0].Rows[0]["RTGS_CHQ_AMOUNT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["RTGS_CHQ_AMOUNT"]);
                    mr.RTGS_CHQ_NO = Convert.ToString(ds.Tables[0].Rows[0]["RTGS_CHQ_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["RTGS_CHQ_NO"]);
                    mr.RTGS_CHQ_DATE = Convert.ToString(ds.Tables[0].Rows[0]["RTGS_CHQ_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["RTGS_CHQ_DATE"]);
                    mr.PAY_TYPE = Convert.ToString(ds.Tables[0].Rows[0]["PAY_TYPE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["PAY_TYPE"]);
                    mr.CASH_AMOUNT = Convert.ToDecimal(ds.Tables[0].Rows[0]["CASH_AMOUNT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["CASH_AMOUNT"]);
                    mr.BANK_NAME = Convert.ToString(ds.Tables[0].Rows[0]["BANK_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["BANK_NAME"]);
                    mr.AMOUNT_IN_WORD = Convert.ToString(ds.Tables[0].Rows[0]["AMOUNT_IN_WORD"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["AMOUNT_IN_WORD"]);

                    mr.MR_AMOUNT = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_AMOUNT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_AMOUNT"]);
                    mr.TDS_AMOUNT = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_TDS_AMOUNT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_TDS_AMOUNT"]);
                    mr.MR_TOTAL_AMOUNT = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_TOTAL_AMOUNT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_TOTAL_AMOUNT"]);

                    mr.EMP_CODE = Convert.ToString(ds.Tables[0].Rows[0]["EMP_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["EMP_CODE"]);

                    mr.TOTAL_FRT = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_FRT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["TOTAL_FRT"]);
                    mr.DELAY_COLL_CHARGE = Convert.ToDecimal(ds.Tables[0].Rows[0]["DELAY_COLL_CHARGE"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["DELAY_COLL_CHARGE"]);
                    mr.MATERIAL_HAND_CHARGE = Convert.ToDecimal(ds.Tables[0].Rows[0]["MATERIAL_HAND_CHARGE"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MATERIAL_HAND_CHARGE"]);
                    mr.DELIVERY_CHARGE = Convert.ToDecimal(ds.Tables[0].Rows[0]["DELIVERY_CHARGE"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["DELIVERY_CHARGE"]);
                    mr.MISC_CHARGE = Convert.ToDecimal(ds.Tables[0].Rows[0]["MISC_CHARGE"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MISC_CHARGE"]);
                    mr.OPR_MANG_CHARGE = Convert.ToDecimal(ds.Tables[0].Rows[0]["OPR_MANG_CHARGE"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["OPR_MANG_CHARGE"]);
                    mr.OTHER_CHARGE = Convert.ToDecimal(ds.Tables[0].Rows[0]["OTHER_CHARGE"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["OTHER_CHARGE"]);
                    mr.DEDUCTION = Convert.ToDecimal(ds.Tables[0].Rows[0]["DEDUCTION"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["DEDUCTION"]);
                    mr.SUB_TOTAL = Convert.ToDecimal(ds.Tables[0].Rows[0]["SUB_TOTAL"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["SUB_TOTAL"]);
                    mr.TOTAL = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["TOTAL"]);

                    mr.PRINT_COPY = Convert.ToInt32(ds.Tables[0].Rows[0]["PRINT_COPY"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["PRINT_COPY"]);

                    mr.MR_TYPE_CODE = Convert.ToString(ds.Tables[0].Rows[0]["MR_TYPE_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_TYPE_CODE"]);

                    mr.BILL_P_CODE = Convert.ToString(ds.Tables[0].Rows[0]["BILL_P_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["BILL_P_CODE"]);
                    mr.BILL_P_NAME = Convert.ToString(ds.Tables[0].Rows[0]["BILL_P_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["BILL_P_NAME"]);
                    mr.BILL_P_GSTN = Convert.ToString(ds.Tables[0].Rows[0]["BILL_P_GSTN"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["BILL_P_GSTN"]);

                    //---------------- QR Code Generation ----------------------------
                    CommonFunction cf = new CommonFunction();
                    string imageUrl = cf.QRCODE_Image(Convert.ToString(mr.MR_ID) + "/" + mr.MR_NO.Substring(0, 1) + "/" + Convert.ToString(mr.PRINT_COPY) + "/" + Convert.ToString(mr.BR_ID)); // 1 true copy
                    mr.MR_QR_CODE = imageUrl;

                    #endregion
                }
            }

            return mr;
        }

        public MR_SHEET MR_VIEW(decimal MR_ID, string MR_NO)
        {
            MR_SHEET mr = new MR_SHEET();
            //clsPrintSettings obj = new clsPrintSettings();
            SqlParameter[] param = { new SqlParameter("@MR_ID", MR_ID), new SqlParameter("@MR_NO", MR_NO) };
            DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iTMS].[USP_MR_PRINT_PAID]", CommandType.StoredProcedure, param);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    #region Basic Information-1


                    mr.MR_ID = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_ID"]);
                    mr.MR_NO = Convert.ToString(ds.Tables[0].Rows[0]["MR_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_NO"]);
                    mr.MR_DATE = Convert.ToString(ds.Tables[0].Rows[0]["MR_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_DATE"]);
                    mr.MR_TYPE = Convert.ToString(ds.Tables[0].Rows[0]["MR_TYPE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_TYPE"]);
                    mr.MR_TYPE_NAME = Convert.ToString(ds.Tables[0].Rows[0]["MR_TYPE_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_TYPE_NAME"]);
                    mr.BR_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["BR_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["BR_ID"]);
                    mr.BR_CODE = Convert.ToString(ds.Tables[0].Rows[0]["BR_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["BR_CODE"]);
                    mr.BR_NAME = Convert.ToString(ds.Tables[0].Rows[0]["BR_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["BR_NAME"]);
                    mr.BR_ADDRESS = Convert.ToString(ds.Tables[0].Rows[0]["BR_ADDRESS"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["BR_ADDRESS"]);
                    mr.BR_GSTN = Convert.ToString(ds.Tables[0].Rows[0]["BR_GSTN"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["BR_GSTN"]);
                    mr.BR_SAC = Convert.ToString(ds.Tables[0].Rows[0]["BR_SAC"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["BR_SAC"]);
                    mr.PAYMENT_REC_FROM = Convert.ToString(ds.Tables[0].Rows[0]["PAYMENT_REC_FROM"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["PAYMENT_REC_FROM"]);

                    mr.CNOR_CODE = Convert.ToString(ds.Tables[0].Rows[0]["CNOR_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CNOR_CODE"]);
                    mr.CNOR_NAME = Convert.ToString(ds.Tables[0].Rows[0]["CNOR_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CNOR_NAME"]);
                    mr.CNOR_ADDRESS = Convert.ToString(ds.Tables[0].Rows[0]["CNOR_ADDRESS"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CNOR_ADDRESS"]);
                    mr.CNOR_GSTN = Convert.ToString(ds.Tables[0].Rows[0]["CNOR_GSTN"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CNOR_GSTN"]);
                    mr.CNEE_CODE = Convert.ToString(ds.Tables[0].Rows[0]["CNEE_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CNEE_CODE"]);
                    mr.CNEE_NAME = Convert.ToString(ds.Tables[0].Rows[0]["CNEE_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CNEE_NAME"]);
                    mr.CNEE_ADDRESS = Convert.ToString(ds.Tables[0].Rows[0]["CNEE_ADDRESS"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CNEE_ADDRESS"]);
                    mr.CNEE_GSTN = Convert.ToString(ds.Tables[0].Rows[0]["CNEE_GSTN"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CNEE_GSTN"]);

                    mr.OTHER_CODE = Convert.ToString(ds.Tables[0].Rows[0]["OTHER_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["OTHER_CODE"]);
                    mr.OTHER_NAME = Convert.ToString(ds.Tables[0].Rows[0]["OTHER_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["OTHER_NAME"]);
                    mr.OTHER_ADDRESS = Convert.ToString(ds.Tables[0].Rows[0]["OTHER_ADDRESS"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["OTHER_ADDRESS"]);
                    mr.OTHER_GSTN = Convert.ToString(ds.Tables[0].Rows[0]["PAYMENT_REC_FROM"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["OTHER_GSTN"]);

                    mr.CN_BILL_BR_CODE = Convert.ToString(ds.Tables[0].Rows[0]["CN_BILL_BR_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CN_BILL_BR_CODE"]);
                    mr.CN_BILL_NO = Convert.ToString(ds.Tables[0].Rows[0]["CN_BILL_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CN_BILL_NO"]);
                    mr.CN_BILL_DATE = Convert.ToString(ds.Tables[0].Rows[0]["CN_BILL_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CN_BILL_DATE"]);
                    mr.NO_OF_CN_BILL = Convert.ToInt32(ds.Tables[0].Rows[0]["NO_OF_CN_BILL"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["NO_OF_CN_BILL"]);

                    mr.HSN_CODE = Convert.ToString(ds.Tables[0].Rows[0]["HSN_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["HSN_CODE"]);
                    mr.NO_OF_PKG = Convert.ToInt32(ds.Tables[0].Rows[0]["NO_OF_PKG"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["NO_OF_PKG"]);
                    mr.CHARGE_WT = Convert.ToDecimal(ds.Tables[0].Rows[0]["CHARGE_WT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["CHARGE_WT"]);

                    mr.ARRIVAL_DATE = Convert.ToString(ds.Tables[0].Rows[0]["ARRIVAL_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["ARRIVAL_DATE"]);
                    mr.DELIVERY_DATE = Convert.ToString(ds.Tables[0].Rows[0]["DELIVERY_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["DELIVERY_DATE"]);
                    mr.TOTAL_DELAY_DEL_DAYS = Convert.ToInt32(ds.Tables[0].Rows[0]["TOTAL_DELAY_DEL_DAYS"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["TOTAL_DELAY_DEL_DAYS"]);
                    mr.FREE_DAYS_ALLOW = Convert.ToInt32(ds.Tables[0].Rows[0]["FREE_DAYS_ALLOW"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["FREE_DAYS_ALLOW"]);
                    mr.DC_CHARGABLE_DAYS = Convert.ToInt32(ds.Tables[0].Rows[0]["DC_CHARGABLE_DAYS"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["DC_CHARGABLE_DAYS"]);

                    mr.DCC_RATE_PER_KG = Convert.ToDecimal(ds.Tables[0].Rows[0]["DCC_RATE_PER_KG"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["DCC_RATE_PER_KG"]);
                    mr.TOTAL_DCC_CHARGABLE_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_DCC_CHARGABLE_AMT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["TOTAL_DCC_CHARGABLE_AMT"]);
                    mr.FURTHER_DCC_GIVEN_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["FURTHER_DCC_GIVEN_AMT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["FURTHER_DCC_GIVEN_AMT"]);
                    mr.NET_DCC_AMT_CHARGE = Convert.ToDecimal(ds.Tables[0].Rows[0]["NET_DCC_AMT_CHARGE"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["NET_DCC_AMT_CHARGE"]);

                    mr.RTGS_CHQ_AMOUNT = Convert.ToDecimal(ds.Tables[0].Rows[0]["RTGS_CHQ_AMOUNT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["RTGS_CHQ_AMOUNT"]);
                    mr.RTGS_CHQ_NO = Convert.ToString(ds.Tables[0].Rows[0]["RTGS_CHQ_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["RTGS_CHQ_NO"]);
                    mr.RTGS_CHQ_DATE = Convert.ToString(ds.Tables[0].Rows[0]["RTGS_CHQ_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["RTGS_CHQ_DATE"]);
                    mr.PAY_TYPE = Convert.ToString(ds.Tables[0].Rows[0]["PAY_TYPE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["PAY_TYPE"]);
                    mr.CASH_AMOUNT = Convert.ToDecimal(ds.Tables[0].Rows[0]["CASH_AMOUNT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["CASH_AMOUNT"]);
                    mr.BANK_NAME = Convert.ToString(ds.Tables[0].Rows[0]["BANK_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["BANK_NAME"]);
                    mr.AMOUNT_IN_WORD = Convert.ToString(ds.Tables[0].Rows[0]["AMOUNT_IN_WORD"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["AMOUNT_IN_WORD"]);

                    mr.MR_AMOUNT = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_AMOUNT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_AMOUNT"]);
                    mr.TDS_AMOUNT = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_TDS_AMOUNT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_TDS_AMOUNT"]);
                    mr.MR_TOTAL_AMOUNT = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_TOTAL_AMOUNT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_TOTAL_AMOUNT"]);

                    mr.EMP_CODE = Convert.ToString(ds.Tables[0].Rows[0]["EMP_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["EMP_CODE"]);

                    mr.TOTAL_FRT = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL_FRT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["TOTAL_FRT"]);
                    mr.DELAY_COLL_CHARGE = Convert.ToDecimal(ds.Tables[0].Rows[0]["DELAY_COLL_CHARGE"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["DELAY_COLL_CHARGE"]);
                    mr.MATERIAL_HAND_CHARGE = Convert.ToDecimal(ds.Tables[0].Rows[0]["MATERIAL_HAND_CHARGE"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MATERIAL_HAND_CHARGE"]);
                    mr.DELIVERY_CHARGE = Convert.ToDecimal(ds.Tables[0].Rows[0]["DELIVERY_CHARGE"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["DELIVERY_CHARGE"]);
                    mr.MISC_CHARGE = Convert.ToDecimal(ds.Tables[0].Rows[0]["MISC_CHARGE"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MISC_CHARGE"]);
                    mr.OPR_MANG_CHARGE = Convert.ToDecimal(ds.Tables[0].Rows[0]["OPR_MANG_CHARGE"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["OPR_MANG_CHARGE"]);
                    mr.OTHER_CHARGE = Convert.ToDecimal(ds.Tables[0].Rows[0]["OTHER_CHARGE"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["OTHER_CHARGE"]);
                    mr.DEDUCTION = Convert.ToDecimal(ds.Tables[0].Rows[0]["DEDUCTION"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["DEDUCTION"]);
                    mr.SUB_TOTAL = Convert.ToDecimal(ds.Tables[0].Rows[0]["SUB_TOTAL"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["SUB_TOTAL"]);
                    mr.TOTAL = Convert.ToDecimal(ds.Tables[0].Rows[0]["TOTAL"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["TOTAL"]);

                    mr.PRINT_COPY = Convert.ToInt32(ds.Tables[0].Rows[0]["PRINT_COPY"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["PRINT_COPY"]);

                    //---------------- QR Code Generation ----------------------------
                    //CommonFunction cf = new CommonFunction();
                    //string imageUrl = cf.QRCODE_Image(Convert.ToString(mr.MR_ID) + "/" + mr.MR_NO.Substring(0, 1) + "/" + Convert.ToString(mr.PRINT_COPY) + "/" + Convert.ToString(mr.BR_ID)); // 1 true copy
                    //mr.MR_QR_CODE = imageUrl;

                    #endregion
                }
            }
            return mr;

        }

        public MR_SHEET GET_MR_INFO(string MR_NO)
        {
            SqlParameter[] param = { new SqlParameter("@MR_NO", MR_NO) };
            using (DataTable dt = new DataAccess(sqlConnection.GetConnectionString()).GetDataTable("[iTMS].[USP_GET_MR_INFO]", CommandType.StoredProcedure, param))
            {
                MR_SHEET mr = new MR_SHEET();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        mr.MR_ID = Convert.ToDecimal(row["MR_ID"] == DBNull.Value ? "0" : row["MR_ID"]);
                        mr.MR_NO_SEARCH = Convert.ToString(row["MR_NO"] == DBNull.Value ? "" : row["MR_NO"]);
                        mr.MR_DATE = Convert.ToString(row["MR_DATE"] == DBNull.Value ? "" : row["MR_DATE"]);
                        mr.MR_BR_ID = Convert.ToInt32(row["MR_BR_ID"] == DBNull.Value ? "0" : row["MR_BR_ID"]);
                        mr.MR_BR_NAME = Convert.ToString(row["MR_BR_NAME"] == DBNull.Value ? "" : row["MR_BR_NAME"]);
                        mr.MR_STATUS = Convert.ToBoolean(row["MR_STATUS"] == DBNull.Value ? 0 : row["MR_STATUS"]);
                        mr.IS_MR_PRINT = Convert.ToInt32(row["IS_MR_PRINT"] == DBNull.Value ? "0" : row["IS_MR_PRINT"]);
                        mr.MR_TYPE = Convert.ToString(row["MR_TYPE"] == DBNull.Value ? "0" : row["MR_TYPE"]);
                        mr.MR_IS_ADJ = Convert.ToBoolean(row["MR_IS_ADJ"] == DBNull.Value ? 0 : row["MR_IS_ADJ"]);
                    }
                }

                return mr;
            }
        }

        #region JSON
        // Added by Pramesh kumar Vishwakarma, Date:10-09-2020
        public int SELECT_CBS_PERIOD(int brId)
        {
            SqlParameter[] param = { new SqlParameter("@BR_ID", brId) };
            using (DataTable dt = new DataAccess(sqlConnection.GetConnectionString()).GetDataTable("[iFMS].[USP_SELECT_CBS_PERIOD]", CommandType.StoredProcedure, param))
            {
                int cbsPeriod = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    cbsPeriod = Convert.ToInt32(dt.Rows[0]["CBS_SRP_PD"]);
                }
                return cbsPeriod;
            }
        }

        public object SELECT_LAST_CBS_DATE(int brId)
        {
            SqlParameter[] param = { new SqlParameter("@BR_ID", brId) };
            using (DataTable dt = new DataAccess(sqlConnection.GetConnectionString()).GetDataTable("[iFMS].[USP_SELECT_LAST_CBS_DATE]", CommandType.StoredProcedure, param))
            {
                string LastCbsDate = "";
                string fromCbsDate = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    LastCbsDate = Convert.ToString(dt.Rows[0]["CBS_DATE"]);
                    fromCbsDate = Convert.ToString(dt.Rows[0]["FROM_CBS_DATE"]);//25-05-2022,pramesh
                }
                return new { LastCbsDate, fromCbsDate };
            }
        }

        public CBS_BR_DTLS GET_CBS_STN(int brId)
        {
            SqlParameter[] param = { new SqlParameter("@BR_ID", brId) };
            using (DataTable dt = new DataAccess(sqlConnection.GetConnectionString()).GetDataTable("[iFMS].[USP_GET_CBS_STN]", CommandType.StoredProcedure, param))
            {
                CBS_BR_DTLS _cbsstn = new CBS_BR_DTLS();
                if (dt != null && dt.Rows.Count > 0)
                {
                    _cbsstn.BR_ID = Convert.ToInt32(dt.Rows[0]["BR_ID"]);
                    _cbsstn.BR_CODE = Convert.ToString(dt.Rows[0]["BR_CODE"]);
                    _cbsstn.BR_NAME = Convert.ToString(dt.Rows[0]["BR_NAME"]);
                }
                return _cbsstn;
            }
        }

        // Added By - Sunil Kumar Singh Date: 18/11/2020
        public object SELECT_LAST_MR_CBS_DATE(int brId)
        {
            SqlParameter[] param = { new SqlParameter("@BR_ID", brId) };
            using (DataTable dt = new DataAccess(sqlConnection.GetConnectionString()).GetDataTable("[iFMS].[USP_SELECT_LAST_MR_CBS_DATE]", CommandType.StoredProcedure, param))
            {
                string LastCbsDateEnter = "";
                string fromCbsDate = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    LastCbsDateEnter = Convert.ToString(dt.Rows[0]["MR_CBS_DATE"]);
                    fromCbsDate = Convert.ToString(dt.Rows[0]["FROM_CBS_DATE"]);//25-05-2022,pramesh
                }
                return new { LastCbsDate = LastCbsDateEnter, fromCbsDate };
            }
        }
        // Added By - SUnil Kumar Singh Date: 18/11/2020

        public List<ddlClass> SELECT_CNS_LIST_BY_BOOKING_BR_ID(int brId, string DATE, int? partyId, string searchtext)
        {
            SqlParameter[] param = { new SqlParameter("@BR_ID", brId), new SqlParameter("@Date", DATE), new SqlParameter("@BILL_PARTY_ID", partyId), new SqlParameter("@CN_NO", searchtext) };
            using (DataTable dt = new DataAccess(sqlConnection.GetConnectionString()).GetDataTable("[iTMS].[USP_SELECT_CN_LIST_FOR_MR_BY_BOOKING_BR_ID]", CommandType.StoredProcedure, param))
            {
                List<ddlClass> cnList = new List<ddlClass>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        cnList.Add(new ddlClass
                        {
                            ddlValue = Convert.ToString(row["CN_ID"] == DBNull.Value ? "" : row["CN_ID"]),
                            ddlText = Convert.ToString(row["CN_NO"] == DBNull.Value ? "" : row["CN_NO"])
                        });
                    }
                }
                return cnList;
            }
        }

        public List<MR_CNS_BFD> SELECT_BILL_CNS_REF_DTL_FOR_MR_BFD(decimal billId)
        {
            SqlParameter[] param = { new SqlParameter("@BILL_ID", billId) };

            DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iTMS].[USP_SELECT_BILL_CNS_REF_DTL_FOR_MR_BFD]", CommandType.StoredProcedure, param);

            List<MR_CNS_BFD> _list = new List<MR_CNS_BFD>();
            DataTable dt = ds.Tables[0];
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    _list.Add(new MR_CNS_BFD
                    {
                        CN_ID = Convert.ToDecimal(row["CN_ID"]),
                        CN_NO = Convert.ToString(row["CN_NO"]),
                    });
                }
            }

            return _list;
        }

        // Added: Sunil Kumar Singh Date: 21/12/2020
        public CN_BILL_IDS SELECT_CN_ID(int brId, string cnno)
        {
            CN_BILL_IDS cns = new CN_BILL_IDS();
            try
            {
                SqlParameter[] param = { new SqlParameter("@BR_ID", brId), new SqlParameter("@CNNO", cnno) };
                DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iTMS].[USP_SELECT_CN_ID]", CommandType.StoredProcedure, param);
                if (ds != null)
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        cns.CNs_ID = Convert.ToDecimal(ds.Tables[0].Rows[0]["CN_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["CN_ID"]);
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            return cns;
        }

        public CN_BILL_IDS SELECT_BILL_ID(int brId, string billno)
        {
            CN_BILL_IDS bills = new CN_BILL_IDS();
            try
            {
                SqlParameter[] param = { new SqlParameter("@BR_ID", brId), new SqlParameter("@BILLNO", billno) };
                DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iTMS].[USP_SELECT_BILL_ID]", CommandType.StoredProcedure, param);
                if (ds != null)
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        bills.BILL_ID = Convert.ToDecimal(ds.Tables[0].Rows[0]["BILL_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["BILL_ID"]);
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            return bills;
        }

        // Added: Sunil Kumar Singh Date: 21/12/2020

        //23-07-2021
        public int SELECT_POS_ALLOW(int brId)
        {
            int allow = 0;
            try
            {
                SqlParameter[] param = { new SqlParameter("@BR_ID", brId) };
                DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iTMS].[USP_SELECT_POS_ALLOW]", CommandType.StoredProcedure, param);
                if (ds != null)
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        allow = Convert.ToInt32(ds.Tables[0].Rows[0]["BR_POS_ALLOW"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["BR_POS_ALLOW"]);
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            return allow;
        }

        public CN_OR_BILL_DTL SELECT_CN_DETAILS_BY_CN_ID(decimal cnId, decimal paId, int mrBrId, string mrdate)
        {
            CN_OR_BILL_DTL cn = new CN_OR_BILL_DTL();
            try
            {
                SqlParameter[] param = { new SqlParameter("@CN_ID", cnId), new SqlParameter("@PA_ID", paId), new SqlParameter("@MR_BR_ID", mrBrId), new SqlParameter("@MR_DATE", mrdate) };
                DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iTMS].[USP_SELECT_CN_DETAILS_FOR_MR_BY_CN_ID]", CommandType.StoredProcedure, param);
                if (ds != null)
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        cn.CN_OR_BILL_ID = Convert.ToDecimal(ds.Tables[0].Rows[0]["CN_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["CN_ID"]);
                        cn.CN_OR_BILL_NO = Convert.ToString(ds.Tables[0].Rows[0]["CN_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CN_NO"]);
                        if (ds.Tables[0].Rows[0]["CN_DATE"] != DBNull.Value)
                        {
                            cn.CN_OR_BILL_DATE = Convert.ToDateTime(ds.Tables[0].Rows[0]["CN_DATE"]);
                        }
                        cn.CN_OR_BILL_FRT_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["FRT_AMT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["FRT_AMT"]);
                        cn.CN_BTYPE_NM = Convert.ToString(ds.Tables[0].Rows[0]["CN_BTYPE_NM"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CN_BTYPE_NM"]);
                        cn.CN_BTYPE_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["CN_BTYPE_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["CN_BTYPE_ID"]);
                        cn.CN_DESTINATION_BR_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["CN_DESTINATION_BR_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["CN_DESTINATION_BR_ID"]);
                        cn.DPR_BR_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["DPR_BR_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["DPR_BR_ID"]);
                        cn.CN_BILL_P_CODE = Convert.ToString(ds.Tables[0].Rows[0]["CN_BILL_P_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CN_BILL_P_CODE"]);

                        cn.CN_FRT_MR_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["CN_FRT_MR_AMT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["CN_FRT_MR_AMT"]);
                        cn.CN_BILL_RAISED = Convert.ToInt32(ds.Tables[0].Rows[0]["CN_BILL_RAISED"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["CN_BILL_RAISED"]);
                        cn.CN_IS_FRT_PENDING = Convert.ToInt32(ds.Tables[0].Rows[0]["CN_IS_FRT_PENDING"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["CN_IS_FRT_PENDING"]);
                        cn.CN_DEST_BR_STOCK = Convert.ToInt32(ds.Tables[0].Rows[0]["CN_DEST_BR_STOCK"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["CN_DEST_BR_STOCK"]);
                        cn.CN_BILL_ALLOW = Convert.ToInt32(ds.Tables[0].Rows[0]["CN_ALLOW"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["CN_ALLOW"]);

                        cn.MR_ACC_BR_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["MR_ACC_BR_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_ACC_BR_ID"]);
                        cn.CN_DESTINATION_ACC_BR_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["CN_DESTINATION_ACC_BR_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["CN_DESTINATION_ACC_BR_ID"]);

                        cn.P_CVGRP_CODE = Convert.ToString(ds.Tables[0].Rows[0]["P_CVGRP_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["P_CVGRP_CODE"]);
                        cn.BILL_GSTIN = Convert.ToString(ds.Tables[0].Rows[0]["BILL_GSTIN"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["BILL_GSTIN"]);
                        cn.CNEE_GSTIN = Convert.ToString(ds.Tables[0].Rows[0]["CNEE_GSTIN"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CNEE_GSTIN"]);
                        cn.CNOR_GSTIN = Convert.ToString(ds.Tables[0].Rows[0]["CNOR_GSTIN"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CNOR_GSTIN"]);
                        cn.CNEE_NAME = Convert.ToString(ds.Tables[0].Rows[0]["CNEE_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CNEE_NAME"]).Trim();

                        cn.MR_OCT_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["OCT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["OCT"]);
                        cn.MR_OCS_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["OMC"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["OMC"]);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return cn;
        }

        public List<ddlClass> SELECT_BILL_LIST_BY_BR_ID(int brId, string DATE, int? partyId, string searchtext)
        {
            SqlParameter[] param = { new SqlParameter("@BR_ID", brId), new SqlParameter("@Date", DATE), new SqlParameter("@BILL_PARTY_ID", partyId), new SqlParameter("@BILL_NO", searchtext) };
            using (DataTable dt = new DataAccess(sqlConnection.GetConnectionString()).GetDataTable("[iTMS].[USP_SELECT_BILL_LIST_FOR_MR_BY_BR_ID]", CommandType.StoredProcedure, param))
            {
                List<ddlClass> cnList = new List<ddlClass>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        cnList.Add(new ddlClass
                        {
                            ddlValue = Convert.ToString(row["BILL_ID"] == DBNull.Value ? "" : row["BILL_ID"]),
                            ddlText = Convert.ToString(row["BILL_NO"] == DBNull.Value ? "" : row["BILL_NO"])
                        });
                    }
                }
                return cnList;
            }
        }

        public CN_OR_BILL_DTL SELECT_BILL_DETAILS_BY_BILL_ID(decimal billId, string mrdate)
        {
            CN_OR_BILL_DTL cn = new CN_OR_BILL_DTL();
            try
            {
                SqlParameter[] param = { new SqlParameter("@BILL_ID", billId), new SqlParameter("@MR_DATE", mrdate) };
                DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iTMS].[USP_SELECT_BILL_DETAILS_FOR_MR_BY_BILL_ID]", CommandType.StoredProcedure, param);
                if (ds != null)
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        cn.CN_OR_BILL_ID = Convert.ToDecimal(ds.Tables[0].Rows[0]["BILL_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["BILL_ID"]);
                        cn.CN_OR_BILL_NO = Convert.ToString(ds.Tables[0].Rows[0]["BILL_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["BILL_NO"]);
                        if (ds.Tables[0].Rows[0]["BILL_DATE"] != DBNull.Value)
                        {
                            cn.CN_OR_BILL_DATE = Convert.ToDateTime(ds.Tables[0].Rows[0]["BILL_DATE"]);
                        }

                        cn.CN_OR_BILL_FRT_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["FRT_AMT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["FRT_AMT"]);

                        cn.MR_DEMM_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["DCC"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["DCC"]);
                        cn.MR_CPE_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["CPE"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["CPE"]);
                        cn.MR_OCT_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["OCT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["OCT"]);
                        cn.MR_OCS_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["OCS"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["OCS"]);
                        cn.MR_OTH_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["OTH"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["OTH"]);
                        cn.MR_MISC_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["MISC"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MISC"]);

                        cn.CN_BILL_P_CODE = Convert.ToString(ds.Tables[0].Rows[0]["BILL_P_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["BILL_P_CODE"]);

                        cn.BILL_MR_PREPARED = Convert.ToInt32(ds.Tables[0].Rows[0]["BILL_MR_PREPARED"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["BILL_MR_PREPARED"]);
                        cn.CN_BILL_ALLOW = Convert.ToInt32(ds.Tables[0].Rows[0]["BILL_ALLOW"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["BILL_ALLOW"]);
                        cn.BILL_SUBMIT = Convert.ToInt32(ds.Tables[0].Rows[0]["BILL_SUBMIT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["BILL_SUBMIT"]);

                        cn.P_CVGRP_CODE = Convert.ToString(ds.Tables[0].Rows[0]["P_CVGRP_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["P_CVGRP_CODE"]);
                        cn.BILL_GSTIN = Convert.ToString(ds.Tables[0].Rows[0]["BILL_GSTIN"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["BILL_GSTIN"]);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return cn;
        }

        public MR_Receipt GET_LAST_ENTRY_RECORD(int brId)
        {
            SqlParameter[] param = { new SqlParameter("@BR_ID", brId) };
            MR_Receipt mr = new MR_Receipt();
            using (DataTable dt = new DataAccess(sqlConnection.GetConnectionString()).GetDataTable("[iTMS].[USP_GET_MR_LAST_RECORD]", CommandType.StoredProcedure, param))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    mr.MR_MANUAL_NO = Convert.ToString(dt.Rows[0]["MR_MANUAL_NO"]);
                    if (dt.Rows[0]["MR_DATE"] != DBNull.Value)
                    {
                        mr.MR_DATE = Convert.ToDateTime(dt.Rows[0]["MR_DATE"]);
                    }
                    mr.LSTDATA = Convert.ToString(dt.Rows[0]["LSTDATA"]);
                }
            }
            return mr;
        }


        // Get MR Branch GST No - Sunil Kumar Singh Date:12/01/2022

        public MR_BR_GST GetMRStnGstRecord(int brId)
        {
            SqlParameter[] param = { new SqlParameter("@BR_ID", brId) };
            MR_BR_GST gst = new MR_BR_GST();
            using (DataTable dt = new DataAccess(sqlConnection.GetConnectionString()).GetDataTable("[iTMS].[USP_GET_BR_GSTNO]", CommandType.StoredProcedure, param))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    gst.GST_NO = Convert.ToString(dt.Rows[0]["STATE_ARC_GST"]);
                }
            }
            return gst;
        }

        // END

        public MR_CHECKING CHECK_EXISTING_MR_NO(string mrNo, int mrbrid)
        {
            SqlConnection connection = null;
            MR_CHECKING check = new MR_CHECKING();
            try
            {
                SqlParameter[] param = { new SqlParameter("@MR_MANUAL_NO", mrNo), new SqlParameter("@BR_ID", mrbrid) };
                using (IDataReader reader = new DataAccess(sqlConnection.GetConnectionString()).GetDataReader("[iTMS].[USP_CHECK_MR_EXISTANCE]", CommandType.StoredProcedure, param, out connection))
                {
                    while (reader.Read())
                    {
                        check.IsExist = Convert.ToBoolean(reader[0]);
                        check.IsAvailableInDCC = Convert.ToBoolean(reader[1]);
                        check.DCR_ID = Convert.ToDecimal(reader[2]);
                    }
                    reader.Close();
                }
            }
            catch (Exception)
            {
                connection.Close();
            }
            return check;
        }

        public List<MR_EMD_DATA> SELECT_MR_EMD_LIST(int brId, decimal paId, string cbsdate)
        {
            SqlParameter[] param = {
                                       new SqlParameter("@EMD_BR_ID", brId),
                                       new SqlParameter("@PARTY_ADD_ID", paId),
                                       new SqlParameter("@CBS_DATE", cbsdate),
                                   };

            DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iFMS].[USP_SELECT_MR_EMD_LIST]", CommandType.StoredProcedure, param);

            List<MR_EMD_DATA> _list = new List<MR_EMD_DATA>();
            DataTable dt = ds.Tables[0];
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Nullable<DateTime> emdDate = null;
                    if (row["EMD_DATE"] != DBNull.Value)
                    {
                        emdDate = Convert.ToDateTime(row["EMD_DATE"]);
                    }

                    _list.Add(new MR_EMD_DATA
                    {
                        EMD_ID = Convert.ToDecimal(row["EMD_ID"]),
                        EMD_NO = Convert.ToString(row["EMD_NO"]),
                        EMD_TENDER_NO = Convert.ToString(row["EMD_TENDER_NO"]),

                        EMD_BR_ID = Convert.ToInt32(row["EMD_BR_ID"]),
                        EMD_BR_NAME = Convert.ToString(row["EMD_BR_NAME"]),

                        EMD_DATE = emdDate,
                        EMD_DATE1 = Convert.ToString(row["EMD_DATE1"]),

                        EMD_P_CODE = Convert.ToString(row["EMD_P_CODE"]),
                        EMD_P_NAME = Convert.ToString(row["EMD_P_NAME"]),
                        EMD_PA_ID = Convert.ToDecimal(row["EMD_PA_ID"]),

                        EMD_AMT = Convert.ToDecimal(row["EMD_AMT"]),
                        EMD_BAL_AMT = Convert.ToDecimal(row["EMD_BAL_AMT"]),
                        EMD_CBS_DATE = Convert.ToString(row["EMD_CBS_DATE"]),
                        EMD_DUE_DATE = Convert.ToString(row["EMD_DUE_DATE"]),
                    });
                }
            }

            return _list;
        }

        public List<ddlClass> SELECT_MR_CREDIT_ADVICE_LIST(decimal? paId)
        {
            SqlParameter[] param = { new SqlParameter("@PARTY_ADD_ID", paId) };
            using (DataTable dt = new DataAccess(sqlConnection.GetConnectionString()).GetDataTable("[iFMS].[USP_SELECT_MR_CREDIT_ADVICE_LIST]", CommandType.StoredProcedure, param))
            {
                List<ddlClass> cnList = new List<ddlClass>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        cnList.Add(new ddlClass
                        {
                            ddlValue = Convert.ToString(row["CRA_ID"] == DBNull.Value ? "" : row["CRA_ID"]),
                            ddlText = Convert.ToString(row["CRA_NO"] == DBNull.Value ? "" : row["CRA_NO"])
                        });
                    }
                }
                return cnList;
            }
        }

        public MR_CREDIT_ADVICE_DTLS SELECT_MR_CREDIT_ADVICE_DTLS(decimal craId)
        {
            SqlConnection connection = null;
            MR_CREDIT_ADVICE_DTLS cra = new MR_CREDIT_ADVICE_DTLS();
            try
            {
                SqlParameter[] param = { new SqlParameter("@CRA_ID", craId) };
                using (DataTable dt = new DataAccess(sqlConnection.GetConnectionString()).GetDataTable("[iFMS].[USP_SELECT_MR_CREDIT_ADVICE_LIST]", CommandType.StoredProcedure, param))
                {
                    if (dt.Rows.Count > 0)
                    {
                        cra.CRA_ID = Convert.ToDecimal(dt.Rows[0]["CRA_ID"]);
                        cra.CRA_AMT = Convert.ToDecimal(dt.Rows[0]["CRA_AMT"]);
                        cra.CRA_DATE = Convert.ToDateTime(dt.Rows[0]["CRA_DATE"]);
                    }
                }
            }
            catch (Exception)
            {
                connection.Close();
            }
            return cra;
        }
        #endregion

        #region MR Preparation


        public string INSERT_MR(MR_Receipt mr)
        {
            string errorMsg = "";

            decimal MRBFD_CLAM_AMT = 0;
            decimal MRBFD_RECO_AMT = 0;
            decimal MRBFD_NREC_AMT = 0;
            decimal MRBFD_EMD_AMT = 0;
            decimal MRBFD_SD_AMT = 0;
            decimal MR_TDS_ON_AMT = 0;
            decimal MR_TDS_AMT = 0;

            decimal voucher_Id = 0;
            string voucher_No = "";
            string debit_voucher_No = string.Empty;

            decimal MR_ID = 0;

            using (var connection = new SqlConnection(sqlConnection.GetConnectionString()))
            {
                connection.Open();
                SqlCommand command;
                SqlTransaction transactionScope = null;
                transactionScope = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    //IBT-Advice
                    decimal CRA_ID = 0;

                    if (mr.IBT_TYPE_ID == true && (mr.MRIBT_CRA_NO ?? "") != "")
                    {
                        SqlParameter[] param_ibt =
                        { 
                          new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                         ,new SqlParameter("@CRA_ID", SqlDbType.Decimal) 
                         ,new SqlParameter("@CRA_SYS_NO", SqlDbType.VarChar, 12)
                         ,new SqlParameter("@CRA_NO", string.IsNullOrEmpty(mr.MRIBT_CRA_NO)?(object)DBNull.Value : mr.MRIBT_CRA_NO)
                         ,new SqlParameter("@CRA_F_BR_ID",(mr.MR_BR_ID == null) ? (object)DBNull.Value : mr.MR_BR_ID) 
                         ,new SqlParameter("@CRA_T_BR_ID",(mr.MR_IBT_BR_ID == null) ? (object)DBNull.Value : mr.MR_IBT_BR_ID) 
                         ,new SqlParameter("@CRA_DATE",( mr.MR_DATE == null) ? (object)DBNull.Value : mr.MR_DATE) 
                         ,new SqlParameter("@CRA_PA_ID", ( mr.MR_PA_ID == null) ? (object)DBNull.Value : mr.MR_PA_ID) 
                         ,new SqlParameter("@CRA_AMT", ( mr.MR_TOTAL_AMT  == null) ? (object)DBNull.Value : mr.MR_TOTAL_AMT ) 
                         ,new SqlParameter("@CRA_MODE", ( mr.MR_PAY_MODE  == null) ? (object)DBNull.Value : mr.MR_PAY_MODE ) 
                         ,new SqlParameter("@CRA_REC_FROM", string.IsNullOrEmpty( mr.MR_P_NAME)?(object)DBNull.Value : mr.MR_P_NAME)   
                         ,new SqlParameter("@CRA_DESC", ( mr.MR_REMARKS  == null) ? (object)DBNull.Value : mr.MR_REMARKS )   
                         ,new SqlParameter("@CRA_GEN_FROM", 1) // 1 = MR MODULE 2 = CHEQUE RETURN 3 = IBT
                         ,new SqlParameter("@CRA_TYPE", 2)  // 1 = FUND TRANSFER 2 = FREIGHT 3 = LOCAL PETRO CARD 4 = OTHERS
                         ,new SqlParameter("@CRA_ADD_BY_TYPE",( mr.MR_USER_TYPE  == null) ? (object)DBNull.Value : mr.MR_USER_TYPE ) 
                         ,new SqlParameter("@CRA_ADD_BY",( mr.MR_ADD_BY  == null) ? (object)DBNull.Value : mr.MR_ADD_BY ) 

                        ,new SqlParameter("@CBS_BR_ID",( mr.MR_CBS_BR_ID  == null) ? (object)DBNull.Value : mr.MR_CBS_BR_ID)  
                        ,new SqlParameter("@CBS_DATE", (mr.MR_CBS_DATE == null) ? (object)DBNull.Value :  mr.MR_CBS_DATE )
                        ,new SqlParameter("@FROM_CBS_DATE", (mr.FROM_CBS_DATE == null) ? (object)DBNull.Value :  mr.FROM_CBS_DATE)
                        ,new SqlParameter("@CBS_OPN_DATE", (mr.CBS_OPN_DATE == null) ? (object)DBNull.Value :mr.CBS_OPN_DATE)
                        ,new SqlParameter("@CBS_DAYS", (mr.CBS_SRP_PD == null) ? (object)DBNull.Value : mr.CBS_SRP_PD)
                     
                        };

                        param_ibt[0].Direction = ParameterDirection.Output;
                        param_ibt[1].Direction = ParameterDirection.Output;
                        param_ibt[2].Direction = ParameterDirection.Output;
                        new DataAccess().InsertWithTransaction("[iFMS].[USP_INSERT_CREDIT_ADVICE]", CommandType.StoredProcedure, out command, connection, transactionScope, param_ibt);
                        CRA_ID = (decimal)command.Parameters["@CRA_ID"].Value;

                        string error_ibt = (string)command.Parameters["@ERRORSTR"].Value;

                        if (CRA_ID == -1) { errorMsg = error_ibt; }

                        /*
                        if (CRA_ID > 0)
                        {
                            // Voucher_Posting postingParam = new Voucher_Posting();

                            //Posting Begin
                            // FILL_RETURN_MR_POSTING_DEAILS(ref postingParam, mr); 

                            Finance fn = new Finance();
                            fn.INSERT_VOUCHER_REF_NO(CommandType.StoredProcedure, out command, connection, transactionScope,
                              Convert.ToInt32(mr.MR_BR_ID), 2, 2, Convert.ToDateTime(mr.MR_DATE), mr.MR_REMARKS, "CRAD", Convert.ToDecimal(mr.MR_ADD_BY), out voucher_Id, out debit_voucher_No, out errorMsg);

                            SqlParameter[] param_ibt_status =
                            { 
                              new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                             ,new SqlParameter("@CRAS_ID", SqlDbType.Decimal) 
                             ,new SqlParameter("@CRAS_CRA_ID", CRA_ID) 
                             ,new SqlParameter("@CRAS_STATUS" ,1)
                             ,new SqlParameter("@CRAS_DATE" ,DateTime.Now.Date)  
                             ,new SqlParameter("@CRAS_CBS_BR_ID",( mr.MR_CBS_BR_ID  == null) ? (object)DBNull.Value : mr.MR_CBS_BR_ID )    
                             ,new SqlParameter("@CRAS_CBS_DATE",( mr.MR_CBS_DATE  == null) ? (object)DBNull.Value : mr.MR_CBS_DATE ) 
                             ,new SqlParameter("@CRAS_REMARKS","APPROVED,THROUGH MR. " + Convert.ToString(mr.MR_MANUAL_NO))
                             ,new SqlParameter("@CRAS_V_ID",voucher_Id) 
                             ,new SqlParameter("@CRAS_ADD_BY_TYPE",( mr.MR_USER_TYPE  == null) ? (object)DBNull.Value : mr.MR_USER_TYPE ) 
                             ,new SqlParameter("@CRAS_ADD_BY",( mr.MR_ADD_BY  == null) ? (object)DBNull.Value : mr.MR_ADD_BY )
                            };

                            param_ibt_status[0].Direction = ParameterDirection.Output;
                            param_ibt_status[1].Direction = ParameterDirection.Output;
                            new DataAccess().InsertWithTransaction("[iFMS].[USP_INSERT_CREDIT_ADVICE_STATUS]", CommandType.StoredProcedure, out command, connection, transactionScope, param_ibt_status);
                            decimal CRAS_ID = (decimal)command.Parameters["@CRAS_ID"].Value;
                            string error_ibt_status = (string)command.Parameters["@ERRORSTR"].Value;

                            if (CRAS_ID == -1) { errorMsg = error_ibt_status; }
                        } */
                    }
                    if (errorMsg == "")
                    {
                        SqlParameter[] param =
                        {
                            new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                            ,new SqlParameter("@MR_ID", SqlDbType.Decimal) 
                            ,new SqlParameter("@MR_NO", SqlDbType.VarChar, 15)
                            ,new SqlParameter("@MR_MANUAL_NO",(mr.MR_MANUAL_NO == null) ? (object)DBNull.Value : mr.MR_MANUAL_NO) 
                            ,new SqlParameter("@MR_BR_ID", (mr.MR_BR_ID == null) ? (object)DBNull.Value : mr.MR_BR_ID) 
                            ,new SqlParameter("@MR_P_ID",( mr.MR_P_ID == null) ? (object)DBNull.Value : mr.MR_P_ID) 
                            ,new SqlParameter("@MR_PA_ID",( mr.MR_PA_ID == null) ? (object)DBNull.Value : mr.MR_PA_ID) 
                            ,new SqlParameter("@MR_P_PAN",( mr.PAN_NO == null) ? (object)DBNull.Value : mr.PAN_NO)  
                            ,new SqlParameter("@MR_P_GSTNO",( mr.MR_P_GSTNO == null) ? (object)DBNull.Value : mr.MR_P_GSTNO)  
                            ,new SqlParameter("@MR_TYPE",( mr.MR_TYPE == null) ? (object)DBNull.Value : mr.MR_TYPE) 
                            ,new SqlParameter("@MR_DOC_TYPE",( mr.MR_DOC_TYPE == null) ? (object)DBNull.Value : mr.MR_DOC_TYPE) 
                            ,new SqlParameter("@MR_DATE",( mr.MR_DATE == null) ? (object)DBNull.Value : mr.MR_DATE) 
                            ,new SqlParameter("@MR_SUFFIX",( mr.MR_SUFFIX == null) ? (object)DBNull.Value : mr.MR_SUFFIX) 
                            ,new SqlParameter("@MR_FRT_AMT",( mr.TOTAL_FRT_II == null) ? (object)DBNull.Value : mr.TOTAL_FRT_II) 
                            ,new SqlParameter("@MR_DEMM_AMT",( mr.MR_DEMM_AMT == null) ? (object)DBNull.Value : mr.MR_DEMM_AMT) 
                            ,new SqlParameter("@MR_HNDL_AMT",( mr.MR_HNDL_AMT == null) ? (object)DBNull.Value : mr.MR_HNDL_AMT) 
                            ,new SqlParameter("@MR_OCT_AMT",( mr.MR_OCT_AMT == null) ? (object)DBNull.Value : mr.MR_OCT_AMT) 
                            ,new SqlParameter("@MR_OCS_AMT",( mr.MR_OCS_AMT == null) ? (object)DBNull.Value : mr.MR_OCS_AMT)  
                            ,new SqlParameter("@MR_DLVCH_AMT",( mr.MR_DLVCH_AMT == null) ? (object)DBNull.Value : mr.MR_DLVCH_AMT) 
                            ,new SqlParameter("@MR_MISC_AMT",( mr.MR_MISC_AMT == null) ? (object)DBNull.Value : mr.MR_MISC_AMT) 
                            ,new SqlParameter("@MR_OTH_AMT",( mr.MR_OTH_AMT == null) ? (object)DBNull.Value : mr.MR_OTH_AMT) 
                            ,new SqlParameter("@MR_OPMC_AMT",( mr.MR_OPMC_AMT == null) ? (object)DBNull.Value : mr.MR_OPMC_AMT)  
                            ,new SqlParameter("@MR_GTX_AMT",( mr.MR_GTX_AMT == null) ? (object)DBNull.Value : mr.MR_GTX_AMT) 
                            ,new SqlParameter("@MR_CPE_AMT",( mr.MR_CPE_AMT  == null) ? (object)DBNull.Value : mr.MR_CPE_AMT ) 
                            ,new SqlParameter("@MR_GDN_CHRG_AMT",( mr.MR_GDN_CHRG_AMT  == null) ? (object)DBNull.Value : mr.MR_GDN_CHRG_AMT ) 
                            ,new SqlParameter("@MR_SUB_TOTAL_AMT",( mr.MR_SUB_TOTAL_AMT  == null) ? (object)DBNull.Value : mr.MR_SUB_TOTAL_AMT ) 
                            ,new SqlParameter("@MR_BFD_AMT",( mr.MR_BFD_AMT  == null) ? (object)DBNull.Value : mr.MR_BFD_AMT ) 
                            ,new SqlParameter("@MR_TOTAL_AMT",( mr.MR_TOTAL_AMT  == null) ? (object)DBNull.Value : mr.MR_TOTAL_AMT ) 
                            ,new SqlParameter("@MR_CN_AMT_EXTRA",( mr.MR_CN_AMT_EXTRA  == null) ? (object)DBNull.Value : mr.MR_CN_AMT_EXTRA ) 
                            ,new SqlParameter("@MR_PAY_MODE",( mr.MR_PAY_MODE  == null) ? (object)DBNull.Value : mr.MR_PAY_MODE ) 
                            ,new SqlParameter("@MR_CBS_DATE",( mr.MR_CBS_DATE  == null) ? (object)DBNull.Value : mr.MR_CBS_DATE ) 
                            ,new SqlParameter("@MR_CBS_BR_ID",( mr.MR_CBS_BR_ID  == null) ? (object)DBNull.Value : mr.MR_CBS_BR_ID ) 
                            ,new SqlParameter("@MR_OLD_ID",( mr.MR_OLD_ID  == null) ? (object)DBNull.Value : mr.MR_OLD_ID ) 
                            ,new SqlParameter("@MR_DCR_ID",( mr.MR_DCR_ID  == null) ? (object)DBNull.Value : mr.MR_DCR_ID ) 
                            ,new SqlParameter("@MR_DPR_STATUS",( mr.MR_DPR_STATUS  == null) ? (object)DBNull.Value : mr.MR_DPR_STATUS ) 
                            ,new SqlParameter("@MR_DESC",( mr.MR_DESC  == null) ? (object)DBNull.Value : mr.MR_DESC ) 
                            ,new SqlParameter("@MR_STAX_FRT",(mr.MR_STAX_FRT == null) ? (object)DBNull.Value : mr.MR_STAX_FRT )
                            ,new SqlParameter("@MR_STAX",( mr.MR_STAX  == null) ? (object)DBNull.Value : mr.MR_STAX ) 
                            ,new SqlParameter("@MR_USER_TYPE",( mr.MR_USER_TYPE  == null) ? (object)DBNull.Value : mr.MR_USER_TYPE ) 
                            ,new SqlParameter("@MR_ADD_BY",( mr.MR_ADD_BY  == null) ? (object)DBNull.Value : mr.MR_ADD_BY ) 
                            ,new SqlParameter("@MR_REMARKS",( mr.MR_REMARKS  == null) ? (object)DBNull.Value : mr.MR_REMARKS ) 
                            ,new SqlParameter("@MR_TENDER_ID",( mr.MR_TENDER_ID  == null) ? (object)DBNull.Value : mr.MR_TENDER_ID ) 
                            ,new SqlParameter("@MR_TENDER_NO",( mr.MR_TENDER_NO  == null) ? (object)DBNull.Value : mr.MR_TENDER_NO ) 
                            ,new SqlParameter("@IBT_TYPE_ID",( mr.IBT_TYPE_ID  == null) ? (object)DBNull.Value : mr.IBT_TYPE_ID ) 
                            ,new SqlParameter("@CRA_ID ",( CRA_ID  == null) ? (object)DBNull.Value : CRA_ID ) 

                            ,new SqlParameter("@FROM_CBS_DATE",( mr.FROM_CBS_DATE  == null) ? (object)DBNull.Value : mr.FROM_CBS_DATE ) 
                            ,new SqlParameter("@CBS_OPN_DATE",( mr.CBS_OPN_DATE  == null) ? (object)DBNull.Value : mr.CBS_OPN_DATE ) 
                            ,new SqlParameter("@CBS_DAYS",( mr.CBS_SRP_PD  == null) ? (object)DBNull.Value : mr.CBS_SRP_PD )
                            ,new SqlParameter("@MR_P_NAME",( mr.MR_MP_NAME  == null) ? (object)DBNull.Value : mr.MR_MP_NAME )
                            ,new SqlParameter("@MR_PREPARED_BY",( mr.EMP_ID  == null) ? (object)DBNull.Value : mr.EMP_ID )

                            // Added By - Sunil Kumar Singh Dated:13/01/2022
                            ,new SqlParameter("@SOA_AMT",( mr.SOA_AMT  == null) ? (object)DBNull.Value : mr.SOA_AMT )
                            ,new SqlParameter("@CGST_AMT",( mr.CGST_AMT  == null) ? (object)DBNull.Value : mr.CGST_AMT )
                            ,new SqlParameter("@SGST_AMT",( mr.SGST_AMT  == null) ? (object)DBNull.Value : mr.SGST_AMT )
                            ,new SqlParameter("@IGST_AMT",( mr.IGST_AMT  == null) ? (object)DBNull.Value : mr.IGST_AMT )
                        };

                        param[0].Direction = ParameterDirection.Output;
                        param[1].Direction = ParameterDirection.Output;
                        param[2].Direction = ParameterDirection.Output;

                        new DataAccess().InsertWithTransaction("[iTMS].[USP_INSERT_MR]", CommandType.StoredProcedure, out command, connection, transactionScope, param, 90);
                        MR_ID = (decimal)command.Parameters["@MR_ID"].Value;
                        string MR_NO = (string)command.Parameters["@MR_NO"].Value;
                        string error_1 = (string)command.Parameters["@ERRORSTR"].Value;
                        mr.MR_ID = MR_ID;
                        mr.MR_NO = MR_NO;
                        if (MR_ID == -1) { errorMsg = error_1; }
                    }


                    if (MR_ID > 0)
                    {
                        if (CRA_ID > 0 && errorMsg == "")
                        {
                            // ADJUSTMENT TABLE ADD  at the time of credit advice
                            SqlParameter[] paramAdj = new SqlParameter[9];

                            paramAdj[0] = new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200);
                            paramAdj[1] = new SqlParameter("@MR_ADJ_ID", SqlDbType.Decimal);
                            paramAdj[2] = new SqlParameter("@MR_ADJ_CV_MR_ID", DBNull.Value);
                            paramAdj[3] = new SqlParameter("@MR_ADJ_MR_ID", MR_ID);
                            paramAdj[4] = new SqlParameter("@MR_ADJ_CRA_ID", DBNull.Value);
                            paramAdj[5] = new SqlParameter("@MR_ADJ_MR_BILL_ID", (object)DBNull.Value);
                            paramAdj[6] = new SqlParameter("@MR_ADJ_MR_CN_ID", (object)DBNull.Value);
                            paramAdj[7] = new SqlParameter("@MR_ADJ_AMT", mr.MR_TOTAL_AMT);
                            paramAdj[8] = new SqlParameter("@MR_ADJ_CHQT_ID", DBNull.Value);

                            paramAdj[0].Direction = ParameterDirection.Output;
                            paramAdj[1].Direction = ParameterDirection.Output;

                            new DataAccess().InsertWithTransaction("[iFMS].[USP_INSERT_MR_ADJ]", CommandType.StoredProcedure, out command, connection, transactionScope, paramAdj);
                            decimal MR_ADJ_ID = (decimal)command.Parameters["@MR_ADJ_ID"].Value;
                            string error_Adj = (string)command.Parameters["@ERRORSTR"].Value;

                            if (MR_ADJ_ID == -1) { errorMsg = error_Adj; }
                        }

                        if (mr.REC_PAY_MODE_CHQ == true && errorMsg == "")
                        {
                            int payMode = 2;    //Cheque
                            if (Convert.ToInt32(mr.CHQ_RTGS_DD_FRESH) == 1)
                            {
                                SqlParameter[] paramChq = {
                                 new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                                ,new SqlParameter("@CHQ_ID", SqlDbType.Decimal) 
                                ,new SqlParameter("@CHQ_BR_ID",(mr.MR_BR_ID == null) ? (object)DBNull.Value : mr.MR_BR_ID) 
                                ,new SqlParameter("@CHQ_PA_ID",(mr.MR_PA_ID == null) ? (object)DBNull.Value : mr.MR_PA_ID) 
                                ,new SqlParameter("@CHQ_MODE",payMode) 
                                ,new SqlParameter("@CHQ_NO",( mr.CHQ_RTGS_DD_NO == null) ? (object)DBNull.Value : mr.CHQ_RTGS_DD_NO) 
                                ,new SqlParameter("@CHQ_COLL_DATE",( mr.CHQ_COLL_DATE == null) ? (object)DBNull.Value : mr.CHQ_COLL_DATE) 
                                ,new SqlParameter("@CHQ_COLL_BY",( mr.CHQ_COLL_BY == null) ? (object)DBNull.Value : mr.CHQ_COLL_BY) 
                                ,new SqlParameter("@CHQ_DATE",( mr.CHQ_RTGS_DD_DATE == null) ? (object)DBNull.Value : mr.CHQ_RTGS_DD_DATE) 
                                ,new SqlParameter("@CHQ_AMT",( mr.CHQ_RTGS_DD_AMT == null) ? (object)DBNull.Value : mr.CHQ_RTGS_DD_AMT)
                                ,new SqlParameter("@CHQ_BANK_ID",( mr.CHQ_RTGS_DD_BANK == null) ? (object)DBNull.Value : mr.CHQ_RTGS_DD_BANK) 
                                ,new SqlParameter("@CHQ_BANK_BR_NAME",( mr.CHQ_RTGS_DD_BANK_NAME == null) ? (object)DBNull.Value : mr.CHQ_RTGS_DD_BANK_NAME) 
                                ,new SqlParameter("@CHQ_BANK_STATE_ID ",( mr.CHQ_BANK_STATE_ID == null) ? (object)DBNull.Value : mr.CHQ_BANK_STATE_ID) 
                                ,new SqlParameter("@CHQ_BANK_CITY_ID",( mr.CHQ_BANK_CITY_ID == null) ? (object)DBNull.Value : mr.CHQ_BANK_CITY_ID) 
                                ,new SqlParameter("@CHQ_FILE_PATH",( mr.CHQ_FILE_PATH == null) ? (object)DBNull.Value : mr.CHQ_FILE_PATH) 
                                ,new SqlParameter("@CHQ_FILE_NAME",( mr.CHQ_FILE_NAME == null) ? (object)DBNull.Value : mr.CHQ_FILE_NAME) 
                                ,new SqlParameter("@CHQ_REMARKS",( mr.CHQ_REMARKS == null) ? (object)DBNull.Value : mr.CHQ_REMARKS) 
                                ,new SqlParameter("@CHQ_ADDED_BY_TYPE",( mr.MR_USER_TYPE == null) ? (object)DBNull.Value : mr.MR_USER_TYPE) 
                                ,new SqlParameter("@CHQ_ADDED_BY",( mr.EMP_ID == null) ? (object)DBNull.Value : mr.EMP_ID) 
                                };

                                paramChq[0].Direction = ParameterDirection.Output;
                                paramChq[1].Direction = ParameterDirection.Output;

                                new DataAccess().InsertWithTransaction("[iFMS].[USP_INSERT_CHQ_RECEIPTS_REGISTER]", CommandType.StoredProcedure, out command, connection, transactionScope, paramChq);
                                decimal CHQ_ID = (decimal)command.Parameters["@CHQ_ID"].Value;
                                string error_chq = (string)command.Parameters["@ERRORSTR"].Value;
                                if (CHQ_ID == -1) { errorMsg = error_chq; }
                                mr.CHQ_ID = CHQ_ID;
                            }

                            SqlParameter[] param1 = {
                                 new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                                ,new SqlParameter("@MR_RID", SqlDbType.Decimal) 
                                ,new SqlParameter("@MR_ID", MR_ID) 
                                ,new SqlParameter("@PAY_MODE",payMode) 
                                ,new SqlParameter("@CHQ_ID",( mr.CHQ_ID == null) ? (object)DBNull.Value : mr.CHQ_ID) 
                                ,new SqlParameter("@CHQ_RTGS_DD_NO",( mr.CHQ_RTGS_DD_NO == null) ? (object)DBNull.Value : mr.CHQ_RTGS_DD_NO) 
                                ,new SqlParameter("@CHQ_RTGS_DD_DATE",( mr.CHQ_RTGS_DD_DATE == null) ? (object)DBNull.Value : mr.CHQ_RTGS_DD_DATE) 
                                ,new SqlParameter("@CHQ_RTGS_DD_BANK",( mr.CHQ_RTGS_DD_BANK == null) ? (object)DBNull.Value : mr.CHQ_RTGS_DD_BANK) 
                                ,new SqlParameter("@AMOUNT",( mr.CHQ_RTGS_DD_AMT == null) ? (object)DBNull.Value : mr.CHQ_RTGS_DD_AMT)  
                                // 20-10-2021 MR INSERT TIME CHQ_RECEIPTS_STATUS TABLE INSERT
                                ,new SqlParameter("@MR_CBS_BR_ID",( mr.MR_CBS_BR_ID  == null) ? (object)DBNull.Value : mr.MR_CBS_BR_ID )  
                                ,new SqlParameter("@MR_CBS_DATE",( mr.MR_CBS_DATE  == null) ? (object)DBNull.Value : mr.MR_CBS_DATE )   
                                ,new SqlParameter("@MR_USER_TYPE",( mr.MR_USER_TYPE  == null) ? (object)DBNull.Value : mr.MR_USER_TYPE )   
                                ,new SqlParameter("@MR_ADD_BY",( mr.MR_ADD_BY  == null) ? (object)DBNull.Value : mr.MR_ADD_BY ) 

                                };

                            param1[0].Direction = ParameterDirection.Output;
                            param1[1].Direction = ParameterDirection.Output;

                            new DataAccess().InsertWithTransaction("[iTMS].[USP_INSERT_MR_REC]", CommandType.StoredProcedure, out command, connection, transactionScope, param1);
                            decimal MR_RID = (decimal)command.Parameters["@MR_RID"].Value;
                            string error_2 = (string)command.Parameters["@ERRORSTR"].Value;
                            if (MR_RID == -1) { errorMsg = error_2; }

                            // Insert MR Adjustment table
                            SqlParameter[] param10 =
                                                {
                                                  new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)    
                                                 ,new SqlParameter("@CHQA_ID", SqlDbType.Decimal)                                      
                                                 ,new SqlParameter("@CHQA_CHQ_ID", mr.CHQ_ID)
                                                 ,new SqlParameter("@CHQA_AMT", ( mr.CHQ_RTGS_DD_AMT == null) ? (object)DBNull.Value : mr.CHQ_RTGS_DD_AMT)
                                                 ,new SqlParameter("@CHQA_REF_ID",  MR_ID)
                                                 ,new SqlParameter("@CHQA_REF_TYPE", "MR") 
                                                 ,new SqlParameter("@CHQA_ADDED_BY_TYPE", (mr.MR_USER_TYPE ==null)?(object)DBNull.Value:Convert.ToString(mr.MR_USER_TYPE))
                                                 ,new SqlParameter("@CHQA_ADDED_BY", (mr.MR_ADD_BY ==null)?(object)DBNull.Value:Convert.ToDecimal(mr.MR_ADD_BY))
                                                };

                            param10[0].Direction = ParameterDirection.Output;
                            param10[1].Direction = ParameterDirection.Output;

                            new DataAccess().InsertWithTransaction("[iFMS].[USP_INSERT_CHQ_RECEIPTS_ADJ]", CommandType.StoredProcedure, out command, connection, transactionScope, param10);
                            decimal CHQA_ID = (decimal)command.Parameters["@CHQA_ID"].Value;
                            string error_11 = (string)command.Parameters["@ERRORSTR"].Value;

                            if (CHQA_ID == -1) { errorMsg = error_11; }

                            if (CRA_ID > 0)
                            {
                                SqlParameter[] param_ibt_mode_chq =
                                    { 
                                      new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                                     ,new SqlParameter("@CRM_ID", SqlDbType.Decimal) 
                                     ,new SqlParameter("@CRM_CRA_ID", CRA_ID) 
                                     ,new SqlParameter("@CRM_MODE", payMode)
                                     ,new SqlParameter("@CRN_CHQ_ID", ( mr.CHQ_ID == null) ? (object)DBNull.Value : mr.CHQ_ID) 
                                     ,new SqlParameter("@CRM_INS_NO",  ( mr.CHQ_RTGS_DD_NO == null) ? (object)DBNull.Value : mr.CHQ_RTGS_DD_NO) 
                                     ,new SqlParameter("@CRM_INS_DATE", ( mr.CHQ_RTGS_DD_DATE == null) ? (object)DBNull.Value : mr.CHQ_RTGS_DD_DATE) 
                                     ,new SqlParameter("@CRM_INS_BANK_ID", ( mr.CHQ_RTGS_DD_BANK == null) ? (object)DBNull.Value : mr.CHQ_RTGS_DD_BANK) 
                                     ,new SqlParameter("@CRM_INS_AMT", ( mr.CHQ_RTGS_DD_AMT == null) ? (object)DBNull.Value : mr.CHQ_RTGS_DD_AMT)
                                    };

                                param_ibt_mode_chq[0].Direction = ParameterDirection.Output;
                                param_ibt_mode_chq[1].Direction = ParameterDirection.Output;

                                new DataAccess().InsertWithTransaction("[iFMS].[USP_INSERT_CREDIT_ADVICE_MODE]", CommandType.StoredProcedure, out command, connection, transactionScope, param_ibt_mode_chq);
                                string error_ibt_mode_chq = (string)command.Parameters["@ERRORSTR"].Value;
                                decimal CRM_ID = (decimal)command.Parameters["@CRM_ID"].Value;
                                if (CRM_ID == -1) { errorMsg = error_ibt_mode_chq; }
                            }
                        }

                        if (mr.REC_PAY_MODE_CASH == true && errorMsg == "")
                        {
                            int payMode = 1;//Cash
                            SqlParameter[] param1 = {
                                 new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                                ,new SqlParameter("@MR_RID", SqlDbType.Decimal) 
                                ,new SqlParameter("@MR_ID", MR_ID) 
                                ,new SqlParameter("@PAY_MODE",payMode) 
                                ,new SqlParameter("@CHQ_ID",(object)DBNull.Value) 
                                ,new SqlParameter("@CHQ_RTGS_DD_NO",(object)DBNull.Value) 
                                ,new SqlParameter("@CHQ_RTGS_DD_DATE",(object)DBNull.Value) 
                                ,new SqlParameter("@CHQ_RTGS_DD_BANK",(object)DBNull.Value ) 
                                ,new SqlParameter("@AMOUNT",( mr.CASH_AMT == null) ? (object)DBNull.Value : mr.CASH_AMT) 
                                };

                            param1[0].Direction = ParameterDirection.Output;
                            param1[1].Direction = ParameterDirection.Output;

                            new DataAccess().InsertWithTransaction("[iTMS].[USP_INSERT_MR_REC]", CommandType.StoredProcedure, out command, connection, transactionScope, param1);
                            decimal MR_RID = (decimal)command.Parameters["@MR_RID"].Value;
                            string error_2 = (string)command.Parameters["@ERRORSTR"].Value;
                            if (MR_RID == -1) { errorMsg = error_2; }


                            if (CRA_ID > 0 && errorMsg == "")
                            {
                                SqlParameter[] param_ibt_mode_cash =
                                    { 
                                      new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                                     ,new SqlParameter("@CRM_ID", SqlDbType.Decimal) 
                                     ,new SqlParameter("@CRM_CRA_ID", CRA_ID) 
                                     ,new SqlParameter("@CRM_MODE", payMode)
                                     ,new SqlParameter("@CRN_CHQ_ID", (object)DBNull.Value)  
                                     ,new SqlParameter("@CRM_INS_NO",(object)DBNull.Value) 
                                     ,new SqlParameter("@CRM_INS_DATE",(object)DBNull.Value)  
                                     ,new SqlParameter("@CRM_INS_BANK_ID",(object)DBNull.Value)  
                                     ,new SqlParameter("@CRM_INS_AMT",( mr.CASH_AMT == null) ? (object)DBNull.Value : mr.CASH_AMT)
                                    };

                                param_ibt_mode_cash[0].Direction = ParameterDirection.Output;
                                param_ibt_mode_cash[1].Direction = ParameterDirection.Output;

                                new DataAccess().InsertWithTransaction("[iFMS].[USP_INSERT_CREDIT_ADVICE_MODE]", CommandType.StoredProcedure, out command, connection, transactionScope, param_ibt_mode_cash);
                                string error_ibt_mode_cash = (string)command.Parameters["@ERRORSTR"].Value;
                                decimal CRM_ID = (decimal)command.Parameters["@CRM_ID"].Value;
                                if (CRM_ID == -1) { errorMsg = error_ibt_mode_cash; }
                            }
                        }

                        if (mr.REC_PAY_MODE_POS == true && errorMsg == "")
                        {
                            int payMode = 5;//POS        // 19/10/2020 As per sunil sir Instruction
                            SqlParameter[] param1 = {
                                 new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                                ,new SqlParameter("@MR_RID", SqlDbType.Decimal) 
                                ,new SqlParameter("@MR_ID", MR_ID) 
                                ,new SqlParameter("@PAY_MODE",payMode) 
                                ,new SqlParameter("@CHQ_ID",(object)DBNull.Value) 
                                ,new SqlParameter("@CHQ_RTGS_DD_NO",( mr.POS_TRAN_NO == null) ? (object)DBNull.Value : mr.POS_TRAN_NO) 
                                ,new SqlParameter("@CHQ_RTGS_DD_DATE",(object)DBNull.Value) 
                                ,new SqlParameter("@CHQ_RTGS_DD_BANK",( mr.CHQ_RTGS_DD_BANK == null) ? (object)DBNull.Value : mr.CHQ_RTGS_DD_BANK) 
                                ,new SqlParameter("@AMOUNT",( mr.POS_AMT == null) ? (object)DBNull.Value : mr.POS_AMT) 
                                };

                            param1[0].Direction = ParameterDirection.Output;
                            param1[1].Direction = ParameterDirection.Output;

                            new DataAccess().InsertWithTransaction("[iTMS].[USP_INSERT_MR_REC]", CommandType.StoredProcedure, out command, connection, transactionScope, param1);
                            decimal MR_RID = (decimal)command.Parameters["@MR_RID"].Value;
                            string error_2 = (string)command.Parameters["@ERRORSTR"].Value;
                            if (MR_RID == -1) { errorMsg = error_2; }

                            if (CRA_ID > 0 && errorMsg == "")
                            {
                                SqlParameter[] param_ibt_mode_pos =
                                    { 
                                      new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                                     ,new SqlParameter("@CRM_ID", SqlDbType.Decimal) 
                                     ,new SqlParameter("@CRM_CRA_ID", CRA_ID) 
                                     ,new SqlParameter("@CRM_MODE", payMode)
                                     ,new SqlParameter("@CRN_CHQ_ID", (object)DBNull.Value)  
                                     ,new SqlParameter("@CRM_INS_NO",( mr.POS_TRAN_NO == null) ? (object)DBNull.Value : mr.POS_TRAN_NO) 
                                     ,new SqlParameter("@CRM_INS_DATE",(object)DBNull.Value)  
                                     ,new SqlParameter("@CRM_INS_BANK_ID",( mr.CHQ_RTGS_DD_BANK == null) ? (object)DBNull.Value : mr.CHQ_RTGS_DD_BANK) 
                                     ,new SqlParameter("@CRM_INS_AMT",( mr.POS_AMT == null) ? (object)DBNull.Value : mr.POS_AMT) 
                                    };

                                param_ibt_mode_pos[0].Direction = ParameterDirection.Output;
                                param_ibt_mode_pos[1].Direction = ParameterDirection.Output;

                                new DataAccess().InsertWithTransaction("[iFMS].[USP_INSERT_CREDIT_ADVICE_MODE]", CommandType.StoredProcedure, out command, connection, transactionScope, param_ibt_mode_pos);
                                string error_ibt_mode_pos = (string)command.Parameters["@ERRORSTR"].Value;
                                decimal CRM_ID = (decimal)command.Parameters["@CRM_ID"].Value;
                                if (CRM_ID == -1) { errorMsg = error_ibt_mode_pos; }
                            }
                        }

                        if (mr.BILL_CNS_DTL_LIST != null && (mr.MR_DOC_TYPE == 2) && errorMsg == "")
                        {
                            //SON_TYPE_ID: 2 for Bill
                            SqlParameter[] param2 = new SqlParameter[23];
                            SqlParameter[] paramBFD = new SqlParameter[17];
                            SqlParameter[] paramCNs_BFD = new SqlParameter[10];
                            foreach (CN_OR_BILL_DTL cn in mr.BILL_CNS_DTL_LIST)
                            {
                                if ((cn.CN_OR_BILL_ID) > 0)
                                {

                                    //BFD 
                                    MRBFD_CLAM_AMT = cn.MRBFD_CLAM_AMT ?? 0;
                                    MRBFD_RECO_AMT = cn.MRBFD_RECO_AMT ?? 0;
                                    MRBFD_NREC_AMT = cn.MRBFD_NREC_AMT ?? 0;
                                    MRBFD_EMD_AMT = cn.MRBFD_EMD_AMT ?? 0;
                                    MRBFD_SD_AMT = cn.MRBFD_SD_AMT ?? 0;
                                    MR_TDS_ON_AMT = cn.MR_TDS_ON_AMT ?? 0;
                                    MR_TDS_AMT = cn.MR_TDS_AMT ?? 0;

                                    param2[0] = new SqlParameter("@MR_CN_ID", SqlDbType.Decimal);
                                    param2[0].Direction = ParameterDirection.Output;
                                    param2[1] = new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200);
                                    param2[1].Direction = ParameterDirection.Output;

                                    param2[2] = new SqlParameter("@MR_ID", MR_ID);
                                    param2[3] = new SqlParameter("@BILL_ID", (cn.CN_OR_BILL_ID == null) ? (object)DBNull.Value : cn.CN_OR_BILL_ID);
                                    param2[4] = new SqlParameter("@BILL_FRT_AMT", (cn.CN_OR_BILL_FRT_AMT == null) ? (object)DBNull.Value : cn.CN_OR_BILL_FRT_AMT);
                                    param2[5] = new SqlParameter("@MR_DEMM_AMT", (cn.MR_DEMM_AMT == null) ? (object)DBNull.Value : cn.MR_DEMM_AMT);
                                    param2[6] = new SqlParameter("@MR_HNDL_AMT", (cn.MR_HNDL_AMT == null) ? (object)DBNull.Value : cn.MR_HNDL_AMT);
                                    param2[7] = new SqlParameter("@MR_OCT_AMT", (cn.MR_OCT_AMT == null) ? (object)DBNull.Value : cn.MR_OCT_AMT);
                                    param2[8] = new SqlParameter("@MR_OCS_AMT", (cn.MR_OCS_AMT == null) ? (object)DBNull.Value : cn.MR_OCS_AMT);
                                    param2[9] = new SqlParameter("@MR_DLVCH_AMT", (cn.MR_DLVCH_AMT == null) ? (object)DBNull.Value : cn.MR_DLVCH_AMT);
                                    param2[10] = new SqlParameter("@MR_MISC_AMT", (cn.MR_MISC_AMT == null) ? (object)DBNull.Value : cn.MR_MISC_AMT);
                                    param2[11] = new SqlParameter("@MR_OTH_AMT", (cn.MR_OTH_AMT == null) ? (object)DBNull.Value : cn.MR_OTH_AMT);
                                    param2[12] = new SqlParameter("@MR_OPMC_AMT", (cn.MR_OPMC_AMT == null) ? (object)DBNull.Value : cn.MR_OPMC_AMT);
                                    param2[13] = new SqlParameter("@MR_GTX_AMT", (cn.MR_GTX_AMT == null) ? (object)DBNull.Value : cn.MR_GTX_AMT);
                                    param2[14] = new SqlParameter("@MR_CPE_AMT", (cn.MR_CPE_AMT == null) ? (object)DBNull.Value : cn.MR_CPE_AMT);
                                    param2[15] = new SqlParameter("@MR_GDN_CHRG_AMT", (cn.MR_GDN_CHRG_AMT == null) ? (object)DBNull.Value : cn.MR_GDN_CHRG_AMT);
                                    param2[16] = new SqlParameter("@MR_SUB_TOTAL_AMT", (cn.MR_TOTAL_AMT == null) ? (object)DBNull.Value : cn.MR_TOTAL_AMT);
                                    param2[17] = new SqlParameter("@MR_BFD_AMT", (cn.TOT_DEDN_AMT == null) ? (object)DBNull.Value : cn.TOT_DEDN_AMT);
                                    param2[18] = new SqlParameter("@MR_TOTAL_AMT", (cn.NET_RECD_AMT == null) ? (object)DBNull.Value : cn.NET_RECD_AMT);
                                    param2[19] = new SqlParameter("@MR_STAX_FRT", (cn.MR_STAX_FRT == null) ? (object)DBNull.Value : cn.MR_STAX_FRT);
                                    param2[20] = new SqlParameter("@MR_STAX", (cn.MR_STAX == null) ? (object)DBNull.Value : cn.MR_STAX);
                                    param2[21] = new SqlParameter("@MR_COD_CQNO", (cn.MR_COD_CQNO == null) ? (object)DBNull.Value : cn.MR_COD_CQNO);
                                    param2[22] = new SqlParameter("@MR_COD_AMT", (cn.MR_COD_AMT == null) ? (object)DBNull.Value : cn.MR_COD_AMT);

                                    new DataAccess().InsertWithTransaction("[iTMS].[USP_INSERT_MR_BILL]", CommandType.StoredProcedure, out command, connection, transactionScope, param2);
                                    decimal MR_CN_ID = (decimal)command.Parameters["@MR_CN_ID"].Value;
                                    string error_3 = (string)command.Parameters["@ERRORSTR"].Value;
                                    if (MR_CN_ID == -1) { errorMsg = error_3; break; }

                                    if ((MRBFD_CLAM_AMT + MRBFD_RECO_AMT + MRBFD_NREC_AMT + MRBFD_EMD_AMT + MRBFD_SD_AMT + MR_TDS_ON_AMT + MR_TDS_AMT) > 0)
                                    {
                                        paramBFD[0] = new SqlParameter("@MRBFD_ID", SqlDbType.Decimal);
                                        paramBFD[0].Direction = ParameterDirection.Output;
                                        paramBFD[1] = new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200);
                                        paramBFD[1].Direction = ParameterDirection.Output;

                                        paramBFD[2] = new SqlParameter("@MRBFD_MR_ID", MR_ID);
                                        paramBFD[3] = new SqlParameter("@MRBFD_BILL_ID", (cn.CN_OR_BILL_ID == null) ? (object)DBNull.Value : cn.CN_OR_BILL_ID);
                                        paramBFD[4] = new SqlParameter("@MRBFD_DOC_TYPE", (mr.MR_DOC_TYPE == null) ? (object)DBNull.Value : mr.MR_DOC_TYPE);
                                        paramBFD[5] = new SqlParameter("@MRBFD_CN_ID", (object)DBNull.Value);
                                        paramBFD[6] = new SqlParameter("@MRBFD_CLAM_AMT", (cn.MRBFD_CLAM_AMT == null) ? (object)DBNull.Value : cn.MRBFD_CLAM_AMT);
                                        paramBFD[7] = new SqlParameter("@MRBFD_RECO_AMT", (cn.MRBFD_RECO_AMT == null) ? (object)DBNull.Value : cn.MRBFD_RECO_AMT);
                                        paramBFD[8] = new SqlParameter("@MRBFD_NREC_AMT", (cn.MRBFD_NREC_AMT == null) ? (object)DBNull.Value : cn.MRBFD_NREC_AMT);
                                        paramBFD[9] = new SqlParameter("@MRBFD_BFDR_ID", (cn.MRBFD_BFDR_ID == null) ? (object)DBNull.Value : cn.MRBFD_BFDR_ID);
                                        paramBFD[10] = new SqlParameter("@MRBFD_EMD_AMT", (cn.MRBFD_EMD_AMT == null) ? (object)DBNull.Value : cn.MRBFD_EMD_AMT);
                                        paramBFD[11] = new SqlParameter("@MRBFD_SD_AMT", (cn.MRBFD_SD_AMT == null) ? (object)DBNull.Value : cn.MRBFD_SD_AMT);
                                        paramBFD[12] = new SqlParameter("@MRBFD_TDS_ON_AMT", (cn.MR_TDS_ON_AMT == null) ? (object)DBNull.Value : cn.MR_TDS_ON_AMT);
                                        paramBFD[13] = new SqlParameter("@MRBFD_TDS_PER", (cn.MR_TDS_PER == null) ? (object)DBNull.Value : cn.MR_TDS_PER);
                                        paramBFD[14] = new SqlParameter("@MRBFD_TDS_AMT", (cn.MR_TDS_AMT == null) ? (object)DBNull.Value : cn.MR_TDS_AMT);
                                        paramBFD[15] = new SqlParameter("@MRBFD_MR_CN_ID", DBNull.Value);
                                        paramBFD[16] = new SqlParameter("@MRBFD_MR_BILL_ID", (MR_CN_ID == null) ? (object)DBNull.Value : MR_CN_ID);

                                        new DataAccess().InsertWithTransaction("[iTMS].[USP_INSERT_MR_BFD]", CommandType.StoredProcedure, out command, connection, transactionScope, paramBFD);
                                        decimal MRBFD_ID = (decimal)command.Parameters["@MRBFD_ID"].Value;
                                        string error_BFD = (string)command.Parameters["@ERRORSTR"].Value;
                                        if (MRBFD_ID == -1) { errorMsg = error_BFD; break; }

                                        // Bill wise Cn BFD Deduction
                                        if (cn.CNS_BFD_LIST != null)
                                        {
                                            if (cn.CNS_BFD_LIST.Count() > 0)
                                            {
                                                foreach (MR_CNS_BFD cnBFD in cn.CNS_BFD_LIST) // CNs BFD, 26-12-2020
                                                {
                                                    if ((cnBFD.CN_ID ?? 0) > 0)
                                                    {
                                                        paramCNs_BFD[0] = new SqlParameter("@CNS_BFD_ID", SqlDbType.Decimal);
                                                        paramCNs_BFD[0].Direction = ParameterDirection.Output;
                                                        paramCNs_BFD[1] = new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200);
                                                        paramCNs_BFD[1].Direction = ParameterDirection.Output;

                                                        paramCNs_BFD[2] = new SqlParameter("@BFD_ID", MRBFD_ID);
                                                        paramCNs_BFD[3] = new SqlParameter("@CN_ID", (cnBFD.CN_ID == null) ? (object)DBNull.Value : cnBFD.CN_ID);
                                                        paramCNs_BFD[4] = new SqlParameter("@CLAM_AMT", (cnBFD.CLAM_AMT == null) ? (object)DBNull.Value : cnBFD.CLAM_AMT);
                                                        paramCNs_BFD[5] = new SqlParameter("@RECO_AMT", (cnBFD.RECO_AMT == null) ? (object)DBNull.Value : cnBFD.RECO_AMT);
                                                        paramCNs_BFD[6] = new SqlParameter("@NREC_AMT", (cnBFD.NREC_AMT == null) ? (object)DBNull.Value : cnBFD.NREC_AMT);
                                                        paramCNs_BFD[7] = new SqlParameter("@BFDR_ID", (cnBFD.BFDR_ID == null) ? (object)DBNull.Value : cnBFD.BFDR_ID);
                                                        paramCNs_BFD[8] = new SqlParameter("@EMD_AMT", (cnBFD.EMD_AMT == null) ? (object)DBNull.Value : cnBFD.EMD_AMT);
                                                        paramCNs_BFD[9] = new SqlParameter("@SD_AMT", (cnBFD.SD_AMT == null) ? (object)DBNull.Value : cnBFD.SD_AMT);

                                                        new DataAccess().InsertWithTransaction("[iFMS].[USP_INSERT_MR_CNS_BFD]", CommandType.StoredProcedure, out command, connection, transactionScope, paramCNs_BFD);
                                                        decimal CNS_BFD_ID = (decimal)command.Parameters["@CNS_BFD_ID"].Value;
                                                        string error_cns_bfd = (string)command.Parameters["@ERRORSTR"].Value;
                                                        if (CNS_BFD_ID == -1) { errorMsg = error_cns_bfd; break; }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (mr.BILL_CNS_DTL_LIST != null && (mr.MR_DOC_TYPE == 1) && errorMsg == "")
                        {
                            //SON_TYPE_ID: 1 for CN
                            SqlParameter[] param3 = new SqlParameter[24];
                            SqlParameter[] paramBFD = new SqlParameter[17];
                            Nullable<decimal> MR_DPR_ID = null;
                            mr.DPR_NO_LIST = new List<MR_DPR_NO>();
                            int dpr_cnt = 0;
                            int DPR_MR_UCG = 0;

                            if (mr.MR_TYPE == 7)
                            {
                                DPR_MR_UCG = 1;
                            }
                            else
                            {
                                DPR_MR_UCG = 0;
                            }

                            foreach (CN_OR_BILL_DTL cn in mr.BILL_CNS_DTL_LIST)
                            {
                                if ((cn.CN_OR_BILL_ID) > 0)
                                {
                                    MR_DPR_NO dpr = null;
                                    if ((mr.MR_TYPE == 1 || mr.MR_TYPE == 7) && mr.MR_DPR_STATUS == false)// 1 for delivery  7 - U C G SALES
                                    {
                                        //Dpr No Check
                                        SqlParameter[] paramDprCheck = { new SqlParameter("@CN_ID", cn.CN_OR_BILL_ID), new SqlParameter("@DPR_BR_ID", mr.MR_BR_ID) };
                                        DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iTMS].[USP_CHECK_DPR_FOR_MR_BY_CN_ID]", CommandType.StoredProcedure, paramDprCheck);
                                        if (ds != null)
                                        {
                                            if (ds.Tables[0].Rows.Count > 0)
                                            {
                                                string dprNo = Convert.ToString(ds.Tables[0].Rows[0]["DPR_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["DPR_NO"]);
                                                //Added by pramesh,25-02-2023
                                                string nextDprNo = Convert.ToString(ds.Tables[0].Rows[0]["NEXT_DPR_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["NEXT_DPR_NO"]);
                                                if (dprNo.Trim() == "")
                                                {
                                                    if (dpr_cnt > 0)
                                                    {
                                                        if (DPR_MR_UCG == 0)
                                                        {
                                                            mr.DPR_NO = nextDprNo;
                                                        }
                                                    }

                                                    SqlParameter[] paramDpr =
                                                    {
                                                         new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                                                        ,new SqlParameter("@DPR_ID", SqlDbType.Decimal) 
                                                        ,new SqlParameter("@DPR_SYS_NO", SqlDbType.VarChar, 15)
                                                        ,new SqlParameter("@DPR_BR_ID", (mr.MR_BR_ID == null) ? (object)DBNull.Value : mr.MR_BR_ID) 
                                                        ,new SqlParameter("@DPR_NO",(mr.DPR_NO == null) ? (object)DBNull.Value : mr.DPR_NO) 
                                                        ,new SqlParameter("@DPR_SFX",".") 
                                                        ,new SqlParameter("@DPR_DATE",( mr.MR_DATE == null) ? (object)DBNull.Value : mr.MR_DATE) 
                                                        ,new SqlParameter("@DPR_CN_CNEE_PA_ID",( mr.MR_PA_ID == null) ? (object)DBNull.Value : mr.MR_PA_ID) 
                                                        ,new SqlParameter("@DPR_CN_BR_ID",( cn.CN_OR_BILL_BR_ID == null) ? (object)DBNull.Value : cn.CN_OR_BILL_BR_ID) 
                                                        ,new SqlParameter("@DPR_CN_ID",( cn.CN_OR_BILL_ID == null) ? (object)DBNull.Value : cn.CN_OR_BILL_ID) 
                                                        ,new SqlParameter("@DPR_PKG_DEL",(object)DBNull.Value) 
                                                        ,new SqlParameter("@DPR_ACTWT_DEL",(object)DBNull.Value) 
                                                        ,new SqlParameter("@DPR_CN_DEL_DT",( mr.MR_DATE == null) ? (object)DBNull.Value : mr.MR_DATE) 
                                                        ,new SqlParameter("@DPR_GEN_BR_ID",( mr.MR_BR_ID == null) ? (object)DBNull.Value : mr.MR_BR_ID) 
                                                        ,new SqlParameter("@DPR_DEL_STATUS",true) 
                                                        ,new SqlParameter("@DPR_DEL_FAIL_REASON",(object)DBNull.Value) 
                                                        ,new SqlParameter("@DPR_IMG_UPLOAD",(object)DBNull.Value) 
                                                        ,new SqlParameter("@DPR_IMG_PATH",(object)DBNull.Value) 
                                                        ,new SqlParameter("@DPR_IMG_NAME",(object)DBNull.Value) 
                                                        ,new SqlParameter("@DPR_REMARKS",(object)DBNull.Value) 

                                                        ,new SqlParameter("@DPR_DELAY_DAY",(object)DBNull.Value) 
                                                        ,new SqlParameter("@DPR_DELAY_REASON",(object)DBNull.Value) 
                                                        ,new SqlParameter("@DPR_TAR_ID",DBNull.Value) 
                                                        ,new SqlParameter("@DPR_DATA_SOURCE", 2) // MR
                                                        ,new SqlParameter("@DPR_ADDBY_TYPE",( mr.MR_USER_TYPE  == null) ? (object)DBNull.Value : mr.MR_USER_TYPE ) 
                                                        ,new SqlParameter("@DPR_ADDBY",( mr.EMP_ID  == null) ? (object)DBNull.Value : mr.EMP_ID ) 
                                                        ,new SqlParameter("@DPR_CN_DELTYPE_TO",(object)DBNull.Value) 
                                                        ,new SqlParameter("@DPR_MR_ID",MR_ID) 
                                                        ,new SqlParameter("@DPR_MR_UCG",DPR_MR_UCG) 
                                                    };

                                                    paramDpr[0].Direction = ParameterDirection.Output;
                                                    paramDpr[1].Direction = ParameterDirection.Output;
                                                    paramDpr[2].Direction = ParameterDirection.Output;

                                                    new DataAccess().InsertWithTransaction("[iTMS].[USP_INSERT_DPR]", CommandType.StoredProcedure, out command, connection, transactionScope, paramDpr, 90);
                                                    MR_DPR_ID = (decimal)command.Parameters["@DPR_ID"].Value;
                                                    string DPR_SYS_NO = (string)command.Parameters["@DPR_SYS_NO"].Value;
                                                    string error_dpr = (string)command.Parameters["@ERRORSTR"].Value;
                                                    if (MR_DPR_ID == -1) { errorMsg = error_dpr; break; }

                                                    dpr = new MR_DPR_NO();
                                                    dpr.DPR_NO = DPR_SYS_NO;
                                                    mr.DPR_NO_LIST.Add(dpr);
                                                    // mr.DPR_NO_LIST = dprList;

                                                    dpr_cnt = dpr_cnt + 1;
                                                }
                                            }
                                        }
                                    }

                                    //BFD
                                    MRBFD_CLAM_AMT = cn.MRBFD_CLAM_AMT ?? 0;
                                    MRBFD_RECO_AMT = cn.MRBFD_RECO_AMT ?? 0;
                                    MRBFD_NREC_AMT = cn.MRBFD_NREC_AMT ?? 0;
                                    MRBFD_EMD_AMT = cn.MRBFD_EMD_AMT ?? 0;
                                    MRBFD_SD_AMT = cn.MRBFD_SD_AMT ?? 0;
                                    MR_TDS_ON_AMT = cn.MR_TDS_ON_AMT ?? 0;
                                    MR_TDS_AMT = cn.MR_TDS_AMT ?? 0;

                                    param3[0] = new SqlParameter("@MR_CN_ID", SqlDbType.Decimal);
                                    param3[0].Direction = ParameterDirection.Output;
                                    param3[1] = new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200);
                                    param3[1].Direction = ParameterDirection.Output;

                                    param3[2] = new SqlParameter("@MR_ID", MR_ID);
                                    param3[3] = new SqlParameter("@CN_ID", (cn.CN_OR_BILL_ID == null) ? (object)DBNull.Value : cn.CN_OR_BILL_ID);
                                    param3[4] = new SqlParameter("@CN_FRT_AMT", (cn.CN_OR_BILL_FRT_AMT == null) ? (object)DBNull.Value : cn.CN_OR_BILL_FRT_AMT);
                                    param3[5] = new SqlParameter("@MR_DEMM_AMT", (cn.MR_DEMM_AMT == null) ? (object)DBNull.Value : cn.MR_DEMM_AMT);
                                    param3[6] = new SqlParameter("@MR_HNDL_AMT", (cn.MR_HNDL_AMT == null) ? (object)DBNull.Value : cn.MR_HNDL_AMT);
                                    param3[7] = new SqlParameter("@MR_OCT_AMT", (cn.MR_OCT_AMT == null) ? (object)DBNull.Value : cn.MR_OCT_AMT);
                                    param3[8] = new SqlParameter("@MR_OCS_AMT", (cn.MR_OCS_AMT == null) ? (object)DBNull.Value : cn.MR_OCS_AMT);
                                    param3[9] = new SqlParameter("@MR_DLVCH_AMT", (cn.MR_DLVCH_AMT == null) ? (object)DBNull.Value : cn.MR_DLVCH_AMT);
                                    param3[10] = new SqlParameter("@MR_MISC_AMT", (cn.MR_MISC_AMT == null) ? (object)DBNull.Value : cn.MR_MISC_AMT);
                                    param3[11] = new SqlParameter("@MR_OTH_AMT", (cn.MR_OTH_AMT == null) ? (object)DBNull.Value : cn.MR_OTH_AMT);
                                    param3[12] = new SqlParameter("@MR_OPMC_AMT", (cn.MR_OPMC_AMT == null) ? (object)DBNull.Value : cn.MR_OPMC_AMT);
                                    param3[13] = new SqlParameter("@MR_GTX_AMT", (cn.MR_GTX_AMT == null) ? (object)DBNull.Value : cn.MR_GTX_AMT);
                                    param3[14] = new SqlParameter("@MR_CPE_AMT", (cn.MR_CPE_AMT == null) ? (object)DBNull.Value : cn.MR_CPE_AMT);
                                    param3[15] = new SqlParameter("@MR_GDN_CHRG_AMT", (cn.MR_GDN_CHRG_AMT == null) ? (object)DBNull.Value : cn.MR_GDN_CHRG_AMT);
                                    param3[16] = new SqlParameter("@MR_SUB_TOTAL_AMT", (cn.MR_TOTAL_AMT == null) ? (object)DBNull.Value : cn.MR_TOTAL_AMT);
                                    param3[17] = new SqlParameter("@MR_BFD_AMT", (cn.TOT_DEDN_AMT == null) ? (object)DBNull.Value : cn.TOT_DEDN_AMT);       //MR_BFD_AMT
                                    param3[18] = new SqlParameter("@MR_TOTAL_AMT", (cn.NET_RECD_AMT == null) ? (object)DBNull.Value : cn.NET_RECD_AMT);
                                    param3[19] = new SqlParameter("@MR_STAX_FRT", (cn.MR_STAX_FRT == null) ? (object)DBNull.Value : cn.MR_STAX_FRT);
                                    param3[20] = new SqlParameter("@MR_STAX", (cn.MR_STAX == null) ? (object)DBNull.Value : cn.MR_STAX);
                                    param3[21] = new SqlParameter("@MR_COD_CQNO", (cn.MR_COD_CQNO == null) ? (object)DBNull.Value : cn.MR_COD_CQNO);
                                    param3[22] = new SqlParameter("@MR_COD_AMT", (cn.MR_COD_AMT == null) ? (object)DBNull.Value : cn.MR_COD_AMT);
                                    param3[23] = new SqlParameter("@MR_DPR_ID", (MR_DPR_ID == null) ? (object)DBNull.Value : MR_DPR_ID);

                                    new DataAccess().InsertWithTransaction("[iTMS].[USP_INSERT_MR_CN]", CommandType.StoredProcedure, out command, connection, transactionScope, param3);
                                    decimal MR_CN_ID = (decimal)command.Parameters["@MR_CN_ID"].Value;
                                    string error_4 = (string)command.Parameters["@ERRORSTR"].Value;
                                    if (MR_CN_ID == -1) { errorMsg = error_4; break; }

                                    if ((MRBFD_CLAM_AMT + MRBFD_RECO_AMT + MRBFD_NREC_AMT + MRBFD_EMD_AMT + MRBFD_SD_AMT + MR_TDS_ON_AMT + MR_TDS_AMT) > 0 && errorMsg == "")
                                    {
                                        paramBFD[0] = new SqlParameter("@MRBFD_ID", SqlDbType.Decimal);
                                        paramBFD[0].Direction = ParameterDirection.Output;
                                        paramBFD[1] = new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200);
                                        paramBFD[1].Direction = ParameterDirection.Output;

                                        paramBFD[2] = new SqlParameter("@MRBFD_MR_ID", MR_ID);
                                        paramBFD[3] = new SqlParameter("@MRBFD_BILL_ID", (object)DBNull.Value);
                                        paramBFD[4] = new SqlParameter("@MRBFD_DOC_TYPE", (mr.MR_DOC_TYPE == null) ? (object)DBNull.Value : mr.MR_DOC_TYPE);
                                        paramBFD[5] = new SqlParameter("@MRBFD_CN_ID", (cn.CN_OR_BILL_ID == null) ? (object)DBNull.Value : cn.CN_OR_BILL_ID);
                                        paramBFD[6] = new SqlParameter("@MRBFD_CLAM_AMT", (cn.MRBFD_CLAM_AMT == null) ? (object)DBNull.Value : cn.MRBFD_CLAM_AMT);
                                        paramBFD[7] = new SqlParameter("@MRBFD_RECO_AMT", (cn.MRBFD_RECO_AMT == null) ? (object)DBNull.Value : cn.MRBFD_RECO_AMT);
                                        paramBFD[8] = new SqlParameter("@MRBFD_NREC_AMT", (cn.MRBFD_NREC_AMT == null) ? (object)DBNull.Value : cn.MRBFD_NREC_AMT);
                                        paramBFD[9] = new SqlParameter("@MRBFD_BFDR_ID", (cn.MRBFD_BFDR_ID == null) ? (object)DBNull.Value : cn.MRBFD_BFDR_ID);
                                        paramBFD[10] = new SqlParameter("@MRBFD_EMD_AMT", (cn.MRBFD_EMD_AMT == null) ? (object)DBNull.Value : cn.MRBFD_EMD_AMT);
                                        paramBFD[11] = new SqlParameter("@MRBFD_SD_AMT", (cn.MRBFD_SD_AMT == null) ? (object)DBNull.Value : cn.MRBFD_SD_AMT);
                                        paramBFD[12] = new SqlParameter("@MRBFD_TDS_ON_AMT", (cn.MR_TDS_ON_AMT == null) ? (object)DBNull.Value : cn.MR_TDS_ON_AMT);
                                        paramBFD[13] = new SqlParameter("@MRBFD_TDS_PER", (cn.MR_TDS_PER == null) ? (object)DBNull.Value : cn.MR_TDS_PER);
                                        paramBFD[14] = new SqlParameter("@MRBFD_TDS_AMT", (cn.MR_TDS_AMT == null) ? (object)DBNull.Value : cn.MR_TDS_AMT);
                                        paramBFD[15] = new SqlParameter("@MRBFD_MR_CN_ID", (MR_CN_ID == null) ? (object)DBNull.Value : MR_CN_ID);
                                        paramBFD[16] = new SqlParameter("@MRBFD_MR_BILL_ID", DBNull.Value);

                                        new DataAccess().InsertWithTransaction("[iTMS].[USP_INSERT_MR_BFD]", CommandType.StoredProcedure, out command, connection, transactionScope, paramBFD);
                                        decimal MRBFD_ID = (decimal)command.Parameters["@MRBFD_ID"].Value;
                                        string error_BFD = (string)command.Parameters["@ERRORSTR"].Value;
                                        if (MRBFD_ID == -1) { errorMsg = error_BFD; break; }
                                    }
                                }
                            }
                        }

                        //23-07-2021
                        // POS - TRANS (START HERE)
                        decimal vAmountInRs = 0;
                        decimal vMRAmtInPs = 0;
                        string vPayType = "0";
                        string vInputs = "";

                        string ResponseCode = "";
                        string Request_Input = "";
                        string ApprovalCode = "";
                        string RRN_No = "";
                        string Card_Num = "";
                        string Card_Type = "";
                        string CardHolder_Name = "";
                        string Acquirer_Bank = "";
                        string Txn_Date = "";
                        string Txn_Type = "";
                        string BankInvoice_Num = "";
                        string Batch_Number = "";
                        string Terminal_Id = "";
                        string Merchant_Id = "";
                        double Amount = 0;

                        if (mr.REC_PAY_MODE_POS == true && (mr.POS_AMT ?? 0) > 0 && errorMsg == "")
                        {
                            vMRAmtInPs = Convert.ToDecimal(mr.POS_AMT) * 100;
                            vPayType = Convert.ToString(mr.MR_POSPAYTYPE);
                            vInputs = mr.MR_MANUAL_NO + "!" + mr.MR_ADD_BY_CODE + "!" + mr.MR_POSMOB;

                            string requestURL = "http://incognitus.innoviti.com:20129/innoweb/api/MSwipe?value=0," + vPayType + "," + vInputs + "," + vMRAmtInPs;

                            //string requestURL = "http://localhost:81/innoweb/api/MSwipe?value=0," + vPayType + "," + vInputs + "," + vMRAmtInPs;

                            try
                            {
                                string OutPut = string.Empty;
                                string responseFromServer = "";
                                //string res = "";

                                HttpWebRequest request = WebRequest.Create(requestURL) as HttpWebRequest;
                                request.Method = "GET";
                                request.MediaType = "application/json";

                                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                                Stream dataStream = response.GetResponseStream();
                                StreamReader reader1 = new StreamReader(dataStream);
                                responseFromServer = reader1.ReadToEnd();
                                reader1.Close();
                                response.Close();

                                //res = responseFromServer.Replace("{", "[{").Replace("}", "}]");
                                //dtValue = (DataTable)JsonConvert.DeserializeObject(res, (typeof(DataTable)));

                                JObject jObject = JObject.Parse(responseFromServer);

                                ResponseCode = (string)jObject.SelectToken("ResponseCode");
                                Request_Input = (string)jObject.SelectToken("Request_Input");
                                ApprovalCode = (string)jObject.SelectToken("ApprovalCode");
                                RRN_No = (string)jObject.SelectToken("RRN_No");
                                Card_Num = (string)jObject.SelectToken("Card_Num");
                                Card_Type = (string)jObject.SelectToken("Card_Type");
                                CardHolder_Name = (string)jObject.SelectToken("CardHolder_Name");
                                Acquirer_Bank = (string)jObject.SelectToken("Acquirer_Bank");
                                Txn_Date = (string)jObject.SelectToken("Txn_Date");
                                Txn_Type = (string)jObject.SelectToken("Txn_Type");
                                BankInvoice_Num = (string)jObject.SelectToken("BankInvoice_Num");
                                Batch_Number = (string)jObject.SelectToken("Batch_Number");
                                Terminal_Id = (string)jObject.SelectToken("Terminal_Id");
                                Merchant_Id = (string)jObject.SelectToken("Merchant_Id");

                                if ((string)jObject.SelectToken("Amount") != "")
                                {
                                    Amount = (double)jObject.SelectToken("Amount");
                                }
                                else
                                {
                                    Amount = 0;
                                }


                                if (Convert.ToString(ResponseCode) != "00")
                                {
                                    errorMsg = "ERROR - " + Convert.ToString(ResponseCode) + " : TRANSACTION FAILED....";
                                }

                                vAmountInRs = Convert.ToDecimal(Amount) / 100;

                                try
                                {
                                    SqlParameter[] paramPOS = new SqlParameter[23];
                                    paramPOS[0] = new SqlParameter("@POST_ID", SqlDbType.Decimal);
                                    paramPOS[0].Direction = ParameterDirection.Output;
                                    paramPOS[1] = new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200);
                                    paramPOS[1].Direction = ParameterDirection.Output;

                                    paramPOS[2] = new SqlParameter("@POS_REQ_INP", (Convert.ToString(Request_Input) == null) ? (object)DBNull.Value : Convert.ToString(Request_Input));
                                    paramPOS[3] = new SqlParameter("@POS_REP_CODE", (Convert.ToString(ResponseCode) == null) ? (object)DBNull.Value : Convert.ToString(ResponseCode));
                                    paramPOS[4] = new SqlParameter("@POS_REP_MSG", (Convert.ToString(Request_Input) == null) ? (object)DBNull.Value : Convert.ToString(Request_Input));
                                    paramPOS[5] = new SqlParameter("@POS_APP_CODE", (Convert.ToString(ApprovalCode) == null) ? (object)DBNull.Value : Convert.ToString(ApprovalCode));
                                    paramPOS[6] = new SqlParameter("@POS_RRN_NO", (Convert.ToString(RRN_No) == null) ? (object)DBNull.Value : Convert.ToString(RRN_No));
                                    paramPOS[7] = new SqlParameter("@POS_AMT", (vAmountInRs == null) ? (object)DBNull.Value : vAmountInRs);
                                    paramPOS[8] = new SqlParameter("@POS_CARD_NO", (Convert.ToString(Card_Num) == null) ? (object)DBNull.Value : Convert.ToString(Card_Num));
                                    paramPOS[9] = new SqlParameter("@POS_CARD_TYPE", (Convert.ToString(Card_Type) == null) ? (object)DBNull.Value : Convert.ToString(Card_Type));
                                    paramPOS[10] = new SqlParameter("@POS_CARDHOLDER_NAME", (Convert.ToString(CardHolder_Name) == null) ? (object)DBNull.Value : Convert.ToString(CardHolder_Name));
                                    paramPOS[11] = new SqlParameter("@POS_ACQUIRER_BANK", (Convert.ToString(Acquirer_Bank) == null) ? (object)DBNull.Value : Convert.ToString(Acquirer_Bank));
                                    paramPOS[12] = new SqlParameter("@POS_TXN_DATE", (Txn_Date) == null ? (object)DBNull.Value : Txn_Date);
                                    paramPOS[13] = new SqlParameter("@POS_TXN_TYPE", (Convert.ToString(Txn_Type) == null) ? (object)DBNull.Value : Convert.ToString(Txn_Type));
                                    paramPOS[14] = new SqlParameter("@POS_BANK_INVNO", (Convert.ToString(BankInvoice_Num) == null) ? (object)DBNull.Value : Convert.ToString(BankInvoice_Num));
                                    paramPOS[15] = new SqlParameter("@POS_BATCH_NO", (Convert.ToString(Batch_Number) == null) ? (object)DBNull.Value : Convert.ToString(Batch_Number));
                                    paramPOS[16] = new SqlParameter("@POS_TERMINAL_ID", (Convert.ToString(Terminal_Id) == null) ? (object)DBNull.Value : Convert.ToString(Terminal_Id));
                                    paramPOS[17] = new SqlParameter("@POS_MERCHANT_ID", (Convert.ToString(Merchant_Id) == null) ? (object)DBNull.Value : Convert.ToString(Merchant_Id));
                                    paramPOS[18] = new SqlParameter("@POS_PARTY_PHONE_NO", (mr.MR_POSMOB == null) ? (object)DBNull.Value : mr.MR_POSMOB);
                                    paramPOS[19] = new SqlParameter("@POS_MR_STN", (mr.MR_BR_ID == null) ? (object)DBNull.Value : mr.MR_BR_ID);
                                    paramPOS[20] = new SqlParameter("@POS_MR_ID", (MR_ID == null) ? (object)DBNull.Value : MR_ID);
                                    paramPOS[21] = new SqlParameter("@POS_MR_TYPE_ID", (mr.MR_TYPE == null) ? (object)DBNull.Value : mr.MR_TYPE);
                                    paramPOS[22] = new SqlParameter("@POS_PA_ID", (mr.MR_PA_ID == null) ? (object)DBNull.Value : mr.MR_PA_ID);

                                    new DataAccess().InsertWithTransaction("[iFMS].[USP_INSERT_POS]", CommandType.StoredProcedure, out command, connection, transactionScope, paramPOS);
                                    decimal POST_ID = (decimal)command.Parameters["@POST_ID"].Value;
                                    string error_POS = (string)command.Parameters["@ERRORSTR"].Value;

                                    mr.POS_TRAN_NO = Convert.ToString(RRN_No);

                                    if (POST_ID == -1) { errorMsg = error_POS; }
                                }
                                catch (Exception ex)
                                {
                                    errorMsg = ex.Message;
                                    System.IO.File.AppendAllText(System.Web.HttpContext.Current.Server.MapPath("~/POSErrLog.txt"), System.DateTime.Now.ToString() + ',' + ex.Message + ',' + Convert.ToString(Request_Input) + ',' + Convert.ToString(ResponseCode) + ',' + Convert.ToString(ApprovalCode) + ',' + Convert.ToString(RRN_No) + ',' + Convert.ToString(vAmountInRs) + ',' + Convert.ToString(Card_Num) + ',' + Convert.ToString(Card_Type) + ',' + Convert.ToString(CardHolder_Name) + ',' + Convert.ToString(Acquirer_Bank) + ',' + Convert.ToString(Txn_Date) + ',' + Convert.ToString(Txn_Type) + ',' + Convert.ToString(BankInvoice_Num) + ',' + Convert.ToString(Batch_Number) + ',' + Convert.ToString(Terminal_Id) + ',' + Convert.ToString(Merchant_Id) + ',' + mr.MR_POSMOB + ',' + mr.MR_BR_NAME + ',' + mr.MR_MANUAL_NO + ',' + mr.MR_TYPE_CODE + ',' + mr.MR_P_NAME + Environment.NewLine);
                                }
                            }
                            catch (Exception ex)
                            {
                                errorMsg = ex.Message;
                                System.IO.File.AppendAllText(System.Web.HttpContext.Current.Server.MapPath("~/POSErrLog.txt"), System.DateTime.Now.ToString() + ',' + ex.Message + ',' + Convert.ToString(Request_Input) + ',' + Convert.ToString(ResponseCode) + ',' + Convert.ToString(ApprovalCode) + ',' + Convert.ToString(RRN_No) + ',' + Convert.ToString(vAmountInRs) + ',' + Convert.ToString(Card_Num) + ',' + Convert.ToString(Card_Type) + ',' + Convert.ToString(CardHolder_Name) + ',' + Convert.ToString(Acquirer_Bank) + ',' + Convert.ToString(Txn_Date) + ',' + Convert.ToString(Txn_Type) + ',' + Convert.ToString(BankInvoice_Num) + ',' + Convert.ToString(Batch_Number) + ',' + Convert.ToString(Terminal_Id) + ',' + Convert.ToString(Merchant_Id) + ',' + mr.MR_POSMOB + ',' + mr.MR_BR_NAME + ',' + mr.MR_MANUAL_NO + ',' + mr.MR_TYPE_CODE + ',' + mr.MR_P_NAME + Environment.NewLine);
                            }
                        }
                        //23-07-2021
                        // POS - TRANS (END HERE)

                        //Added By : Pramesh Kumar Vishwakarma,Date:23/09/2022
                        if (mr.MR_TYPE_CODE == "11" && mr.MR_DOC_TYPE == 3 && errorMsg == "")
                        {
                            SqlParameter[] param_Fleet =
                            { 
                                new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                                ,new SqlParameter("@MR_FIC_ID", SqlDbType.Decimal) 
                                ,new SqlParameter("@MR_FIC_MR_ID", MR_ID) 
                                ,new SqlParameter("@MR_FIC_FL_ID",( mr.MR_FLEET_ID == null) ? (object)DBNull.Value : mr.MR_FLEET_ID) 
                                ,new SqlParameter("@MR_FIC_FL_CODE",( mr.MR_FLEET_CODE == null) ? (object)DBNull.Value : mr.MR_FLEET_CODE) 
                                ,new SqlParameter("@MR_FIC_FL_VEHNO",( mr.MR_VEH_NO == null) ? (object)DBNull.Value : mr.MR_VEH_NO) 
                            };

                            param_Fleet[0].Direction = ParameterDirection.Output;
                            param_Fleet[1].Direction = ParameterDirection.Output;

                            new DataAccess().InsertWithTransaction("[iFMS].[USP_INSERT_MR_FLEET_INS_CLAIM]", CommandType.StoredProcedure, out command, connection, transactionScope, param_Fleet);
                            string error_Fleet = (string)command.Parameters["@ERRORSTR"].Value;
                            decimal MR_FIC_ID = (decimal)command.Parameters["@MR_FIC_ID"].Value;
                            if (MR_FIC_ID == -1) { errorMsg = error_Fleet; }
                        }
                        //End

                        //begin Insert distributed cash amount, added by pramesh,30-12-2022
                        if (mr.BILL_CNS_DTL_LIST_FOR_CASH != null && (mr.MR_DOC_TYPE == 1 || mr.MR_DOC_TYPE == 2) && mr.REC_PAY_MODE_CASH == true && mr.REC_PAY_MODE_CHQ == true && errorMsg == "")
                        {
                            foreach (CN_OR_BILL_DTL cn in mr.BILL_CNS_DTL_LIST_FOR_CASH)
                            {
                                if ((cn.CN_OR_BILL_ID) > 0 && (cn.NET_RECD_AMT ?? 0) > 0)
                                {
                                    SqlParameter[] paramCash =
                                    {
                                         new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                                        ,new SqlParameter("@MR_PYD_ID", SqlDbType.Decimal) 
                                        ,new SqlParameter("@MR_PYD_MR_ID", (MR_ID == null) ? (object)DBNull.Value : MR_ID) 
                                        
                                        ,new SqlParameter("@MR_PYD_CN_ID",(mr.MR_DOC_TYPE == 2) ? (object)DBNull.Value : cn.CN_OR_BILL_ID) 
                                        ,new SqlParameter("@MR_PYD_BILL_ID",(mr.MR_DOC_TYPE == 1) ? (object)DBNull.Value : cn.CN_OR_BILL_ID) 

                                        ,new SqlParameter("@MR_PYD_FRT_AMT",( cn.CN_OR_BILL_FRT_AMT == null) ? (object)DBNull.Value : cn.CN_OR_BILL_FRT_AMT) 
                                        ,new SqlParameter("@MR_PYD_DEMM_AMT",( cn.MR_DEMM_AMT== null) ? (object)DBNull.Value : cn.MR_DEMM_AMT) 
                                        ,new SqlParameter("@MR_PYD_HNDL_AMT",( cn.MR_HNDL_AMT == null) ? (object)DBNull.Value : cn.MR_HNDL_AMT) 
                                        ,new SqlParameter("@MR_PYD_OCT_AMT",( cn.MR_OCT_AMT == null) ? (object)DBNull.Value : cn.MR_OCT_AMT) 
                                        ,new SqlParameter("@MR_PYD_OCS_AMT",( cn.MR_OCS_AMT == null) ? (object)DBNull.Value : cn.MR_OCS_AMT) 
                                        ,new SqlParameter("@MR_PYD_DLVCH_AMT",( cn.MR_DLVCH_AMT == null) ? (object)DBNull.Value : cn.MR_DLVCH_AMT) 
                                        ,new SqlParameter("@MR_PYD_MISC_AMT",( cn.MR_MISC_AMT == null) ? (object)DBNull.Value : cn.MR_MISC_AMT) 
                                        ,new SqlParameter("@MR_PYD_OTH_AMT",( cn.MR_OTH_AMT == null) ? (object)DBNull.Value : cn.MR_OTH_AMT) 
                                        ,new SqlParameter("@MR_PYD_OPMC_AMT",( cn.MR_OPMC_AMT == null) ? (object)DBNull.Value : cn.MR_OPMC_AMT) 
                                        ,new SqlParameter("@MR_PYD_GTX_AMT",( cn.MR_GTX_AMT == null) ? (object)DBNull.Value : cn.MR_GTX_AMT) 
                                        ,new SqlParameter("@MR_PYD_CPE_AMT",( cn.MR_CPE_AMT == null) ? (object)DBNull.Value : cn.MR_CPE_AMT) 
                                        ,new SqlParameter("@MR_PYD_GDN_CHRG_AMT",( cn.MR_GDN_CHRG_AMT == null) ? (object)DBNull.Value : cn.MR_GDN_CHRG_AMT) 
                                        ,new SqlParameter("@MR_PYD_BFD_AMT",( cn.MR_BFD_AMT == null) ? (object)DBNull.Value : cn.MR_BFD_AMT) 
                                        ,new SqlParameter("@MR_PYD_SUB_TOTAL_AMT",( cn.MR_SUB_TOTAL_AMT == null) ? (object)DBNull.Value : cn.MR_SUB_TOTAL_AMT) 
                                        ,new SqlParameter("@MR_PYD_TOTAL_AMT",( cn.MR_TOTAL_AMT == null) ? (object)DBNull.Value : cn.MR_TOTAL_AMT) 
                                        ,new SqlParameter("@MR_PYD_STAX_FRT",( cn.MR_STAX_FRT == null) ? (object)DBNull.Value : cn.MR_STAX_FRT) 
                                        ,new SqlParameter("@MR_PYD_STAX",( cn.MR_STAX == null) ? (object)DBNull.Value : cn.MR_STAX) 
                                        ,new SqlParameter("@MR_PYD_CLAM_AMT", ( cn.MRBFD_CLAM_AMT == null) ? (object)DBNull.Value : cn.MRBFD_CLAM_AMT) 
                                        ,new SqlParameter("@MR_PYD_RECO_AMT",( cn.MRBFD_RECO_AMT== null) ? (object)DBNull.Value : cn.MRBFD_RECO_AMT) 
                                        ,new SqlParameter("@MR_PYD_NREC_AMT",( cn.MRBFD_NREC_AMT  == null) ? (object)DBNull.Value : cn.MRBFD_NREC_AMT  ) 
                                        ,new SqlParameter("@MR_PYD_EMD_AMT",( cn.MRBFD_EMD_AMT == null) ? (object)DBNull.Value : cn.MRBFD_EMD_AMT) 
                                        ,new SqlParameter("@MR_PYD_SD_AMT",( cn.MRBFD_SD_AMT == null) ? (object)DBNull.Value : cn.MRBFD_SD_AMT) 
                                        ,new SqlParameter("@MR_PYD_TDS_AMT",( cn.MR_TDS_AMT == null) ? (object)DBNull.Value : cn.MR_TDS_AMT) 
                                    };

                                    paramCash[0].Direction = ParameterDirection.Output;
                                    paramCash[1].Direction = ParameterDirection.Output;

                                    new DataAccess().InsertWithTransaction("[iTMS].[USP_INSERT_MR_PAY_DTLS]", CommandType.StoredProcedure, out command, connection, transactionScope, paramCash, 90);
                                    var MR_PYD_ID = (decimal)command.Parameters["@MR_PYD_ID"].Value;
                                    string error_cash = (string)command.Parameters["@ERRORSTR"].Value;
                                    if (MR_PYD_ID == -1) { errorMsg = error_cash; break; }
                                }
                            }
                        }
                        //end Insert distributed cash amount
                    }

                    if (errorMsg == "")
                    {
                        transactionScope.Commit();
                    }
                    else
                    {
                        transactionScope.Rollback();
                    }
                }
                catch (Exception)
                {
                    errorMsg = "Error: Exception occured.";
                    transactionScope.Rollback();
                }
                finally
                {
                    connection.Close();
                }
            }
            return errorMsg;
        }

        public void FILL_RETURN_MR_POSTING_DEAILS(ref Voucher_Posting _posting, MR_Receipt mr)
        {
            List<Voucher_Dtls> voucherDtl = new List<Voucher_Dtls>();

            // voucher header data

            _posting.V_DATE = Convert.ToDateTime(mr.MR_DATE);
            _posting.V_VT_ID = 2;//For Payment
            _posting.V_PAY_MODE = Convert.ToInt32(mr.MR_PAY_MODE);

            _posting.V_NARRATION = mr.MR_REMARKS;

            _posting.V_PAY_TO = 3;
            _posting.V_IN_FAVOUR_ID = Convert.ToDecimal(mr.MR_PA_ID);
            _posting.V_IN_FAVOUR = Convert.ToString(mr.MR_P_NAME);
            _posting.V_IS_AUTO = true;
            _posting.V_CMP_ID = 2;
            _posting.V_BR_ID = Convert.ToInt32(mr.MR_BR_ID);
            // vouvher details data

            //L_ID	 L_NAME
            //1	     Cash in Bank
            //2	     Cash in Hand
            if (Convert.ToInt32(mr.MR_PAY_MODE) == 1)
            {
                //Cash
                voucherDtl.Add(new Voucher_Dtls { VD_SL_NO = (voucherDtl.Count() + 1), VD_L_ID = 2, VD_TRANS_TYPE = "Dr", VD_AMOUNT = mr.MR_TOTAL_AMT, VD_CCAT = 0, VD_CCEN = 0 });
            }
            else if (Convert.ToInt32(mr.MR_PAY_MODE) == 2)
            {
                //Bnak
                voucherDtl.Add(new Voucher_Dtls { VD_SL_NO = (voucherDtl.Count() + 1), VD_L_ID = 1, VD_TRANS_TYPE = "Dr", VD_AMOUNT = mr.MR_TOTAL_AMT, VD_CCAT = 0, VD_CCEN = 0 });
            }
            else if (Convert.ToInt32(mr.MR_PAY_MODE) == 3)
            {
                //Both
                voucherDtl.Add(new Voucher_Dtls { VD_SL_NO = (voucherDtl.Count() + 1), VD_L_ID = 2, VD_TRANS_TYPE = "Dr", VD_AMOUNT = mr.CASH_AMT, VD_CCAT = 0, VD_CCEN = 0 });

                voucherDtl.Add(new Voucher_Dtls { VD_SL_NO = (voucherDtl.Count() + 1), VD_L_ID = 1, VD_TRANS_TYPE = "Dr", VD_AMOUNT = mr.CHQ_RTGS_DD_AMT, VD_CCAT = 0, VD_CCEN = 0 });
            }
            //Vendor/Supplier
            voucherDtl.Add(new Voucher_Dtls { VD_SL_NO = (voucherDtl.Count() + 1), VD_L_ID = 4, VD_TRANS_TYPE = "Cr", VD_AMOUNT = mr.MR_TOTAL_AMT, VD_CCAT = 0, VD_CCEN = 0 });

            _posting.VoucherDtls_List = voucherDtl;
        }

        #endregion

        #region MR List
        public List<Mr_Data_List> SELECT_MR_DATA_LIST(Mr_List mr)
        {
            SqlParameter[] param = { 
                                       new SqlParameter("@MR_BR_ID", mr.SEARCH_MR_BR_ID??0),
                                       new SqlParameter("@MR_NO", mr.SEARCH_MR_NO??""),
                                       new SqlParameter("@FDT", mr.FROM_MR_DATE1),
                                       new SqlParameter("@EDT", mr.TO_MR_DATE1),
                                       new SqlParameter("@MR_TYPE_ID", mr.SEARCH_MR_TYPE_ID??0),
                                       new SqlParameter("@PARTY_ADD_ID", mr.SEARCH_PARTY_ADD_ID??0),
                                   };

            DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iTMS].[USP_SELECT_MR_DATA_LIST]", CommandType.StoredProcedure, param);

            List<Mr_Data_List> _list = new List<Mr_Data_List>();
            DataTable dt = ds.Tables[0];
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    _list.Add(new Mr_Data_List
                    {
                        MR_ID = Convert.ToDecimal(row["MR_ID"]),
                        MR_NO = Convert.ToString(row["MR_NO"]),
                        MR_DATE1 = Convert.ToString(row["MR_DATE1"]),
                        MR_BR_NAME = Convert.ToString(row["MR_BR_NAME"]),
                        MR_MANUAL_NO = Convert.ToString(row["MR_MANUAL_NO"]),
                        MR_TYPE_TXT = Convert.ToString(row["MR_TYPE"]),
                        MR_PARTY_CODE = Convert.ToString(row["MR_PARTY_CODE"]),
                        MR_PARTY_NAME = Convert.ToString(row["MR_PARTY_NAME"]),
                        MR_PARTY_ID = Convert.ToString(row["MR_PARTY_ID"]),
                        MR_PARTY_ADD_ID = Convert.ToString(row["MR_PARTY_ADD_ID"]),
                        MR_TOTAL_AMT = Convert.ToString(row["MR_TOTAL_AMT"]),
                        MR_PRINT_DATE1 = Convert.ToString(row["MR_PRINT_DATE"]),
                        MR_STATUS = Convert.ToString(row["MR_STATUS"]),
                    });
                }
            }

            return _list;
        }
        #endregion

        #region On-Account Adjustment

        public List<MR_OR_ADV_DTLS> SELECT_MR_OR_ADV_LIST(int adjThrough, int brId, decimal paId, string cbsdate)
        {
            SqlParameter[] param = {
                                       new SqlParameter("@ADJ_THROGH", adjThrough),
                                       new SqlParameter("@BR_ID", brId),
                                       new SqlParameter("@PARTY_ADD_ID", paId),
                                       new SqlParameter("@CBS_DATE", cbsdate),
                                   };

            DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iFMS].[USP_SELECT_MR_OR_ADV_LIST]", CommandType.StoredProcedure, param);

            List<MR_OR_ADV_DTLS> _list = new List<MR_OR_ADV_DTLS>();
            DataTable dt = ds.Tables[0];
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Nullable<DateTime> mrDate = null;
                    if (row["MR_ADV_DATE"] != DBNull.Value)
                    {
                        mrDate = Convert.ToDateTime(row["MR_ADV_DATE"]);
                    }

                    _list.Add(new MR_OR_ADV_DTLS
                    {
                        MR_ADV_ID = Convert.ToDecimal(row["MR_ADV_ID"] == DBNull.Value ? "" : row["MR_ADV_ID"]),
                        MR_ADV_NO = Convert.ToString(row["MR_ADV_NO"]),
                        MR_ADV_BR_NAME = Convert.ToString(row["MR_ADV_BR_NAME"]),

                        MR_ADV_BR_ID = Convert.ToInt32(row["MR_ADV_BR_ID"] == DBNull.Value ? "" : row["MR_ADV_BR_ID"]),

                        MR_ADV_DATE = mrDate,
                        MR_ADV_DATE1 = Convert.ToString(row["MR_ADV_DATE1"]),

                        MR_ADV_P_CODE = Convert.ToString(row["MR_ADV_P_CODE"] == DBNull.Value ? "" : row["MR_ADV_P_CODE"]),
                        MR_ADV_P_NAME = Convert.ToString(row["MR_ADV_P_NAME"] == DBNull.Value ? "" : row["MR_ADV_P_NAME"]),
                        MR_ADV_P_ID = Convert.ToDecimal(row["MR_ADV_P_ID"] == DBNull.Value ? "0" : row["MR_ADV_P_ID"]),
                        MR_ADV_PA_ID = Convert.ToDecimal(row["MR_ADV_PA_ID"] == DBNull.Value ? "0" : row["MR_ADV_PA_ID"]),

                        MR_ADV_BAL_AMT = Convert.ToDecimal(row["MR_ADV_BAL_AMT"] == DBNull.Value ? "0" : row["MR_ADV_BAL_AMT"]),
                    });
                }
            }

            return _list;
        }

        public List<ddlClass> SELECT_MR_OR_ADV_EXTENDER(int adjThrough, int brId, decimal paId, string searchText)
        {
            SqlParameter[] param = {
                                       new SqlParameter("@ADJ_THROGH", adjThrough),
                                       new SqlParameter("@BR_ID", brId),
                                       new SqlParameter("@PARTY_ADD_ID", paId),
                                       new SqlParameter("@DOCNO", searchText),
                                   };

            DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iFMS].[USP_SELECT_MR_OR_ADV_EXTENDER]", CommandType.StoredProcedure, param);

            List<ddlClass> _list = new List<ddlClass>();
            DataTable dt = ds.Tables[0];
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    _list.Add(new ddlClass
                    {
                        ddlValue = Convert.ToString(row["MR_ADV_ID"] == DBNull.Value ? "" : row["MR_ADV_ID"]),
                        ddlText = Convert.ToString(row["MR_ADV_NO"] == DBNull.Value ? "" : row["MR_ADV_NO"])
                    });
                }
            }

            return _list;
        }

        public MR_OR_ADV_DTLS SELECT_MR_OR_ADV_DATA(int adjThrough, decimal docId, int brId, decimal paId)
        {
            SqlParameter[] param = {
                                       new SqlParameter("@ADJ_THROGH", adjThrough),
                                       new SqlParameter("@DOC_ID", docId),
                                       new SqlParameter("@BR_ID", brId),
                                       new SqlParameter("@PARTY_ADD_ID", paId)
                                   };

            DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iFMS].[USP_SELECT_MR_OR_ADV_DATA]", CommandType.StoredProcedure, param);

            MR_OR_ADV_DTLS data = new MR_OR_ADV_DTLS();
            DataTable dt = ds.Tables[0];
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["MR_ADV_DATE"] != DBNull.Value)
                    {
                        data.MR_ADV_DATE = Convert.ToDateTime(row["MR_ADV_DATE"]);
                    }

                    data.MR_ADV_ID = Convert.ToDecimal(row["MR_ADV_ID"] == DBNull.Value ? "0" : row["MR_ADV_ID"]);
                    data.MR_ADV_NO = Convert.ToString(row["MR_ADV_NO"] == DBNull.Value ? "" : row["MR_ADV_NO"]);
                    data.MR_ADV_BR_NAME = Convert.ToString(row["MR_ADV_BR_NAME"] == DBNull.Value ? "" : row["MR_ADV_BR_NAME"]);
                    data.MR_ADV_BR_ID = Convert.ToInt32(row["MR_ADV_BR_ID"] == DBNull.Value ? "0" : row["MR_ADV_BR_ID"]);

                    data.MR_ADV_DATE1 = Convert.ToString(row["MR_ADV_DATE1"] == DBNull.Value ? "" : row["MR_ADV_DATE1"]);

                    data.MR_ADV_P_CODE = Convert.ToString(row["MR_ADV_P_CODE"] == DBNull.Value ? "" : row["MR_ADV_P_CODE"]);
                    data.MR_ADV_P_NAME = Convert.ToString(row["MR_ADV_P_NAM"] == DBNull.Value ? "" : row["MR_ADV_P_NAM"]);
                    data.MR_ADV_P_ID = Convert.ToDecimal(row["MR_ADV_P_ID"] == DBNull.Value ? "0" : row["MR_ADV_P_ID"]);
                    data.MR_ADV_PA_ID = Convert.ToDecimal(row["MR_ADV_PA_ID"] == DBNull.Value ? "0" : row["MR_ADV_PA_ID"]);
                    data.MR_ADV_BAL_AMT = Convert.ToDecimal(row["MR_ADV_BAL_AMT"] == DBNull.Value ? "0" : row["MR_ADV_BAL_AMT"]);
                    data.CRAS_STATUS = Convert.ToString(row["CRAS_STATUS"] == DBNull.Value ? "0" : row["CRAS_STATUS"]);
                    data.MSG = Convert.ToString(row["MSG"] == DBNull.Value ? "" : row["MSG"]);
                }
            }

            return data;
        }

        public List<ddlClass> SELECT_BILL_OR_CN_LIST_FOR_ONACC_ADJ_EXTENDER(int brId, string date, decimal paId, int docType, string searchText)
        {
            SqlParameter[] param = {
                                       new SqlParameter("@BR_ID", brId),
                                       new SqlParameter("@EDT", date),
                                       new SqlParameter("@PA_ID", paId),
                                       new SqlParameter("@DOC_TYPE", docType),
                                       new SqlParameter("@DOC_NO", searchText),
                                   };

            DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iFMS].[USP_SELECT_BILL_OR_CN_LIST_FOR_ONACC_ADJ]", CommandType.StoredProcedure, param);

            List<ddlClass> _list = new List<ddlClass>();
            DataTable dt = ds.Tables[0];
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    _list.Add(new ddlClass
                    {
                        ddlValue = Convert.ToString(row["CN_ID"] == DBNull.Value ? "" : row["CN_ID"]),
                        ddlText = Convert.ToString(row["CN_NO"] == DBNull.Value ? "" : row["CN_NO"])
                    });
                }
            }

            return _list;
        }

        public string INSERT_MR_ADJ(On_Account_Adjustment onAcc)
        {
            string errorMsg = "";

            decimal MRBFD_CLAM_AMT = 0;
            decimal MRBFD_RECO_AMT = 0;
            decimal MRBFD_NREC_AMT = 0;
            decimal MRBFD_EMD_AMT = 0;
            decimal MRBFD_SD_AMT = 0;
            decimal MR_TDS_ON_AMT = 0;
            decimal MR_TDS_AMT = 0;

            decimal MRBFD_ID = 0;

            using (var connection = new SqlConnection(sqlConnection.GetConnectionString()))
            {
                connection.Open();
                SqlCommand command;
                SqlTransaction transactionScope = null;
                transactionScope = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    SqlParameter[] param =
                    {
                        new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                        ,new SqlParameter("@MR_ID", SqlDbType.Decimal) 
                        ,new SqlParameter("@MR_NO", SqlDbType.VarChar, 15)
                        ,new SqlParameter("@MR_BR_ID", (onAcc.MR_BR_ID == null) ? (object)DBNull.Value : onAcc.MR_BR_ID) 
                        ,new SqlParameter("@MR_P_ID",( onAcc.MR_P_ID == null) ? (object)DBNull.Value : onAcc.MR_P_ID) 
                        ,new SqlParameter("@MR_PA_ID",( onAcc.MR_PA_ID == null) ? (object)DBNull.Value : onAcc.MR_PA_ID) 
                        ,new SqlParameter("@MR_P_GSTNO",( onAcc.MR_P_GSTNO == null) ? (object)DBNull.Value : onAcc.MR_P_GSTNO)  
                        ,new SqlParameter("@MR_TYPE",( onAcc.MR_TYPE == null) ? (object)DBNull.Value : onAcc.MR_TYPE) 
                        ,new SqlParameter("@MR_DOC_TYPE",( onAcc.ADJ_DOC_TYPE == null) ? (object)DBNull.Value : onAcc.ADJ_DOC_TYPE) 
                        ,new SqlParameter("@MR_DATE",( onAcc.MR_DATE == null) ? (object)DBNull.Value : onAcc.MR_DATE) 
                        ,new SqlParameter("@MR_SUFFIX",( onAcc.MR_SUFFIX == null) ? (object)DBNull.Value : onAcc.MR_SUFFIX) 
                        ,new SqlParameter("@MR_FRT_AMT",( onAcc.MR_TOTAL_FRT == null) ? (object)DBNull.Value : onAcc.MR_TOTAL_FRT) 
                        ,new SqlParameter("@MR_DEMM_AMT",( onAcc.MR_DEMM_AMT == null) ? (object)DBNull.Value : onAcc.MR_DEMM_AMT) 
                        ,new SqlParameter("@MR_HNDL_AMT",( onAcc.MR_HNDL_AMT == null) ? (object)DBNull.Value : onAcc.MR_HNDL_AMT) 
                        ,new SqlParameter("@MR_OCT_AMT",( onAcc.MR_OCT_AMT == null) ? (object)DBNull.Value : onAcc.MR_OCT_AMT) 
                        ,new SqlParameter("@MR_OCS_AMT",( onAcc.MR_OCS_AMT == null) ? (object)DBNull.Value : onAcc.MR_OCS_AMT)  
                        ,new SqlParameter("@MR_DLVCH_AMT",( onAcc.MR_DLVCH_AMT == null) ? (object)DBNull.Value : onAcc.MR_DLVCH_AMT) 
                        ,new SqlParameter("@MR_MISC_AMT",( onAcc.MR_MISC_AMT == null) ? (object)DBNull.Value : onAcc.MR_MISC_AMT) 
                        ,new SqlParameter("@MR_OTH_AMT",( onAcc.MR_OTH_AMT == null) ? (object)DBNull.Value : onAcc.MR_OTH_AMT) 
                        ,new SqlParameter("@MR_OPMC_AMT",( onAcc.MR_OPMC_AMT == null) ? (object)DBNull.Value : onAcc.MR_OPMC_AMT)  
                        ,new SqlParameter("@MR_GTX_AMT",( onAcc.MR_GTX_AMT == null) ? (object)DBNull.Value : onAcc.MR_GTX_AMT) 
                        ,new SqlParameter("@MR_CPE_AMT",( onAcc.MR_CPE_AMT  == null) ? (object)DBNull.Value : onAcc.MR_CPE_AMT ) 
                        ,new SqlParameter("@MR_GDN_CHRG_AMT",( onAcc.MR_GDN_CHRG_AMT  == null) ? (object)DBNull.Value : onAcc.MR_GDN_CHRG_AMT ) 
                        ,new SqlParameter("@MR_SUB_TOTAL_AMT",( onAcc.MR_SUB_TOTAL_AMT  == null) ? (object)DBNull.Value : onAcc.MR_SUB_TOTAL_AMT ) 
                        ,new SqlParameter("@MR_BFD_AMT",( onAcc.MR_BFD_AMT  == null) ? (object)DBNull.Value : onAcc.MR_BFD_AMT ) 
                        ,new SqlParameter("@MR_TOTAL_AMT",( onAcc.MR_NET_RECEIVED  == null) ? (object)DBNull.Value : onAcc.MR_NET_RECEIVED ) 
                        ,new SqlParameter("@MR_CN_AMT_EXTRA",( onAcc.MR_CN_AMT_EXTRA  == null) ? (object)DBNull.Value : onAcc.MR_CN_AMT_EXTRA ) 
                        ,new SqlParameter("@MR_PAY_MODE",(object)DBNull.Value ) 
                        ,new SqlParameter("@MR_CBS_DATE",( onAcc.MR_CBS_DATE  == null) ? (object)DBNull.Value : onAcc.MR_CBS_DATE ) 
                        ,new SqlParameter("@MR_CBS_BR_ID",( onAcc.MR_CBS_BR_ID  == null) ? (object)DBNull.Value : onAcc.MR_CBS_BR_ID ) 
                        ,new SqlParameter("@MR_OLD_ID",( onAcc.MR_OLD_ID  == null) ? (object)DBNull.Value : onAcc.MR_OLD_ID ) 
                        ,new SqlParameter("@MR_DCR_ID",( onAcc.MR_DCR_ID  == null) ? (object)DBNull.Value : onAcc.MR_DCR_ID ) 
                        ,new SqlParameter("@MR_DESC",( onAcc.MR_DESC  == null) ? (object)DBNull.Value : onAcc.MR_DESC ) 
                        ,new SqlParameter("@MR_STAX_FRT",( onAcc.MR_STAX_FRT  == null) ? (object)DBNull.Value : onAcc.MR_STAX_FRT ) 
                        ,new SqlParameter("@MR_STAX",( onAcc.MR_STAX  == null) ? (object)DBNull.Value : onAcc.MR_STAX ) 
                        ,new SqlParameter("@MR_USER_TYPE",( onAcc.MR_ADJ_ADD_BY_TYPE  == null) ? (object)DBNull.Value : onAcc.MR_ADJ_ADD_BY_TYPE ) 
                        ,new SqlParameter("@MR_ADD_BY",( onAcc.MR_ADJ_ADD_BY  == null) ? (object)DBNull.Value : onAcc.MR_ADJ_ADD_BY ) 
                        ,new SqlParameter("@MR_REMARKS",( onAcc.MR_REMARKS  == null) ? (object)DBNull.Value : onAcc.MR_REMARKS ) 
                        
                        ,new SqlParameter("@FROM_CBS_DATE",( onAcc.FROM_CBS_DATE  == null) ? (object)DBNull.Value : onAcc.FROM_CBS_DATE ) 
                        ,new SqlParameter("@CBS_OPN_DATE",( onAcc.CBS_OPN_DATE  == null) ? (object)DBNull.Value : onAcc.CBS_OPN_DATE ) 
                        ,new SqlParameter("@CBS_DAYS",( onAcc.CBS_SRP_PD  == null) ? (object)DBNull.Value : onAcc.CBS_SRP_PD )
                        ,new SqlParameter("@MR_PREPARED_BY",( onAcc.EMP_ID  == null) ? (object)DBNull.Value : onAcc.EMP_ID )
                        ,new SqlParameter("@MR_ADJ_THRU",( onAcc.ADJ_THROUGH  == null) ? (object)DBNull.Value : onAcc.ADJ_THROUGH )
                        ,new SqlParameter("@MR_IBT_NO",( onAcc.MR_IBT_NO  == null) ? (object)DBNull.Value : onAcc.MR_IBT_NO )
                    };

                    param[0].Direction = ParameterDirection.Output;
                    param[1].Direction = ParameterDirection.Output;
                    param[2].Direction = ParameterDirection.Output;

                    new DataAccess().InsertWithTransaction("[iTMS].[USP_INSERT_MR_FOR_ON_ACC_ADJ]", CommandType.StoredProcedure, out command, connection, transactionScope, param);
                    decimal MR_ID = (decimal)command.Parameters["@MR_ID"].Value;
                    string MR_NO = (string)command.Parameters["@MR_NO"].Value;
                    string error_1 = (string)command.Parameters["@ERRORSTR"].Value;
                    onAcc.MR_ADJ_CV_MR_ID = MR_ID;
                    onAcc.MR_NO = MR_NO;
                    if (MR_ID == -1) { errorMsg = error_1; }

                    if (MR_ID > 0)
                    {
                        if (onAcc.BILL_CNS_DTL_LIST != null && (onAcc.ADJ_DOC_TYPE == 2))
                        {
                            // 2 for Bill
                            SqlParameter[] param2 = new SqlParameter[23];
                            SqlParameter[] paramBFD = new SqlParameter[17];
                            SqlParameter[] paramCNs_BFD = new SqlParameter[10];

                            foreach (CN_OR_BILL_DTL cn in onAcc.BILL_CNS_DTL_LIST)
                            {
                                if ((cn.CN_OR_BILL_ID) > 0)
                                {
                                    param2[0] = new SqlParameter("@MR_CN_ID", SqlDbType.Decimal);
                                    param2[0].Direction = ParameterDirection.Output;
                                    param2[1] = new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200);
                                    param2[1].Direction = ParameterDirection.Output;

                                    param2[2] = new SqlParameter("@MR_ID", MR_ID);
                                    param2[3] = new SqlParameter("@BILL_ID", (cn.CN_OR_BILL_ID == null) ? (object)DBNull.Value : cn.CN_OR_BILL_ID);
                                    param2[4] = new SqlParameter("@BILL_FRT_AMT", (cn.CN_OR_BILL_FRT_AMT == null) ? (object)DBNull.Value : cn.CN_OR_BILL_FRT_AMT);
                                    param2[5] = new SqlParameter("@MR_DEMM_AMT", (cn.MR_DEMM_AMT == null) ? (object)DBNull.Value : cn.MR_DEMM_AMT);
                                    param2[6] = new SqlParameter("@MR_HNDL_AMT", (cn.MR_HNDL_AMT == null) ? (object)DBNull.Value : cn.MR_HNDL_AMT);
                                    param2[7] = new SqlParameter("@MR_OCT_AMT", (cn.MR_OCT_AMT == null) ? (object)DBNull.Value : cn.MR_OCT_AMT);
                                    param2[8] = new SqlParameter("@MR_OCS_AMT", (cn.MR_OCS_AMT == null) ? (object)DBNull.Value : cn.MR_OCS_AMT);
                                    param2[9] = new SqlParameter("@MR_DLVCH_AMT", (cn.MR_DLVCH_AMT == null) ? (object)DBNull.Value : cn.MR_DLVCH_AMT);
                                    param2[10] = new SqlParameter("@MR_MISC_AMT", (cn.MR_MISC_AMT == null) ? (object)DBNull.Value : cn.MR_MISC_AMT);
                                    param2[11] = new SqlParameter("@MR_OTH_AMT", (cn.MR_OTH_AMT == null) ? (object)DBNull.Value : cn.MR_OTH_AMT);
                                    param2[12] = new SqlParameter("@MR_OPMC_AMT", (cn.MR_OPMC_AMT == null) ? (object)DBNull.Value : cn.MR_OPMC_AMT);
                                    param2[13] = new SqlParameter("@MR_GTX_AMT", (cn.MR_GTX_AMT == null) ? (object)DBNull.Value : cn.MR_GTX_AMT);
                                    param2[14] = new SqlParameter("@MR_CPE_AMT", (cn.MR_CPE_AMT == null) ? (object)DBNull.Value : cn.MR_CPE_AMT);
                                    param2[15] = new SqlParameter("@MR_GDN_CHRG_AMT", (cn.MR_GDN_CHRG_AMT == null) ? (object)DBNull.Value : cn.MR_GDN_CHRG_AMT);
                                    param2[16] = new SqlParameter("@MR_SUB_TOTAL_AMT", (cn.MR_TOTAL_AMT == null) ? (object)DBNull.Value : cn.MR_TOTAL_AMT);
                                    param2[17] = new SqlParameter("@MR_BFD_AMT", (cn.TOT_DEDN_AMT == null) ? (object)DBNull.Value : cn.TOT_DEDN_AMT);
                                    param2[18] = new SqlParameter("@MR_TOTAL_AMT", (cn.NET_RECD_AMT == null) ? (object)DBNull.Value : cn.NET_RECD_AMT);
                                    param2[19] = new SqlParameter("@MR_STAX_FRT", (cn.MR_STAX_FRT == null) ? (object)DBNull.Value : cn.MR_STAX_FRT);
                                    param2[20] = new SqlParameter("@MR_STAX", (cn.MR_STAX == null) ? (object)DBNull.Value : cn.MR_STAX);
                                    param2[21] = new SqlParameter("@MR_COD_CQNO", (cn.MR_COD_CQNO == null) ? (object)DBNull.Value : cn.MR_COD_CQNO);
                                    param2[22] = new SqlParameter("@MR_COD_AMT", (cn.MR_COD_AMT == null) ? (object)DBNull.Value : cn.MR_COD_AMT);

                                    new DataAccess().InsertWithTransaction("[iTMS].[USP_INSERT_MR_BILL]", CommandType.StoredProcedure, out command, connection, transactionScope, param2);
                                    decimal MR_BILL_ID = (decimal)command.Parameters["@MR_CN_ID"].Value;
                                    string error_3 = (string)command.Parameters["@ERRORSTR"].Value;
                                    if (MR_BILL_ID == -1) { errorMsg = error_3; break; }

                                    //BFD 
                                    MRBFD_CLAM_AMT = cn.MRBFD_CLAM_AMT ?? 0;
                                    MRBFD_RECO_AMT = cn.MRBFD_RECO_AMT ?? 0;
                                    MRBFD_NREC_AMT = cn.MRBFD_NREC_AMT ?? 0;
                                    MRBFD_EMD_AMT = cn.MRBFD_EMD_AMT ?? 0;
                                    MRBFD_SD_AMT = cn.MRBFD_SD_AMT ?? 0;
                                    MR_TDS_ON_AMT = cn.MR_TDS_ON_AMT ?? 0;
                                    MR_TDS_AMT = cn.MR_TDS_AMT ?? 0;

                                    if ((MRBFD_CLAM_AMT + MRBFD_RECO_AMT + MRBFD_NREC_AMT + MRBFD_EMD_AMT + MRBFD_SD_AMT + MR_TDS_ON_AMT + MR_TDS_AMT) > 0 && MR_BILL_ID > 0)
                                    {
                                        paramBFD[0] = new SqlParameter("@MRBFD_ID", SqlDbType.Decimal);
                                        paramBFD[0].Direction = ParameterDirection.Output;
                                        paramBFD[1] = new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200);
                                        paramBFD[1].Direction = ParameterDirection.Output;

                                        paramBFD[2] = new SqlParameter("@MRBFD_MR_ID", MR_ID);
                                        paramBFD[3] = new SqlParameter("@MRBFD_BILL_ID", (cn.CN_OR_BILL_ID == null) ? (object)DBNull.Value : cn.CN_OR_BILL_ID);
                                        paramBFD[4] = new SqlParameter("@MRBFD_DOC_TYPE", (onAcc.ADJ_DOC_TYPE == null) ? (object)DBNull.Value : onAcc.ADJ_DOC_TYPE);
                                        paramBFD[5] = new SqlParameter("@MRBFD_CN_ID", (object)DBNull.Value);
                                        paramBFD[6] = new SqlParameter("@MRBFD_CLAM_AMT", (cn.MRBFD_CLAM_AMT == null) ? (object)DBNull.Value : cn.MRBFD_CLAM_AMT);
                                        paramBFD[7] = new SqlParameter("@MRBFD_RECO_AMT", (cn.MRBFD_RECO_AMT == null) ? (object)DBNull.Value : cn.MRBFD_RECO_AMT);
                                        paramBFD[8] = new SqlParameter("@MRBFD_NREC_AMT", (cn.MRBFD_NREC_AMT == null) ? (object)DBNull.Value : cn.MRBFD_NREC_AMT);
                                        paramBFD[9] = new SqlParameter("@MRBFD_BFDR_ID", (cn.MRBFD_BFDR_ID == null) ? (object)DBNull.Value : cn.MRBFD_BFDR_ID);
                                        paramBFD[10] = new SqlParameter("@MRBFD_EMD_AMT", (cn.MRBFD_EMD_AMT == null) ? (object)DBNull.Value : cn.MRBFD_EMD_AMT);
                                        paramBFD[11] = new SqlParameter("@MRBFD_SD_AMT", (cn.MRBFD_SD_AMT == null) ? (object)DBNull.Value : cn.MRBFD_SD_AMT);
                                        paramBFD[12] = new SqlParameter("@MRBFD_TDS_ON_AMT", (cn.MR_TDS_ON_AMT == null) ? (object)DBNull.Value : cn.MR_TDS_ON_AMT);
                                        paramBFD[13] = new SqlParameter("@MRBFD_TDS_PER", (cn.MR_TDS_PER == null) ? (object)DBNull.Value : cn.MR_TDS_PER);
                                        paramBFD[14] = new SqlParameter("@MRBFD_TDS_AMT", (cn.MR_TDS_AMT == null) ? (object)DBNull.Value : cn.MR_TDS_AMT);
                                        paramBFD[15] = new SqlParameter("@MRBFD_MR_CN_ID", DBNull.Value);
                                        paramBFD[16] = new SqlParameter("@MRBFD_MR_BILL_ID", (MR_BILL_ID == null) ? (object)DBNull.Value : MR_BILL_ID);

                                        new DataAccess().InsertWithTransaction("[iTMS].[USP_INSERT_MR_BFD]", CommandType.StoredProcedure, out command, connection, transactionScope, paramBFD);
                                        MRBFD_ID = (decimal)command.Parameters["@MRBFD_ID"].Value;
                                        string error_BFD = (string)command.Parameters["@ERRORSTR"].Value;
                                        if (MRBFD_ID == -1) { errorMsg = error_BFD; break; }
                                    }

                                    if (cn.CNS_BFD_LIST != null)
                                    {
                                        foreach (MR_CNS_BFD cnBFD in cn.CNS_BFD_LIST) // CNs BFD, 26-12-2020
                                        {
                                            if ((cnBFD.CN_ID ?? 0) > 0)
                                            {
                                                paramCNs_BFD[0] = new SqlParameter("@CNS_BFD_ID", SqlDbType.Decimal);
                                                paramCNs_BFD[0].Direction = ParameterDirection.Output;
                                                paramCNs_BFD[1] = new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200);
                                                paramCNs_BFD[1].Direction = ParameterDirection.Output;

                                                paramCNs_BFD[2] = new SqlParameter("@BFD_ID", MRBFD_ID);
                                                paramCNs_BFD[3] = new SqlParameter("@CN_ID", (cnBFD.CN_ID == null) ? (object)DBNull.Value : cnBFD.CN_ID);
                                                paramCNs_BFD[4] = new SqlParameter("@CLAM_AMT", (cnBFD.CLAM_AMT == null) ? (object)DBNull.Value : cnBFD.CLAM_AMT);
                                                paramCNs_BFD[5] = new SqlParameter("@RECO_AMT", (cnBFD.RECO_AMT == null) ? (object)DBNull.Value : cnBFD.RECO_AMT);
                                                paramCNs_BFD[6] = new SqlParameter("@NREC_AMT", (cnBFD.NREC_AMT == null) ? (object)DBNull.Value : cnBFD.NREC_AMT);
                                                paramCNs_BFD[7] = new SqlParameter("@BFDR_ID", (cnBFD.BFDR_ID == null) ? (object)DBNull.Value : cnBFD.BFDR_ID);
                                                paramCNs_BFD[8] = new SqlParameter("@EMD_AMT", (cnBFD.EMD_AMT == null) ? (object)DBNull.Value : cnBFD.EMD_AMT);
                                                paramCNs_BFD[9] = new SqlParameter("@SD_AMT", (cnBFD.SD_AMT == null) ? (object)DBNull.Value : cnBFD.SD_AMT);

                                                new DataAccess().InsertWithTransaction("[iFMS].[USP_INSERT_MR_CNS_BFD]", CommandType.StoredProcedure, out command, connection, transactionScope, paramCNs_BFD);
                                                decimal CNS_BFD_ID = (decimal)command.Parameters["@CNS_BFD_ID"].Value;
                                                string error_cns_bfd = (string)command.Parameters["@ERRORSTR"].Value;
                                                if (CNS_BFD_ID == -1) { errorMsg = error_cns_bfd; break; }
                                            }
                                        }
                                    }
                                }
                            }
                            if (onAcc.MR_OR_ADV_LIST != null)
                            {
                                SqlParameter[] paramAdj = new SqlParameter[8];
                                foreach (MR_OR_ADV ma in onAcc.MR_OR_ADV_LIST)
                                {
                                    paramAdj[0] = new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200);
                                    paramAdj[1] = new SqlParameter("@MR_ADJ_ID", SqlDbType.Decimal);
                                    paramAdj[2] = new SqlParameter("@MR_ADJ_CV_MR_ID", (onAcc.MR_ADJ_CV_MR_ID == null) ? (object)DBNull.Value : onAcc.MR_ADJ_CV_MR_ID);
                                    paramAdj[3] = new SqlParameter("@MR_ADJ_MR_ID", (ma.MR_ADJ_MR_ID == null) ? (object)DBNull.Value : ma.MR_ADJ_MR_ID);
                                    paramAdj[4] = new SqlParameter("@MR_ADJ_CRA_ID", (ma.MR_ADJ_CRA_ID == null) ? (object)DBNull.Value : ma.MR_ADJ_CRA_ID);
                                    paramAdj[5] = new SqlParameter("@MR_ADJ_MR_BILL_ID", (object)DBNull.Value);//MR_BILL_ID
                                    paramAdj[6] = new SqlParameter("@MR_ADJ_MR_CN_ID", (object)DBNull.Value);
                                    paramAdj[7] = new SqlParameter("@MR_ADJ_AMT", (ma.MR_ADJ_AMT == null) ? (object)DBNull.Value : ma.MR_ADJ_AMT);
                                    paramAdj[0].Direction = ParameterDirection.Output;
                                    paramAdj[1].Direction = ParameterDirection.Output;

                                    new DataAccess().InsertWithTransaction("[iFMS].[USP_INSERT_MR_ADJ]", CommandType.StoredProcedure, out command, connection, transactionScope, paramAdj);
                                    decimal MR_ADJ_ID = (decimal)command.Parameters["@MR_ADJ_ID"].Value;
                                    string error_Adj = (string)command.Parameters["@ERRORSTR"].Value;

                                    if (MR_ADJ_ID == -1) { errorMsg = error_Adj; }
                                }
                            }
                        }
                        else if (onAcc.BILL_CNS_DTL_LIST != null && (onAcc.ADJ_DOC_TYPE == 1))
                        {
                            //1 for CN
                            SqlParameter[] param3 = new SqlParameter[24];
                            SqlParameter[] paramBFD = new SqlParameter[17];
                            foreach (CN_OR_BILL_DTL cn in onAcc.BILL_CNS_DTL_LIST)
                            {
                                if ((cn.CN_OR_BILL_ID) > 0)
                                {
                                    param3[0] = new SqlParameter("@MR_CN_ID", SqlDbType.Decimal);
                                    param3[0].Direction = ParameterDirection.Output;
                                    param3[1] = new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200);
                                    param3[1].Direction = ParameterDirection.Output;

                                    param3[2] = new SqlParameter("@MR_ID", MR_ID);
                                    param3[3] = new SqlParameter("@CN_ID", (cn.CN_OR_BILL_ID == null) ? (object)DBNull.Value : cn.CN_OR_BILL_ID);
                                    param3[4] = new SqlParameter("@CN_FRT_AMT", (cn.CN_OR_BILL_FRT_AMT == null) ? (object)DBNull.Value : cn.CN_OR_BILL_FRT_AMT);
                                    param3[5] = new SqlParameter("@MR_DEMM_AMT", (cn.MR_DEMM_AMT == null) ? (object)DBNull.Value : cn.MR_DEMM_AMT);
                                    param3[6] = new SqlParameter("@MR_HNDL_AMT", (cn.MR_HNDL_AMT == null) ? (object)DBNull.Value : cn.MR_HNDL_AMT);
                                    param3[7] = new SqlParameter("@MR_OCT_AMT", (cn.MR_OCT_AMT == null) ? (object)DBNull.Value : cn.MR_OCT_AMT);
                                    param3[8] = new SqlParameter("@MR_OCS_AMT", (cn.MR_OCS_AMT == null) ? (object)DBNull.Value : cn.MR_OCS_AMT);
                                    param3[9] = new SqlParameter("@MR_DLVCH_AMT", (cn.MR_DLVCH_AMT == null) ? (object)DBNull.Value : cn.MR_DLVCH_AMT);
                                    param3[10] = new SqlParameter("@MR_MISC_AMT", (cn.MR_MISC_AMT == null) ? (object)DBNull.Value : cn.MR_MISC_AMT);
                                    param3[11] = new SqlParameter("@MR_OTH_AMT", (cn.MR_OTH_AMT == null) ? (object)DBNull.Value : cn.MR_OTH_AMT);
                                    param3[12] = new SqlParameter("@MR_OPMC_AMT", (cn.MR_OPMC_AMT == null) ? (object)DBNull.Value : cn.MR_OPMC_AMT);
                                    param3[13] = new SqlParameter("@MR_GTX_AMT", (cn.MR_GTX_AMT == null) ? (object)DBNull.Value : cn.MR_GTX_AMT);
                                    param3[14] = new SqlParameter("@MR_CPE_AMT", (cn.MR_CPE_AMT == null) ? (object)DBNull.Value : cn.MR_CPE_AMT);
                                    param3[15] = new SqlParameter("@MR_GDN_CHRG_AMT", (cn.MR_GDN_CHRG_AMT == null) ? (object)DBNull.Value : cn.MR_GDN_CHRG_AMT);
                                    param3[16] = new SqlParameter("@MR_SUB_TOTAL_AMT", (cn.MR_TOTAL_AMT == null) ? (object)DBNull.Value : cn.MR_TOTAL_AMT);
                                    param3[17] = new SqlParameter("@MR_BFD_AMT", (cn.TOT_DEDN_AMT == null) ? (object)DBNull.Value : cn.TOT_DEDN_AMT);
                                    param3[18] = new SqlParameter("@MR_TOTAL_AMT", (cn.NET_RECD_AMT == null) ? (object)DBNull.Value : cn.NET_RECD_AMT);
                                    param3[19] = new SqlParameter("@MR_STAX_FRT", (cn.MR_STAX_FRT == null) ? (object)DBNull.Value : cn.MR_STAX_FRT);
                                    param3[20] = new SqlParameter("@MR_STAX", (cn.MR_STAX == null) ? (object)DBNull.Value : cn.MR_STAX);
                                    param3[21] = new SqlParameter("@MR_COD_CQNO", (cn.MR_COD_CQNO == null) ? (object)DBNull.Value : cn.MR_COD_CQNO);
                                    param3[22] = new SqlParameter("@MR_COD_AMT", (cn.MR_COD_AMT == null) ? (object)DBNull.Value : cn.MR_COD_AMT);
                                    param3[23] = new SqlParameter("@MR_DPR_ID", (object)DBNull.Value);

                                    new DataAccess().InsertWithTransaction("[iTMS].[USP_INSERT_MR_CN]", CommandType.StoredProcedure, out command, connection, transactionScope, param3);
                                    decimal MR_CN_ID = (decimal)command.Parameters["@MR_CN_ID"].Value;
                                    string error_4 = (string)command.Parameters["@ERRORSTR"].Value;
                                    if (MR_CN_ID == -1) { errorMsg = error_4; break; }

                                    //BFD 
                                    MRBFD_CLAM_AMT = cn.MRBFD_CLAM_AMT ?? 0;
                                    MRBFD_RECO_AMT = cn.MRBFD_RECO_AMT ?? 0;
                                    MRBFD_NREC_AMT = cn.MRBFD_NREC_AMT ?? 0;
                                    MRBFD_EMD_AMT = cn.MRBFD_EMD_AMT ?? 0;
                                    MRBFD_SD_AMT = cn.MRBFD_SD_AMT ?? 0;
                                    MR_TDS_ON_AMT = cn.MR_TDS_ON_AMT ?? 0;
                                    MR_TDS_AMT = cn.MR_TDS_AMT ?? 0;

                                    if ((MRBFD_CLAM_AMT + MRBFD_RECO_AMT + MRBFD_NREC_AMT + MRBFD_EMD_AMT + MRBFD_SD_AMT + MR_TDS_ON_AMT + MR_TDS_AMT) > 0 && MR_CN_ID > 0)
                                    {
                                        paramBFD[0] = new SqlParameter("@MRBFD_ID", SqlDbType.Decimal);
                                        paramBFD[0].Direction = ParameterDirection.Output;
                                        paramBFD[1] = new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200);
                                        paramBFD[1].Direction = ParameterDirection.Output;

                                        paramBFD[2] = new SqlParameter("@MRBFD_MR_ID", MR_ID);
                                        paramBFD[3] = new SqlParameter("@MRBFD_BILL_ID", (object)DBNull.Value);
                                        paramBFD[4] = new SqlParameter("@MRBFD_DOC_TYPE", (onAcc.ADJ_DOC_TYPE == null) ? (object)DBNull.Value : onAcc.ADJ_DOC_TYPE);
                                        paramBFD[5] = new SqlParameter("@MRBFD_CN_ID", (cn.CN_OR_BILL_ID == null) ? (object)DBNull.Value : cn.CN_OR_BILL_ID);
                                        paramBFD[6] = new SqlParameter("@MRBFD_CLAM_AMT", (cn.MRBFD_CLAM_AMT == null) ? (object)DBNull.Value : cn.MRBFD_CLAM_AMT);
                                        paramBFD[7] = new SqlParameter("@MRBFD_RECO_AMT", (cn.MRBFD_RECO_AMT == null) ? (object)DBNull.Value : cn.MRBFD_RECO_AMT);
                                        paramBFD[8] = new SqlParameter("@MRBFD_NREC_AMT", (cn.MRBFD_NREC_AMT == null) ? (object)DBNull.Value : cn.MRBFD_NREC_AMT);
                                        paramBFD[9] = new SqlParameter("@MRBFD_BFDR_ID", (cn.MRBFD_BFDR_ID == null) ? (object)DBNull.Value : cn.MRBFD_BFDR_ID);
                                        paramBFD[10] = new SqlParameter("@MRBFD_EMD_AMT", (cn.MRBFD_EMD_AMT == null) ? (object)DBNull.Value : cn.MRBFD_EMD_AMT);
                                        paramBFD[11] = new SqlParameter("@MRBFD_SD_AMT", (cn.MRBFD_SD_AMT == null) ? (object)DBNull.Value : cn.MRBFD_SD_AMT);
                                        paramBFD[12] = new SqlParameter("@MRBFD_TDS_ON_AMT", (cn.MR_TDS_ON_AMT == null) ? (object)DBNull.Value : cn.MR_TDS_ON_AMT);
                                        paramBFD[13] = new SqlParameter("@MRBFD_TDS_PER", (cn.MR_TDS_PER == null) ? (object)DBNull.Value : cn.MR_TDS_PER);
                                        paramBFD[14] = new SqlParameter("@MRBFD_TDS_AMT", (cn.MR_TDS_AMT == null) ? (object)DBNull.Value : cn.MR_TDS_AMT);
                                        paramBFD[15] = new SqlParameter("@MRBFD_MR_CN_ID", (MR_CN_ID == null) ? (object)DBNull.Value : MR_CN_ID);
                                        paramBFD[16] = new SqlParameter("@MRBFD_MR_BILL_ID", DBNull.Value);

                                        new DataAccess().InsertWithTransaction("[iTMS].[USP_INSERT_MR_BFD]", CommandType.StoredProcedure, out command, connection, transactionScope, paramBFD);
                                        MRBFD_ID = (decimal)command.Parameters["@MRBFD_ID"].Value;
                                        string error_BFD = (string)command.Parameters["@ERRORSTR"].Value;
                                        if (MRBFD_ID == -1) { errorMsg = error_BFD; break; }
                                    }
                                }
                            }
                            if (onAcc.MR_OR_ADV_LIST != null)
                            {
                                SqlParameter[] paramAdj = new SqlParameter[8];
                                foreach (MR_OR_ADV ma in onAcc.MR_OR_ADV_LIST)
                                {
                                    paramAdj[0] = new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200);
                                    paramAdj[1] = new SqlParameter("@MR_ADJ_ID", SqlDbType.Decimal);
                                    paramAdj[2] = new SqlParameter("@MR_ADJ_CV_MR_ID", (onAcc.MR_ADJ_CV_MR_ID == null) ? (object)DBNull.Value : onAcc.MR_ADJ_CV_MR_ID);
                                    paramAdj[3] = new SqlParameter("@MR_ADJ_MR_ID", (ma.MR_ADJ_MR_ID == null) ? (object)DBNull.Value : ma.MR_ADJ_MR_ID);
                                    paramAdj[4] = new SqlParameter("@MR_ADJ_CRA_ID", (ma.MR_ADJ_CRA_ID == null) ? (object)DBNull.Value : ma.MR_ADJ_CRA_ID);
                                    paramAdj[5] = new SqlParameter("@MR_ADJ_MR_BILL_ID", (object)DBNull.Value);
                                    paramAdj[6] = new SqlParameter("@MR_ADJ_MR_CN_ID", (object)DBNull.Value);//MR_CN_ID
                                    paramAdj[7] = new SqlParameter("@MR_ADJ_AMT", (ma.MR_ADJ_AMT == null) ? (object)DBNull.Value : ma.MR_ADJ_AMT);
                                    paramAdj[0].Direction = ParameterDirection.Output;
                                    paramAdj[1].Direction = ParameterDirection.Output;

                                    new DataAccess().InsertWithTransaction("[iFMS].[USP_INSERT_MR_ADJ]", CommandType.StoredProcedure, out command, connection, transactionScope, paramAdj);
                                    decimal MR_ADJ_ID = (decimal)command.Parameters["@MR_ADJ_ID"].Value;
                                    string error_Adj = (string)command.Parameters["@ERRORSTR"].Value;

                                    if (MR_ADJ_ID == -1) { errorMsg = error_Adj; }

                                }
                            }
                        }
                        else if (onAcc.ADJ_DOC_TYPE == 3)//For Misc, 31-12-2020
                        {
                            if (onAcc.MR_OR_ADV_LIST != null)
                            {
                                SqlParameter[] paramAdj = new SqlParameter[8];
                                foreach (MR_OR_ADV ma in onAcc.MR_OR_ADV_LIST)
                                {
                                    paramAdj[0] = new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200);
                                    paramAdj[1] = new SqlParameter("@MR_ADJ_ID", SqlDbType.Decimal);
                                    paramAdj[2] = new SqlParameter("@MR_ADJ_CV_MR_ID", (onAcc.MR_ADJ_CV_MR_ID == null) ? (object)DBNull.Value : onAcc.MR_ADJ_CV_MR_ID);
                                    paramAdj[3] = new SqlParameter("@MR_ADJ_MR_ID", (ma.MR_ADJ_MR_ID == null) ? (object)DBNull.Value : ma.MR_ADJ_MR_ID);
                                    paramAdj[4] = new SqlParameter("@MR_ADJ_CRA_ID", (ma.MR_ADJ_CRA_ID == null) ? (object)DBNull.Value : ma.MR_ADJ_CRA_ID);
                                    paramAdj[5] = new SqlParameter("@MR_ADJ_MR_BILL_ID", (object)DBNull.Value);
                                    paramAdj[6] = new SqlParameter("@MR_ADJ_MR_CN_ID", (object)DBNull.Value);
                                    paramAdj[7] = new SqlParameter("@MR_ADJ_AMT", (ma.MR_ADJ_AMT == null) ? (object)DBNull.Value : ma.MR_ADJ_AMT);
                                    paramAdj[0].Direction = ParameterDirection.Output;
                                    paramAdj[1].Direction = ParameterDirection.Output;
                                    new DataAccess().InsertWithTransaction("[iFMS].[USP_INSERT_MR_ADJ]", CommandType.StoredProcedure, out command, connection, transactionScope, paramAdj);
                                    decimal MR_ADJ_ID = (decimal)command.Parameters["@MR_ADJ_ID"].Value;
                                    string error_Adj = (string)command.Parameters["@ERRORSTR"].Value;
                                    if (MR_ADJ_ID == -1) { errorMsg = error_Adj; }
                                }
                            }
                        }
                    }

                    if (errorMsg == "")
                    {
                        transactionScope.Commit();
                    }
                    else
                    {
                        transactionScope.Rollback();
                    }
                }
                catch (Exception)
                {
                    errorMsg = "Error: Exception occured.";
                    transactionScope.Rollback();
                }
                finally
                {
                    connection.Close();
                }
            }
            return errorMsg;
        }

        #endregion

        #region MONEY REALISATION RECEIPT VIEW
        #region MONEY REALISATION RECEIPT VIEW
        public MR_Receipt MONEY_REALISATION_RECEIPT_VIEW(decimal MR_ID)
        {
            MR_Receipt mr = new MR_Receipt();
            List<CN_OR_BILL_DTL> cnsBillDtls = new List<CN_OR_BILL_DTL>();

            SqlParameter[] param = { new SqlParameter("@MRID", MR_ID) };
            DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iTMS].[USP_MONEY_REALISATION_RECEIPT_VIEW]", CommandType.StoredProcedure, param);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    mr.MR_ID = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_ID"]);
                    mr.MR_NO = Convert.ToString(ds.Tables[0].Rows[0]["MR_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_NO"]);
                    mr.MR_MANUAL_NO = Convert.ToString(ds.Tables[0].Rows[0]["MR_MANUAL_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_MANUAL_NO"]);
                    mr.MR_BR_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["MR_BR_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_BR_ID"]);
                    mr.MR_BR_NAME = Convert.ToString(ds.Tables[0].Rows[0]["MR_BR_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_BR_NAME"]);
                    mr.MR_P_ID = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_P_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_P_ID"]);
                    mr.MR_P_NAME = Convert.ToString(ds.Tables[0].Rows[0]["MR_P_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_P_NAME"]);
                    mr.MR_P_CODE = Convert.ToString(ds.Tables[0].Rows[0]["MR_P_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_P_CODE"]);
                    mr.MR_PA_ID = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_PA_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_PA_ID"]);

                    mr.PAN_NO = Convert.ToString(ds.Tables[0].Rows[0]["MR_P_PAN"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_P_PAN"]);
                    mr.MR_P_GSTNO = Convert.ToString(ds.Tables[0].Rows[0]["MR_P_GSTNO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_P_GSTNO"]);

                    mr.MR_P_PH_NO = Convert.ToString(ds.Tables[0].Rows[0]["MR_P_PHONENO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_P_PHONENO"]);
                    mr.MR_TYPE = Convert.ToInt32(ds.Tables[0].Rows[0]["MR_TYPE"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_TYPE"]);
                    mr.MR_TYPE_NAME = Convert.ToString(ds.Tables[0].Rows[0]["MR_TYPE_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_TYPE_NAME"]);

                    mr.MR_DOC_TYPE = Convert.ToInt32(ds.Tables[0].Rows[0]["MR_DOC_TYPE"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_DOC_TYPE"]);
                    mr.MR_DOC_TYPE_NAME = Convert.ToString(ds.Tables[0].Rows[0]["MR_DOC_TYPE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_DOC_TYPE"]);

                    mr.MR_DATE1 = Convert.ToString(ds.Tables[0].Rows[0]["MR_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_DATE"]);
                    mr.MR_SUFFIX = Convert.ToString(ds.Tables[0].Rows[0]["MR_SUFFIX"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_SUFFIX"]);
                    mr.DPR_NO = Convert.ToString(ds.Tables[0].Rows[0]["DPR_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["DPR_NO"]);

                    mr.MR_TOTAL_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_TOTAL_AMT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_TOTAL_AMT"]);
                    mr.MR_CN_AMT_EXTRA = Convert.ToBoolean(ds.Tables[0].Rows[0]["MR_CN_AMT_EXTRA"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["MR_CN_AMT_EXTRA"]);
                    mr.TO_CBS_DATE1 = Convert.ToString(ds.Tables[0].Rows[0]["MR_CBS_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_CBS_DATE"]);
                    mr.MR_CBS_BR_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["MR_CBS_BR_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_CBS_BR_ID"]);
                    mr.CBS_STN_CODE = Convert.ToString(ds.Tables[0].Rows[0]["MR_CBS_BR_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_CBS_BR_CODE"]);
                    mr.CBS_STN_NAME = Convert.ToString(ds.Tables[0].Rows[0]["MR_CBS_BR_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_CBS_BR_NAME"]);
                    mr.MR_OLD_ID = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_OLD_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_OLD_ID"]);
                    mr.MR_DCR_ID = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_DCR_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_DCR_ID"]);
                    mr.MR_DPR_STATUS = Convert.ToBoolean(ds.Tables[0].Rows[0]["MR_DPR_STATUS"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["MR_DPR_STATUS"]);
                    mr.MR_DESC = Convert.ToString(ds.Tables[0].Rows[0]["MR_DESC"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_DESC"]);
                    mr.MR_STAX_FRT = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_STAX_FRT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_STAX_FRT"]);
                    mr.MR_STAX = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_STAX"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_STAX"]);
                    mr.MR_REMARKS = Convert.ToString(ds.Tables[0].Rows[0]["MR_REMARKS"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_REMARKS"]);
                    mr.MR_TENDER_NO = Convert.ToString(ds.Tables[0].Rows[0]["MR_TENDER_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_TENDER_NO"]);
                    mr.MR_TENDER_ID = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_TENDER_ID"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["MR_TENDER_ID"]);
                    mr.CRA_ID = Convert.ToDecimal(ds.Tables[0].Rows[0]["CRA_ID"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["CRA_ID"]);
                    mr.IBT_TYPE_ID = Convert.ToBoolean(ds.Tables[0].Rows[0]["IBT_TYPE_ID"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["IBT_TYPE_ID"]);
                    mr.MRIBT_CRA_NO = Convert.ToString(ds.Tables[0].Rows[0]["CRA_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CRA_NO"]);
                    mr.MRIBT_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["IBT_AMOUNT"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["IBT_AMOUNT"]);

                    mr.REC_PAY_MODE_CASH = Convert.ToBoolean(ds.Tables[0].Rows[0]["CASH_PAY_MODE"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["CASH_PAY_MODE"]);
                    mr.REC_PAY_MODE_CHQ = Convert.ToBoolean(ds.Tables[0].Rows[0]["CHQ_PAY_MODE"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["CHQ_PAY_MODE"]);
                    mr.REC_PAY_MODE_POS = Convert.ToBoolean(ds.Tables[0].Rows[0]["POS_PAY_MODE"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["POS_PAY_MODE"]);

                    mr.ACC_PAY_MODE_CASH = mr.REC_PAY_MODE_CASH;
                    mr.ACC_PAY_MODE_CHQ = mr.REC_PAY_MODE_CHQ;
                    mr.ACC_PAY_MODE_POS = mr.REC_PAY_MODE_POS;

                    mr.CHQ_RTGS_DD_NO = Convert.ToString(ds.Tables[0].Rows[0]["CHQ_RTGS_DD_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CHQ_RTGS_DD_NO"]);
                    mr.CHQ_RTGS_DD_BANK_NAME = Convert.ToString(ds.Tables[0].Rows[0]["CHQ_RTGS_DD_BANK_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CHQ_RTGS_DD_BANK_NAME"]);
                    mr.CHQ_RTGS_DD_DATE1 = Convert.ToString(ds.Tables[0].Rows[0]["CHQ_RTGS_DD_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CHQ_RTGS_DD_DATE"]);

                    mr.POS_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["POS_AMOUNT"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["POS_AMOUNT"]);
                    mr.CASH_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["CASH_AMOUNT"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["CASH_AMOUNT"]);
                    mr.CHQ_RTGS_DD_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["CHQ_AMOUNT"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["CHQ_AMOUNT"]);
                    mr.POS_TRAN_NO = Convert.ToString(ds.Tables[0].Rows[0]["POS_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["POS_NO"]);
                    mr.EMP_CODE = Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["EMPLOYEE_NAME"]);

                    mr.MR_STATUS = Convert.ToBoolean(ds.Tables[0].Rows[0]["MR_STATUS"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["MR_STATUS"]);

                    mr.MR_TOTAL_AMT = Convert.ToDecimal(mr.CASH_AMT ?? 0) + Convert.ToDecimal(mr.POS_AMT ?? 0) + Convert.ToDecimal(mr.CHQ_RTGS_DD_AMT ?? 0);

                    if (mr.REC_PAY_MODE_POS == true && Convert.ToString(mr.CHQ_RTGS_DD_BANK_NAME) == "")
                    {
                        mr.CHQ_RTGS_DD_BANK_NAME = Convert.ToString(ds.Tables[0].Rows[0]["POS_BANK_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["POS_BANK_NAME"]);
                    }

                    //----- ADDED BY : ASHISH KALSARPE --- DATE : 22/04/2022 ---
                    mr.MR_ENTRY_DATE = Convert.ToString(ds.Tables[0].Rows[0]["MR_ENTRY_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_ENTRY_DATE"]);
                    mr.MR_PRINT_DATE = Convert.ToString(ds.Tables[0].Rows[0]["MR_PRINT_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_PRINT_DATE"]);
                    //------ ASHISH END -----------------------------
                }

                decimal totalOPMCAmt = 0;
                decimal totalFRTAmt = 0;
                decimal totalDCCAmt = 0;
                decimal totalMHCAmt = 0;
                decimal totalCPEAmt = 0;
                decimal totalOCTAmt = 0;
                decimal totalOMCAmt = 0;
                decimal totalDelChgsAmt = 0;
                decimal totalMiscAmt = 0;
                decimal totalGodownChgsAmt = 0;
                decimal totalGTXAmt = 0;
                decimal totalOTHAmt = 0;
                decimal totalCollAmt = 0;
                decimal totalDednAmt = 0;
                decimal totalNetRecdAmt = 0;

                if (ds.Tables.Count > 1)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[1].Rows)
                        {
                            decimal FRT_AMT = Convert.ToDecimal(row["CN_OR_BILL_FRT_AMT"] == DBNull.Value ? "0" : row["CN_OR_BILL_FRT_AMT"]);
                            totalFRTAmt += FRT_AMT;
                            decimal DEMM_AMT = Convert.ToDecimal(row["MR_DEMM_AMT"] == DBNull.Value ? "0" : row["MR_DEMM_AMT"]);
                            totalDCCAmt += DEMM_AMT;
                            decimal HNDL_AMT = Convert.ToDecimal(row["MR_HNDL_AMT"] == DBNull.Value ? "0" : row["MR_HNDL_AMT"]);
                            totalMHCAmt += HNDL_AMT;
                            decimal OCT_AMT = Convert.ToDecimal(row["MR_OCT_AMT"] == DBNull.Value ? "0" : row["MR_OCT_AMT"]);
                            totalOCTAmt += OCT_AMT;
                            decimal OCS_AMT = Convert.ToDecimal(row["MR_OCS_AMT"] == DBNull.Value ? "0" : row["MR_OCS_AMT"]);
                            totalOMCAmt += OCS_AMT;
                            decimal DLVCH_AMT = Convert.ToDecimal(row["MR_DLVCH_AMT"] == DBNull.Value ? "0" : row["MR_DLVCH_AMT"]);
                            totalDelChgsAmt += DLVCH_AMT;
                            decimal MISC_AMT = Convert.ToDecimal(row["MR_MISC_AMT"] == DBNull.Value ? "0" : row["MR_MISC_AMT"]);
                            totalMiscAmt += MISC_AMT;
                            decimal OTH_AMT = Convert.ToDecimal(row["MR_OTH_AMT"] == DBNull.Value ? "0" : row["MR_OTH_AMT"]);
                            totalOTHAmt += OTH_AMT;
                            decimal OPMC_AMT = Convert.ToDecimal(row["MR_OPMC_AMT"] == DBNull.Value ? "0" : row["MR_OPMC_AMT"]);
                            totalOPMCAmt += OPMC_AMT;
                            decimal GTX_AMT = Convert.ToDecimal(row["MR_GTX_AMT"] == DBNull.Value ? "0" : row["MR_GTX_AMT"]);
                            totalGTXAmt += GTX_AMT;
                            decimal CPE_AMT = Convert.ToDecimal(row["MR_CPE_AMT"] == DBNull.Value ? "0" : row["MR_CPE_AMT"]);
                            totalCPEAmt += CPE_AMT;
                            decimal GDN_CHRG_AMT = Convert.ToDecimal(row["MR_GDN_CHRG_AMT"] == DBNull.Value ? "0" : row["MR_GDN_CHRG_AMT"]);
                            totalGodownChgsAmt += GDN_CHRG_AMT;

                            decimal TOTAL_COLL_AMT = OPMC_AMT + FRT_AMT + DEMM_AMT + HNDL_AMT + CPE_AMT + OCT_AMT + OCS_AMT + DLVCH_AMT + MISC_AMT + GDN_CHRG_AMT + OTH_AMT;

                            totalCollAmt += TOTAL_COLL_AMT;


                            decimal MRBFD_CLAM_AMT = Convert.ToDecimal(row["MRBFD_CLAM_AMT"] == DBNull.Value ? "0" : row["MRBFD_CLAM_AMT"]);
                            decimal MRBFD_NREC_AMT = Convert.ToDecimal(row["MRBFD_NREC_AMT"] == DBNull.Value ? "0" : row["MRBFD_NREC_AMT"]);
                            decimal MRBFD_EMD_AMT = Convert.ToDecimal(row["MRBFD_EMD_AMT"] == DBNull.Value ? "0" : row["MRBFD_EMD_AMT"]);
                            decimal MRBFD_SD_AMT = MRBFD_SD_AMT = Convert.ToDecimal(row["MRBFD_SD_AMT"] == DBNull.Value ? "0" : row["MRBFD_SD_AMT"]);
                            decimal MRBFD_TDS_AMT = Convert.ToDecimal(row["MRBFD_TDS_AMT"] == DBNull.Value ? "0" : row["MRBFD_TDS_AMT"]);
                            decimal RECO_AMT = Convert.ToDecimal(row["MRBFD_RECO_AMT"] == DBNull.Value ? "0" : row["MRBFD_RECO_AMT"]);

                            decimal TOTAL_DED_AMT = MRBFD_CLAM_AMT + MRBFD_NREC_AMT + MRBFD_EMD_AMT + MRBFD_SD_AMT + MRBFD_TDS_AMT + RECO_AMT;
                            totalDednAmt += TOTAL_DED_AMT;

                            decimal NET_RECEIVED_AMT = (TOTAL_COLL_AMT - TOTAL_DED_AMT);
                            totalNetRecdAmt += NET_RECEIVED_AMT;

                            cnsBillDtls.Add(new CN_OR_BILL_DTL
                            {

                                CN_OR_BILL_FRT_AMT = Convert.ToDecimal(row["CN_OR_BILL_FRT_AMT"] == DBNull.Value ? "0" : row["CN_OR_BILL_FRT_AMT"]),

                                MR_DEMM_AMT = Convert.ToDecimal(row["MR_DEMM_AMT"] == DBNull.Value ? "0" : row["MR_DEMM_AMT"]),
                                MR_HNDL_AMT = Convert.ToDecimal(row["MR_HNDL_AMT"] == DBNull.Value ? "0" : row["MR_HNDL_AMT"]),
                                MR_OCT_AMT = Convert.ToDecimal(row["MR_OCT_AMT"] == DBNull.Value ? "0" : row["MR_OCT_AMT"]),
                                MR_OCS_AMT = Convert.ToDecimal(row["MR_OCS_AMT"] == DBNull.Value ? "0" : row["MR_OCS_AMT"]),
                                MR_DLVCH_AMT = Convert.ToDecimal(row["MR_DLVCH_AMT"] == DBNull.Value ? "0" : row["MR_DLVCH_AMT"]),
                                MR_MISC_AMT = Convert.ToDecimal(row["MR_MISC_AMT"] == DBNull.Value ? "0" : row["MR_MISC_AMT"]),
                                MR_OTH_AMT = Convert.ToDecimal(row["MR_OTH_AMT"] == DBNull.Value ? "0" : row["MR_OTH_AMT"]),
                                MR_OPMC_AMT = Convert.ToDecimal(row["MR_OPMC_AMT"] == DBNull.Value ? "0" : row["MR_OPMC_AMT"]),
                                MR_GTX_AMT = Convert.ToDecimal(row["MR_GTX_AMT"] == DBNull.Value ? "0" : row["MR_GTX_AMT"]),
                                MR_CPE_AMT = Convert.ToDecimal(row["MR_CPE_AMT"] == DBNull.Value ? "0" : row["MR_CPE_AMT"]),
                                MR_GDN_CHRG_AMT = Convert.ToDecimal(row["MR_GDN_CHRG_AMT"] == DBNull.Value ? "0" : row["MR_GDN_CHRG_AMT"]),
                                MR_BFD_AMT = Convert.ToDecimal(row["MR_BFD_AMT"] == DBNull.Value ? "0" : row["MR_BFD_AMT"]),
                                MR_SUB_TOTAL_AMT = Convert.ToDecimal(row["MR_SUB_TOTAL_AMT"] == DBNull.Value ? "0" : row["MR_SUB_TOTAL_AMT"]),
                                MR_STAX_FRT = Convert.ToDecimal(row["MR_STAX_FRT"] == DBNull.Value ? "0" : row["MR_STAX_FRT"]),
                                MR_STAX = Convert.ToDecimal(row["MR_STAX"] == DBNull.Value ? "0" : row["MR_STAX"]),
                                MR_COD_CQNO_1 = Convert.ToString(row["MR_COD_CQNO"] == DBNull.Value ? "" : row["MR_COD_CQNO"]),
                                MR_COD_AMT = Convert.ToDecimal(row["MR_COD_AMT"] == DBNull.Value ? "0" : row["MR_COD_AMT"]),
                                MR_CN_BILL_STATUS = Convert.ToBoolean(row["MR_CN_BILL_STATUS"] == DBNull.Value ? 0 : row["MR_CN_BILL_STATUS"]),
                                CN_OR_BILL_NO = Convert.ToString(row["CN_OR_BILL_NO"] == DBNull.Value ? "" : row["CN_OR_BILL_NO"]),
                                CN_OR_BILL_DATE1 = Convert.ToString(row["CN_OR_BILL_DATE1"] == DBNull.Value ? "" : row["CN_OR_BILL_DATE1"]),
                                CN_OR_BILL_BR_NAME = Convert.ToString(row["CN_OR_BILL_BR_NAME"] == DBNull.Value ? "" : row["CN_OR_BILL_BR_NAME"]),
                                MRBFD_CLAM_AMT = Convert.ToDecimal(row["MRBFD_CLAM_AMT"] == DBNull.Value ? "0" : row["MRBFD_CLAM_AMT"]),
                                MRBFD_RECO_AMT = Convert.ToDecimal(row["MRBFD_RECO_AMT"] == DBNull.Value ? "0" : row["MRBFD_RECO_AMT"]),
                                MRBFD_NREC_AMT = Convert.ToDecimal(row["MRBFD_NREC_AMT"] == DBNull.Value ? "0" : row["MRBFD_NREC_AMT"]),
                                MRBFD_BFDR_ID = Convert.ToDecimal(row["MRBFD_BFDR_ID"] == DBNull.Value ? "0" : row["MRBFD_BFDR_ID"]),
                                MRBFD_EMD_AMT = Convert.ToDecimal(row["MRBFD_EMD_AMT"] == DBNull.Value ? "0" : row["MRBFD_EMD_AMT"]),
                                MRBFD_SD_AMT = Convert.ToDecimal(row["MRBFD_SD_AMT"] == DBNull.Value ? "0" : row["MRBFD_SD_AMT"]),
                                MR_TDS_PER = Convert.ToDecimal(row["MRBFD_TDS_PER"] == DBNull.Value ? "0" : row["MRBFD_TDS_PER"]),
                                MR_TDS_AMT = Convert.ToDecimal(row["MRBFD_TDS_AMT"] == DBNull.Value ? "0" : row["MRBFD_TDS_AMT"]),
                                MRBFD_BFDR_TXT = Convert.ToString(row["N_CODE_NAME"] == DBNull.Value ? "" : row["N_CODE_NAME"]),
                                MR_TOTAL_AMT = TOTAL_COLL_AMT,

                                TOT_DEDN_AMT = TOTAL_DED_AMT,

                                NET_RECD_AMT = NET_RECEIVED_AMT,
                            });
                        }

                        mr.BILL_CNS_DTL_LIST = cnsBillDtls;
                    }
                }

                mr.MR_DEMM_AMT = totalDCCAmt;
                mr.MR_HNDL_AMT = totalMHCAmt;
                mr.MR_OCT_AMT = totalOCTAmt;

                mr.MR_DLVCH_AMT = totalDelChgsAmt;
                mr.MR_MISC_AMT = totalMiscAmt;
                mr.MR_OTH_AMT = totalOTHAmt + totalOMCAmt + totalOCTAmt;
                mr.MR_OPMC_AMT = totalOPMCAmt;
                mr.MR_GTX_AMT = totalGTXAmt;
                mr.MR_CPE_AMT = totalCPEAmt;
                mr.MR_GDN_CHRG_AMT = totalGodownChgsAmt;
                mr.MR_BFD_AMT = totalDednAmt;

                mr.SUB_TOTAL = (mr.MR_DEMM_AMT + mr.MR_HNDL_AMT + mr.MR_DLVCH_AMT + mr.MR_MISC_AMT + mr.MR_OPMC_AMT + mr.MR_GDN_CHRG_AMT + mr.MR_CPE_AMT + mr.MR_GTX_AMT + mr.MR_OTH_AMT);
                mr.TOTAL_I = mr.SUB_TOTAL;

                mr.TOTAL_FRT_II = totalFRTAmt;
                mr.TOTAL_GRAND = Convert.ToDecimal(mr.TOTAL_I ?? 0) + Convert.ToDecimal(mr.TOTAL_FRT_II ?? 0);
                mr.MR_NET_RECEIVED = Convert.ToDecimal(totalCollAmt) - Convert.ToDecimal(totalDednAmt);
                mr.MR_SUB_TOTAL_AMT = Convert.ToDecimal(totalCollAmt);
            }
            return mr;
        }

        #endregion


        #endregion

        #region  MR Cancellation

        public List<MR_Search_List> SELECT_MR_Cancellation_Data(int? brId, string mrNo)
        {
            SqlParameter[] param = {
                                       new SqlParameter("@MR_NO", mrNo),
                                       new SqlParameter("@BR_ID", brId)
                                   };

            DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iTMS].[USP_GET_MR_DETAILS_BY_MR_NO]", CommandType.StoredProcedure, param);

            List<MR_Search_List> _list = new List<MR_Search_List>();
            DataTable dt = ds.Tables[0];

            //Added By Ashok Date : 23-12-2020
            string DMS_Path = ConfigurationManager.AppSettings["iCANDMSPATH"].ToString();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    MR_Search_List objMR_Search_List = new MR_Search_List();

                    objMR_Search_List._strMR_ID = Convert.ToString(row["MR_ID"] == DBNull.Value ? "" : row["MR_ID"]);
                    objMR_Search_List._strMR_NO = Convert.ToString(row["MR_NO"] == DBNull.Value ? "" : row["MR_NO"]);
                    objMR_Search_List._strMR_DATE = Convert.ToString(row["MR_DATE"] == DBNull.Value ? "" : row["MR_DATE"]);
                    objMR_Search_List._strMR_BRANCH = Convert.ToString(row["MR_BRANCH"] == DBNull.Value ? "" : row["MR_BRANCH"]);

                    objMR_Search_List._strMR_CBS_BRANCH = Convert.ToString(row["MR_CBS_BRANCH"] == DBNull.Value ? "" : row["MR_CBS_BRANCH"]);
                    objMR_Search_List._strMR_CBS_DATE = Convert.ToString(row["MR_CBS_DATE"] == DBNull.Value ? "" : row["MR_CBS_DATE"]);

                    objMR_Search_List._strMR_PARTY_CODE_NAME = Convert.ToString(row["MR_PARTY_CODE_NAME"] == DBNull.Value ? "" : row["MR_PARTY_CODE_NAME"]);
                    objMR_Search_List._strMR_TOTAL_AMT = Convert.ToDecimal(row["MR_TOTAL_AMT"] == DBNull.Value ? "0" : row["MR_TOTAL_AMT"]);

                    objMR_Search_List._strMR_STATUS = Convert.ToInt32(row["MR_STATUS"] == DBNull.Value ? "" : row["MR_STATUS"]);
                    objMR_Search_List._strMR_ACTION_REMARKS = Convert.ToString(row["MR_DESC"] == DBNull.Value ? "" : row["MR_DESC"]);

                    objMR_Search_List._strMR_CBS_LOCK_DATE = Convert.ToString(row["CBS_LOCK_DATE"] == DBNull.Value ? "" : row["CBS_LOCK_DATE"]);
                    objMR_Search_List._strMR_CBS_MR_DIFF = Convert.ToString(row["CBS_MR_DIFF"] == DBNull.Value ? "" : row["CBS_MR_DIFF"]);

                    //Added By Ashok Date : 23-12-2020
                    objMR_Search_List._strMR_MR_DOC_PATH = DMS_Path + Convert.ToString(row["MR_DOC_PATH"] == DBNull.Value ? "" : row["MR_DOC_PATH"]);

                    //Added By Pramesh Date : 09-09-2022
                    objMR_Search_List._strMR_CBS_BR_ID = Convert.ToInt32(row["MR_CBS_BR_ID"] == DBNull.Value ? "0" : row["MR_CBS_BR_ID"]);

                    _list.Add(objMR_Search_List);
                }
            }

            return _list;
        }

        public string UPDATE_MR_STATUS(MR_Cancellation mr)
        {
            string MsgText = "";
            using (var connection = new SqlConnection(sqlConnection.GetConnectionString()))
            {
                connection.Open();
                SqlCommand command;
                SqlTransaction transactionScope = null;
                transactionScope = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    SqlParameter[] param1 = {
                                                new SqlParameter("@MSG", SqlDbType.VarChar, 150)
                                                ,new SqlParameter("@MRID", Convert.ToDouble(mr.MR_ID))
                                                ,new SqlParameter("@REMARKS", mr.MR_Search_List[0]._strMR_ACTION_REMARKS)
                                                ,new SqlParameter("@EMP_ID", mr.MR_ADDBY)
                                                ,new SqlParameter("@ADDTYPE", mr.MR_USER_TYPE)
                                            };
                    param1[0].Direction = ParameterDirection.Output;
                    new DataAccess().InsertWithTransaction("[iTMS].[UPDATE_MR_CANCELLATION]", CommandType.StoredProcedure, out command, connection, transactionScope, param1);
                    string MSGTEXT_SQL = (string)command.Parameters["@MSG"].Value;
                    if (MSGTEXT_SQL != "")
                    {
                        MsgText = MSGTEXT_SQL;
                    }

                    if (MsgText.Contains("SUCCESS") == true)
                    {
                        transactionScope.Commit();
                    }
                    else
                    {
                        transactionScope.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    MsgText = "Error: 104-Exception occured." + ex.Message;
                    transactionScope.Rollback();
                }
                finally
                {
                    connection.Close();
                }
            }
            return MsgText;
        }

        public string CancelMRDoc(string mrNo, int mrBrId, string rmk, decimal MR_ADDBY, string MR_USER_TYPE, bool copyUpload, string copyName, HttpPostedFileBase file)
        {
            string MsgText = "";
            using (var connection = new SqlConnection(sqlConnection.GetConnectionString()))
            {
                connection.Open();
                SqlCommand command;
                SqlTransaction transactionScope = null;
                transactionScope = connection.BeginTransaction(IsolationLevel.ReadCommitted);

                // string DMS_Path = ConfigurationManager.AppSettings["iCANDMSPATH"].ToString();
                //string filePath = "\\MR";
                string filePath = "iCAN\\MR\\";
                //string directoryPath = DMS_Path + filePath;

                string ext = "";

                if (copyUpload && copyName != "" && file != null)
                {
                    ext = new System.IO.FileInfo(file.FileName).Extension;
                    string[] printCommandsArr = copyName.Split('/');
                }

                try
                {
                    SqlParameter[] param1 = {
                                              new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                                             ,new SqlParameter("@MR_ID", SqlDbType.Decimal) 
                                             ,new SqlParameter("@MR_BR_ID",mrBrId )
                                             ,new SqlParameter("@MR_NO",mrNo )
                                             ,new SqlParameter("@MR_REMARKS", rmk)
                                             ,new SqlParameter("@MR_CANCEL_COPY_UPLOAD", copyUpload)
                                             ,new SqlParameter("@MR_CANCEL_COPY_NAME", copyName)
                                             ,new SqlParameter("@MR_CANCEL_COPY_PATH", filePath)
                                             ,new SqlParameter("@MR_ADD_BY", MR_ADDBY)
                                             ,new SqlParameter("@MR_ADD_BY_TYPE", MR_USER_TYPE)
                                            };

                    param1[0].Direction = ParameterDirection.Output;
                    param1[1].Direction = ParameterDirection.Output;
                    new DataAccess().InsertWithTransaction("[iTMS].[USP_CANCEL_MR_DOC]", CommandType.StoredProcedure, out command, connection, transactionScope, param1);
                    string MSGTEXT_SQL = (string)command.Parameters["@ERRORSTR"].Value;
                    if (Convert.ToString(MSGTEXT_SQL).Trim() != "")
                    {
                        MsgText = MSGTEXT_SQL;
                    }

                    if (MsgText.Contains("SUCCESS") == true)
                    {

                        if (copyName != null)
                        {
                            /*--COMMENTED BY :--- ASHISH KALSARPE ---- DATE : 06/05/2022 --------
                            if (!Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }

                            if (file != null)
                            {
                                string fileName = mrNo + ext;
                                file.SaveAs(directoryPath + fileName);
                            }*/
                            //----- ASHISH KALSARPE ---- DATE : 06/05/2022 --------
                            if (file != null)
                            {

                                FTP_UPLOAD ftp = new FTP_UPLOAD();
                                CommonFunction cf = new CommonFunction();
                                ftp.ftpFileName = mrNo + ext;
                                ftp.ftpFileUpload = true;
                                ftp.ftpFolder = filePath;
                                ftp.ftpHost = ftp_Path;
                                ftp.ftpFile = file;
                                ftp.ftpUserId = ftp_User;
                                ftp.ftpPassword = ftp_Pwd;
                                cf.FTPFileUpload(ftp);
                            }
                            //---------ASHISH END---------------------------
                        }


                        transactionScope.Commit();
                    }
                    else
                    {
                        transactionScope.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    MsgText = "Error: 104-Exception occured." + ex.Message;
                    transactionScope.Rollback();
                }
                finally
                {
                    connection.Close();
                }
            }
            return MsgText;
        }
        #endregion

        #region OnAccount Adjustment List & View
        public List<MR_ADJUST_DATA_LIST> SELECT_ONACC_ADJ_DATA_LIST(On_Account_Adjustment_Search onAcc)
        {
            SqlParameter[] param = { 
                                       new SqlParameter("@ADJ_BR_ID", onAcc.SEARCH_BR_ID),
                                       new SqlParameter("@FDT", onAcc.FROM_DATE1),
                                       new SqlParameter("@EDT", onAcc.TO_DATE1),
                                       new SqlParameter("@MR_ADJ_PA_ID", onAcc.SEARCH_PARTY_ADD_ID??0),
                                       new SqlParameter("@MR_DOC_TYPE", onAcc.SEARCH_DOC_TYPE??0),
                                       new SqlParameter("@MR_DOC_NO", onAcc.SEARCH_DOC_NO??"")
                                   };

            DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iTMS].[USP_SELECT_MR_ADJUSTED_LIST]", CommandType.StoredProcedure, param);

            List<MR_ADJUST_DATA_LIST> _list = new List<MR_ADJUST_DATA_LIST>();
            DataTable dt = ds.Tables[0];
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    _list.Add(new MR_ADJUST_DATA_LIST
                    {
                        MR_ADJ_ID = Convert.ToDecimal(row["MR_ID"] == DBNull.Value ? "0" : row["MR_ID"]),
                        MR_ADJ_DATE = Convert.ToString(row["MR_DATE"] == DBNull.Value ? "" : row["MR_DATE"]),
                        MR_ADJ_BR_NAME = Convert.ToString(row["MR_ADJ_BR_NAME"] == DBNull.Value ? "" : row["MR_ADJ_BR_NAME"]),
                        MR_NO = Convert.ToString(row["MR_NO"] == DBNull.Value ? "" : row["MR_NO"]),
                        MR_DATE = Convert.ToString(row["MR_DATE"] == DBNull.Value ? "" : row["MR_DATE"]),
                        P_NAME = Convert.ToString(row["MR_ADJ_P_NAME"] == DBNull.Value ? "" : row["MR_ADJ_P_NAME"]),
                        P_CODE = Convert.ToString(row["MR_ADJ_P_CODE"] == DBNull.Value ? "" : row["MR_ADJ_P_CODE"]),
                        MR_ADJ_AMT = Convert.ToDecimal(row["MR_ADJ_AMT"] == DBNull.Value ? "0" : row["MR_ADJ_AMT"]),
                        MR_ADJ_DESC = Convert.ToString(row["MR_DESC"] == DBNull.Value ? "" : row["MR_DESC"]),
                        MR_DOC_TYPE = Convert.ToString(row["MR_DOC_TYPE"] == DBNull.Value ? "" : row["MR_DOC_TYPE"]),
                        MR_CBS_DATE = Convert.ToString(row["MR_CBS_DATE"] == DBNull.Value ? "" : row["MR_CBS_DATE"]),
                        MR_STMT_NO = Convert.ToString(row["MR_STMT_NO"] == DBNull.Value ? "" : row["MR_STMT_NO"]),
                        MR_REMARKS = Convert.ToString(row["MR_REMARKS"] == DBNull.Value ? "" : row["MR_REMARKS"]),
                    });
                }
            }
            return _list;
        }
        #endregion

        #region MR View & Print
        public TBL_MR_STATUS GET_MR_STATUS(int? br_id, string mrNo)
        {
            TBL_MR_STATUS mrStatus = new TBL_MR_STATUS();
            try
            {
                SqlParameter[] param = { new SqlParameter("@BR_ID", br_id), new SqlParameter("@MR_NO", mrNo) };
                DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iTMS].[USP_SELECT_MR_STATUS_BY_MR_NO]", CommandType.StoredProcedure, param);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        mrStatus.MR_ID = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_ID"]);
                        mrStatus.MR_NO = Convert.ToString(ds.Tables[0].Rows[0]["MR_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_NO"]);
                        mrStatus.MR_STATUS = Convert.ToString(ds.Tables[0].Rows[0]["MR_STATUS"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_STATUS"]);
                        mrStatus.IS_MR_PRINT = Convert.ToString(ds.Tables[0].Rows[0]["IS_MR_PRINT"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["IS_MR_PRINT"]);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return mrStatus;
        }
        #endregion

        #region MR Print

        public MR_ANNUX GET_MR_DTLS_FOR_ANNUX(decimal MR_ID)
        {
            MR_ANNUX mr = new MR_ANNUX();
            clsPrintSettings obj = new clsPrintSettings();
            SqlParameter[] param = { new SqlParameter("@MR_ID", MR_ID) };
            DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iTMS].[USP_SELECT_MR_DTLS_FOR_ANNUX_BY_MR_ID]", CommandType.StoredProcedure, param);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    mr.MR_ID = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_ID"]);
                    mr.MR_NO = Convert.ToString(ds.Tables[0].Rows[0]["MR_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_NO"]);
                    mr.MR_MANUAL_NO = Convert.ToString(ds.Tables[0].Rows[0]["MR_MANUAL_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_MANUAL_NO"]);
                    mr.MR_DATE = Convert.ToString(ds.Tables[0].Rows[0]["MR_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_DATE"]);
                    mr.MR_TYPE = Convert.ToString(ds.Tables[0].Rows[0]["MR_TYPE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_TYPE"]);
                    mr.MR_PAY_MODE = Convert.ToString(ds.Tables[0].Rows[0]["MR_PAY_MODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_PAY_MODE"]);
                    mr.MR_TYPE_CODE = Convert.ToString(ds.Tables[0].Rows[0]["MRT_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MRT_CODE"]);
                    mr.MR_BR_CODE = Convert.ToString(ds.Tables[0].Rows[0]["MR_BR_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_BR_CODE"]);
                    mr.MR_BR_NAME = Convert.ToString(ds.Tables[0].Rows[0]["MR_BR_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_BR_NAME"]);

                    if (ds.Tables.Count > 1)
                    {
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                            {
                                List<MR_ANNUX_DTL> _list = new List<MR_ANNUX_DTL>();

                                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                {
                                    MR_ANNUX_DTL objMR_ANNUX_DTL = new MR_ANNUX_DTL();

                                    objMR_ANNUX_DTL.CN_ID = Convert.ToInt32(ds.Tables[1].Rows[i]["CN_ID"] == DBNull.Value ? "0" : ds.Tables[1].Rows[i]["CN_ID"]);
                                    objMR_ANNUX_DTL.CN_NO = Convert.ToString(ds.Tables[1].Rows[i]["CN_NO"] == DBNull.Value ? "" : ds.Tables[1].Rows[i]["CN_NO"]);
                                    objMR_ANNUX_DTL.BR_CODE = Convert.ToString(ds.Tables[1].Rows[i]["BR_CODE"] == DBNull.Value ? "" : ds.Tables[1].Rows[i]["BR_CODE"]);
                                    objMR_ANNUX_DTL.DC_AMT = Convert.ToDecimal(ds.Tables[1].Rows[i]["DC_AMT"] == DBNull.Value ? "0" : ds.Tables[1].Rows[i]["DC_AMT"]);
                                    objMR_ANNUX_DTL.CN_FRT_AMT = Convert.ToDecimal(ds.Tables[1].Rows[i]["CN_FRT_AMT"] == DBNull.Value ? "0" : ds.Tables[1].Rows[i]["CN_FRT_AMT"]);
                                    objMR_ANNUX_DTL.MR_DEMM_AMT = Convert.ToDecimal(ds.Tables[1].Rows[i]["MR_DEMM_AMT"] == DBNull.Value ? "0" : ds.Tables[1].Rows[i]["MR_DEMM_AMT"]);
                                    objMR_ANNUX_DTL.MR_HNDL_AMT = Convert.ToDecimal(ds.Tables[1].Rows[i]["MR_HNDL_AMT"] == DBNull.Value ? "0" : ds.Tables[1].Rows[i]["MR_HNDL_AMT"]);
                                    objMR_ANNUX_DTL.MR_DLVCH_AMT = Convert.ToDecimal(ds.Tables[1].Rows[i]["MR_DLVCH_AMT"] == DBNull.Value ? "0" : ds.Tables[1].Rows[i]["MR_DLVCH_AMT"]);
                                    objMR_ANNUX_DTL.MR_MISC_AMT = Convert.ToDecimal(ds.Tables[1].Rows[i]["MR_MISC_AMT"] == DBNull.Value ? "0" : ds.Tables[1].Rows[i]["MR_MISC_AMT"]);
                                    objMR_ANNUX_DTL.MR_OPMC_AMT = Convert.ToDecimal(ds.Tables[1].Rows[i]["MR_OPMC_AMT"] == DBNull.Value ? "0" : ds.Tables[1].Rows[i]["MR_OPMC_AMT"]);
                                    objMR_ANNUX_DTL.MR_GDN_CHRG_AMT = Convert.ToDecimal(ds.Tables[1].Rows[i]["MR_GDN_CHRG_AMT"] == DBNull.Value ? "0" : ds.Tables[1].Rows[i]["MR_GDN_CHRG_AMT"]);
                                    objMR_ANNUX_DTL.MR_OCT_AMT = Convert.ToDecimal(ds.Tables[1].Rows[i]["MR_OCT_AMT"] == DBNull.Value ? "0" : ds.Tables[1].Rows[i]["MR_OCT_AMT"]);
                                    objMR_ANNUX_DTL.MR_OCS_AMT = Convert.ToDecimal(ds.Tables[1].Rows[i]["MR_OCS_AMT"] == DBNull.Value ? "0" : ds.Tables[1].Rows[i]["MR_OCS_AMT"]);
                                    objMR_ANNUX_DTL.MR_OTH_AMT = Convert.ToDecimal(ds.Tables[1].Rows[i]["MR_OTH_AMT"] == DBNull.Value ? "0" : ds.Tables[1].Rows[i]["MR_OTH_AMT"]);
                                    objMR_ANNUX_DTL.MR_GTX_AMT = Convert.ToDecimal(ds.Tables[1].Rows[i]["MR_GTX_AMT"] == DBNull.Value ? "0" : ds.Tables[1].Rows[i]["MR_GTX_AMT"]);
                                    objMR_ANNUX_DTL.MR_CPE_AMT = Convert.ToDecimal(ds.Tables[1].Rows[i]["MR_CPE_AMT"] == DBNull.Value ? "0" : ds.Tables[1].Rows[i]["MR_CPE_AMT"]);
                                    objMR_ANNUX_DTL.MR_SUB_TOTAL_AMT = Convert.ToDecimal(ds.Tables[1].Rows[i]["MR_SUB_TOTAL_AMT"] == DBNull.Value ? "0" : ds.Tables[1].Rows[i]["MR_SUB_TOTAL_AMT"]);
                                    objMR_ANNUX_DTL.MR_STAX = Convert.ToDecimal(ds.Tables[1].Rows[i]["MR_STAX"] == DBNull.Value ? "0" : ds.Tables[1].Rows[i]["MR_STAX"]);
                                    objMR_ANNUX_DTL.MR_TOTAL_AMT = Convert.ToDecimal(ds.Tables[1].Rows[i]["MR_TOTAL_AMT"] == DBNull.Value ? "0" : ds.Tables[1].Rows[i]["MR_TOTAL_AMT"]);
                                    objMR_ANNUX_DTL.MR_TOTAL_NET_AMT = Convert.ToDecimal(ds.Tables[1].Rows[i]["MR_TOTAL_NET_AMT"] == DBNull.Value ? "0" : ds.Tables[1].Rows[i]["MR_TOTAL_NET_AMT"]);
                                    objMR_ANNUX_DTL.MR_BFD_AMT = Convert.ToDecimal(ds.Tables[1].Rows[i]["MR_BFD_AMT"] == DBNull.Value ? "0" : ds.Tables[1].Rows[i]["MR_BFD_AMT"]);
                                    objMR_ANNUX_DTL.MR_TDS_AMT = Convert.ToDecimal(ds.Tables[1].Rows[i]["MR_TDS_AMT"] == DBNull.Value ? "0" : ds.Tables[1].Rows[i]["MR_TDS_AMT"]);
                                    objMR_ANNUX_DTL.MR_TOTAL_DEDUCT_AMT = Convert.ToDecimal(ds.Tables[1].Rows[i]["MR_TOTAL_DEDUCT_AMT"] == DBNull.Value ? "0" : ds.Tables[1].Rows[i]["MR_TOTAL_DEDUCT_AMT"]);
                                    objMR_ANNUX_DTL.CNOR = Convert.ToString(ds.Tables[1].Rows[i]["CNOR"] == DBNull.Value ? "" : ds.Tables[1].Rows[i]["CNOR"]);
                                    objMR_ANNUX_DTL.CNOR_GSTIN = Convert.ToString(ds.Tables[1].Rows[i]["CNOR_GSTIN"] == DBNull.Value ? "" : ds.Tables[1].Rows[i]["CNOR_GSTIN"]);
                                    objMR_ANNUX_DTL.CNEE = Convert.ToString(ds.Tables[1].Rows[i]["CNEE"] == DBNull.Value ? "" : ds.Tables[1].Rows[i]["CNEE"]);
                                    objMR_ANNUX_DTL.CNEE_GSTIN = Convert.ToString(ds.Tables[1].Rows[i]["CNEE_GSTIN"] == DBNull.Value ? "" : ds.Tables[1].Rows[i]["CNEE_GSTIN"]);
                                    objMR_ANNUX_DTL.REF_PARTY_CODE = Convert.ToString(ds.Tables[1].Rows[i]["REF_PARTY_CODE"] == DBNull.Value ? "" : ds.Tables[1].Rows[i]["REF_PARTY_CODE"]);
                                    objMR_ANNUX_DTL.BILL_GSTIN = Convert.ToString(ds.Tables[1].Rows[i]["BILL_GSTIN"] == DBNull.Value ? "" : ds.Tables[1].Rows[i]["BILL_GSTIN"]);

                                    _list.Add(objMR_ANNUX_DTL);

                                }
                                mr.MR_ANNUX_DTL_LIST = _list;
                            }
                        }
                    }
                }
            }
            return mr;

        }

        public MR_CNS_BFD SELECT_BILL_CNS_REF_DTL_FOR_MR_BFD_BY_CN_NO(decimal billId, string cnNo)
        {
            SqlParameter[] param = { new SqlParameter("@BILL_ID", billId), new SqlParameter("@CN_NO", cnNo) };
            DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iTMS].[USP_SELECT_BILL_CNS_REF_DTL_FOR_MR_BFD_BY_CN_NO]", CommandType.StoredProcedure, param);
            MR_CNS_BFD _item = new MR_CNS_BFD();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                _item.CN_ID = Convert.ToDecimal(ds.Tables[0].Rows[0]["CN_ID"]);
                _item.CN_NO = Convert.ToString(ds.Tables[0].Rows[0]["CN_NO"]);
            }

            return _item;
        }

        public string INSERT_MR_PRINT(TBL_MR_PRINT mrP)
        {
            string errorMsg = "";
            using (var connection = new SqlConnection(sqlConnection.GetConnectionString()))
            {
                connection.Open();
                SqlCommand command;
                SqlTransaction transactionScope = null;
                transactionScope = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {

                    SqlParameter[] param =
                    { new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                     ,new SqlParameter("@MRP_ID", SqlDbType.Decimal) 
                     ,new SqlParameter("@MRP_MR_ID", mrP.MRP_MR_ID)
                     ,new SqlParameter("@MRP_ADDBY", mrP.MRP_ADDBY) 
                     ,new SqlParameter("@MRP_LOG_TYPE", mrP.MRP_LOG_TYPE) 
                    };

                    param[0].Direction = ParameterDirection.Output;
                    param[1].Direction = ParameterDirection.Output;

                    new DataAccess().InsertWithTransaction("[iTMS].[USP_INSERT_MR_PRINT]", CommandType.StoredProcedure, out command, connection, transactionScope, param);
                    decimal MRP_ID = (decimal)command.Parameters["@MRP_ID"].Value;
                    string error_1 = (string)command.Parameters["@ERRORSTR"].Value;
                    if (MRP_ID == -1) { errorMsg = error_1; }
                    mrP.MRP_ID = MRP_ID;
                    if (errorMsg == "")
                    {
                        transactionScope.Commit();
                    }
                    else
                    {
                        transactionScope.Rollback();
                    }
                }
                catch (Exception)
                {
                    errorMsg = "Error: Exception occured.";
                    transactionScope.Rollback();
                }
                finally
                {
                    connection.Close();
                }
            }
            return errorMsg;
        }

        #endregion

        #region MR ONACCOUNT ADJUSTMENT VIEW
        public MR_OnAccount_Adjust MR_ONACCOUNT_ADJUSTMENT_VIEW(decimal MR_ID)
        {
            MR_OnAccount_Adjust mr = new MR_OnAccount_Adjust();
            List<CN_OR_BILL_DTL> cnsBillDtls = new List<CN_OR_BILL_DTL>();

            SqlParameter[] param = { new SqlParameter("@MRID", MR_ID) };
            DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iTMS].[USP_MR_ONACCOUNT_ADJUSTMENT_VIEW]", CommandType.StoredProcedure, param);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    mr.MR_ID = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_ID"]);
                    mr.MR_NO = Convert.ToString(ds.Tables[0].Rows[0]["MR_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_NO"]);
                    mr.MR_MANUAL_NO = Convert.ToString(ds.Tables[0].Rows[0]["MR_MANUAL_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_MANUAL_NO"]);
                    mr.MR_BR_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["MR_BR_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_BR_ID"]);
                    mr.MR_BR_NAME = Convert.ToString(ds.Tables[0].Rows[0]["MR_BR_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_BR_NAME"]);
                    mr.MR_P_ID = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_P_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_P_ID"]);
                    mr.MR_P_NAME = Convert.ToString(ds.Tables[0].Rows[0]["MR_P_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_P_NAME"]);
                    mr.MR_P_CODE = Convert.ToString(ds.Tables[0].Rows[0]["MR_P_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_P_CODE"]);
                    mr.MR_PA_ID = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_PA_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_PA_ID"]);

                    mr.MR_P_GSTNO = Convert.ToString(ds.Tables[0].Rows[0]["MR_P_GSTNO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_P_GSTNO"]);

                    mr.MR_P_PH_NO = Convert.ToString(ds.Tables[0].Rows[0]["MR_P_PHONENO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_P_PHONENO"]);
                    mr.MR_TYPE = Convert.ToInt32(ds.Tables[0].Rows[0]["MR_TYPE"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_TYPE"]);
                    mr.MR_TYPE_NAME = Convert.ToString(ds.Tables[0].Rows[0]["MR_TYPE_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_TYPE_NAME"]);

                    mr.MR_DOC_TYPE = Convert.ToInt32(ds.Tables[0].Rows[0]["MR_DOC_TYPE"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_DOC_TYPE"]);
                    mr.MR_DOC_TYPE_NAME = Convert.ToString(ds.Tables[0].Rows[0]["MR_DOC_TYPE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_DOC_TYPE"]);

                    mr.MR_DATE1 = Convert.ToString(ds.Tables[0].Rows[0]["MR_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_DATE"]);
                    mr.MR_SUFFIX = Convert.ToString(ds.Tables[0].Rows[0]["MR_SUFFIX"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_SUFFIX"]);
                    mr.DPR_NO = Convert.ToString(ds.Tables[0].Rows[0]["DPR_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["DPR_NO"]);

                    mr.MR_TOTAL_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_TOTAL_AMT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_TOTAL_AMT"]);
                    mr.MR_CN_AMT_EXTRA = Convert.ToBoolean(ds.Tables[0].Rows[0]["MR_CN_AMT_EXTRA"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["MR_CN_AMT_EXTRA"]);
                    mr.TO_CBS_DATE1 = Convert.ToString(ds.Tables[0].Rows[0]["MR_CBS_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_CBS_DATE"]);
                    mr.MR_CBS_BR_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["MR_CBS_BR_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_CBS_BR_ID"]);
                    mr.CBS_STN_CODE = Convert.ToString(ds.Tables[0].Rows[0]["MR_CBS_BR_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_CBS_BR_CODE"]);
                    mr.CBS_STN_NAME = Convert.ToString(ds.Tables[0].Rows[0]["MR_CBS_BR_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_CBS_BR_NAME"]);
                    mr.MR_OLD_ID = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_OLD_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_OLD_ID"]);
                    mr.MR_DCR_ID = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_DCR_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_DCR_ID"]);
                    mr.MR_DPR_STATUS = Convert.ToBoolean(ds.Tables[0].Rows[0]["MR_DPR_STATUS"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["MR_DPR_STATUS"]);
                    mr.MR_DESC = Convert.ToString(ds.Tables[0].Rows[0]["MR_DESC"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_DESC"]);
                    mr.MR_STAX_FRT = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_STAX_FRT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_STAX_FRT"]);
                    mr.MR_STAX = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_STAX"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_STAX"]);
                    mr.MR_REMARKS = Convert.ToString(ds.Tables[0].Rows[0]["MR_REMARKS"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_REMARKS"]);
                    mr.MR_TENDER_NO = Convert.ToString(ds.Tables[0].Rows[0]["MR_TENDER_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_TENDER_NO"]);
                    mr.MR_TENDER_ID = Convert.ToDecimal(ds.Tables[0].Rows[0]["MR_TENDER_ID"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["MR_TENDER_ID"]);
                    mr.CRA_ID = Convert.ToDecimal(ds.Tables[0].Rows[0]["CRA_ID"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["CRA_ID"]);
                    mr.IBT_TYPE_ID = Convert.ToBoolean(ds.Tables[0].Rows[0]["IBT_TYPE_ID"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["IBT_TYPE_ID"]);
                    mr.MRIBT_CRA_NO = Convert.ToString(ds.Tables[0].Rows[0]["CRA_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CRA_NO"]);
                    mr.MRIBT_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["IBT_AMOUNT"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["IBT_AMOUNT"]);

                    mr.REC_PAY_MODE_CASH = Convert.ToBoolean(ds.Tables[0].Rows[0]["CASH_PAY_MODE"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["CASH_PAY_MODE"]);
                    mr.REC_PAY_MODE_CHQ = Convert.ToBoolean(ds.Tables[0].Rows[0]["CHQ_PAY_MODE"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["CHQ_PAY_MODE"]);
                    mr.REC_PAY_MODE_POS = Convert.ToBoolean(ds.Tables[0].Rows[0]["POS_PAY_MODE"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["POS_PAY_MODE"]);

                    mr.ACC_PAY_MODE_CASH = mr.REC_PAY_MODE_CASH;
                    mr.ACC_PAY_MODE_CHQ = mr.REC_PAY_MODE_CHQ;
                    mr.ACC_PAY_MODE_POS = mr.REC_PAY_MODE_POS;

                    mr.CHQ_RTGS_DD_NO = Convert.ToString(ds.Tables[0].Rows[0]["CHQ_RTGS_DD_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CHQ_RTGS_DD_NO"]);
                    mr.CHQ_RTGS_DD_BANK_NAME = Convert.ToString(ds.Tables[0].Rows[0]["CHQ_RTGS_DD_BANK_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CHQ_RTGS_DD_BANK_NAME"]);
                    mr.CHQ_RTGS_DD_DATE1 = Convert.ToString(ds.Tables[0].Rows[0]["CHQ_RTGS_DD_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CHQ_RTGS_DD_DATE"]);

                    mr.POS_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["POS_AMOUNT"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["POS_AMOUNT"]);
                    mr.CASH_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["CASH_AMOUNT"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["CASH_AMOUNT"]);
                    mr.CHQ_RTGS_DD_AMT = Convert.ToDecimal(ds.Tables[0].Rows[0]["CHQ_AMOUNT"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["CHQ_AMOUNT"]);
                    mr.POS_TRAN_NO = Convert.ToString(ds.Tables[0].Rows[0]["POS_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["POS_NO"]);
                    mr.EMP_CODE = Convert.ToString(ds.Tables[0].Rows[0]["EMPLOYEE_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["EMPLOYEE_NAME"]);

                    mr.MR_STATUS = Convert.ToBoolean(ds.Tables[0].Rows[0]["MR_STATUS"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["MR_STATUS"]);

                    mr.MR_TOTAL_AMT = Convert.ToDecimal(mr.CASH_AMT ?? 0) + Convert.ToDecimal(mr.POS_AMT ?? 0) + Convert.ToDecimal(mr.CHQ_RTGS_DD_AMT ?? 0);

                    if (mr.REC_PAY_MODE_POS == true && Convert.ToString(mr.CHQ_RTGS_DD_BANK_NAME) == "")
                    {
                        mr.CHQ_RTGS_DD_BANK_NAME = Convert.ToString(ds.Tables[0].Rows[0]["POS_BANK_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["POS_BANK_NAME"]);
                    }

                    mr.MR_ADJ_THRU = Convert.ToString(ds.Tables[0].Rows[0]["MR_ADJ_THRU"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_ADJ_THRU"]);
                }

                decimal totalOPMCAmt = 0;
                decimal totalFRTAmt = 0;
                decimal totalDCCAmt = 0;
                decimal totalMHCAmt = 0;
                decimal totalCPEAmt = 0;
                decimal totalOCTAmt = 0;
                decimal totalOMCAmt = 0;
                decimal totalDelChgsAmt = 0;
                decimal totalMiscAmt = 0;
                decimal totalGodownChgsAmt = 0;
                decimal totalGTXAmt = 0;
                decimal totalOTHAmt = 0;
                decimal totalCollAmt = 0;
                decimal totalDednAmt = 0;
                decimal totalNetRecdAmt = 0;

                if (ds.Tables.Count > 1)
                {
                    if (ds.Tables[1].Rows.Count > 0 && Convert.ToDouble(ds.Tables[1].Rows[0]["MR_CN_ID"]) > 0)
                    {
                        foreach (DataRow row in ds.Tables[1].Rows)
                        {
                            decimal FRT_AMT = Convert.ToDecimal(row["CN_OR_BILL_FRT_AMT"] == DBNull.Value ? "0" : row["CN_OR_BILL_FRT_AMT"]);
                            totalFRTAmt += FRT_AMT;
                            decimal DEMM_AMT = Convert.ToDecimal(row["MR_DEMM_AMT"] == DBNull.Value ? "0" : row["MR_DEMM_AMT"]);
                            totalDCCAmt += DEMM_AMT;
                            decimal HNDL_AMT = Convert.ToDecimal(row["MR_HNDL_AMT"] == DBNull.Value ? "0" : row["MR_HNDL_AMT"]);
                            totalMHCAmt += HNDL_AMT;
                            decimal OCT_AMT = Convert.ToDecimal(row["MR_OCT_AMT"] == DBNull.Value ? "0" : row["MR_OCT_AMT"]);
                            totalOCTAmt += OCT_AMT;
                            decimal OCS_AMT = Convert.ToDecimal(row["MR_OCS_AMT"] == DBNull.Value ? "0" : row["MR_OCS_AMT"]);
                            totalOMCAmt += OCS_AMT;
                            decimal DLVCH_AMT = Convert.ToDecimal(row["MR_DLVCH_AMT"] == DBNull.Value ? "0" : row["MR_DLVCH_AMT"]);
                            totalDelChgsAmt += DLVCH_AMT;
                            decimal MISC_AMT = Convert.ToDecimal(row["MR_MISC_AMT"] == DBNull.Value ? "0" : row["MR_MISC_AMT"]);
                            totalMiscAmt += MISC_AMT;
                            decimal OTH_AMT = Convert.ToDecimal(row["MR_OTH_AMT"] == DBNull.Value ? "0" : row["MR_OTH_AMT"]);
                            totalOTHAmt += OTH_AMT;
                            decimal OPMC_AMT = Convert.ToDecimal(row["MR_OPMC_AMT"] == DBNull.Value ? "0" : row["MR_OPMC_AMT"]);
                            totalOPMCAmt += OPMC_AMT;
                            decimal GTX_AMT = Convert.ToDecimal(row["MR_GTX_AMT"] == DBNull.Value ? "0" : row["MR_GTX_AMT"]);
                            totalGTXAmt += GTX_AMT;
                            decimal CPE_AMT = Convert.ToDecimal(row["MR_CPE_AMT"] == DBNull.Value ? "0" : row["MR_CPE_AMT"]);
                            totalCPEAmt += CPE_AMT;
                            decimal GDN_CHRG_AMT = Convert.ToDecimal(row["MR_GDN_CHRG_AMT"] == DBNull.Value ? "0" : row["MR_GDN_CHRG_AMT"]);
                            totalGodownChgsAmt += GDN_CHRG_AMT;

                            decimal TOTAL_COLL_AMT = OPMC_AMT + FRT_AMT + DEMM_AMT + HNDL_AMT + CPE_AMT + OCT_AMT + OCS_AMT + DLVCH_AMT + MISC_AMT + GDN_CHRG_AMT + OTH_AMT;

                            totalCollAmt += TOTAL_COLL_AMT;


                            decimal MRBFD_CLAM_AMT = Convert.ToDecimal(row["MRBFD_CLAM_AMT"] == DBNull.Value ? "0" : row["MRBFD_CLAM_AMT"]);
                            decimal MRBFD_NREC_AMT = Convert.ToDecimal(row["MRBFD_NREC_AMT"] == DBNull.Value ? "0" : row["MRBFD_NREC_AMT"]);
                            decimal MRBFD_EMD_AMT = Convert.ToDecimal(row["MRBFD_EMD_AMT"] == DBNull.Value ? "0" : row["MRBFD_EMD_AMT"]);
                            decimal MRBFD_SD_AMT = MRBFD_SD_AMT = Convert.ToDecimal(row["MRBFD_SD_AMT"] == DBNull.Value ? "0" : row["MRBFD_SD_AMT"]);
                            decimal MRBFD_TDS_AMT = Convert.ToDecimal(row["MRBFD_TDS_AMT"] == DBNull.Value ? "0" : row["MRBFD_TDS_AMT"]);
                            decimal RECO_AMT = Convert.ToDecimal(row["MRBFD_RECO_AMT"] == DBNull.Value ? "0" : row["MRBFD_RECO_AMT"]);

                            decimal TOTAL_DED_AMT = MRBFD_CLAM_AMT + MRBFD_NREC_AMT + MRBFD_EMD_AMT + MRBFD_SD_AMT + MRBFD_TDS_AMT + RECO_AMT;
                            totalDednAmt += TOTAL_DED_AMT;

                            decimal NET_RECEIVED_AMT = (TOTAL_COLL_AMT - TOTAL_DED_AMT);
                            totalNetRecdAmt += NET_RECEIVED_AMT;

                            cnsBillDtls.Add(new CN_OR_BILL_DTL
                            {

                                CN_OR_BILL_FRT_AMT = Convert.ToDecimal(row["CN_OR_BILL_FRT_AMT"] == DBNull.Value ? "0" : row["CN_OR_BILL_FRT_AMT"]),

                                MR_DEMM_AMT = Convert.ToDecimal(row["MR_DEMM_AMT"] == DBNull.Value ? "0" : row["MR_DEMM_AMT"]),
                                MR_HNDL_AMT = Convert.ToDecimal(row["MR_HNDL_AMT"] == DBNull.Value ? "0" : row["MR_HNDL_AMT"]),
                                MR_OCT_AMT = Convert.ToDecimal(row["MR_OCT_AMT"] == DBNull.Value ? "0" : row["MR_OCT_AMT"]),
                                MR_OCS_AMT = Convert.ToDecimal(row["MR_OCS_AMT"] == DBNull.Value ? "0" : row["MR_OCS_AMT"]),
                                MR_DLVCH_AMT = Convert.ToDecimal(row["MR_DLVCH_AMT"] == DBNull.Value ? "0" : row["MR_DLVCH_AMT"]),
                                MR_MISC_AMT = Convert.ToDecimal(row["MR_MISC_AMT"] == DBNull.Value ? "0" : row["MR_MISC_AMT"]),
                                MR_OTH_AMT = Convert.ToDecimal(row["MR_OTH_AMT"] == DBNull.Value ? "0" : row["MR_OTH_AMT"]),
                                MR_OPMC_AMT = Convert.ToDecimal(row["MR_OPMC_AMT"] == DBNull.Value ? "0" : row["MR_OPMC_AMT"]),
                                MR_GTX_AMT = Convert.ToDecimal(row["MR_GTX_AMT"] == DBNull.Value ? "0" : row["MR_GTX_AMT"]),
                                MR_CPE_AMT = Convert.ToDecimal(row["MR_CPE_AMT"] == DBNull.Value ? "0" : row["MR_CPE_AMT"]),
                                MR_GDN_CHRG_AMT = Convert.ToDecimal(row["MR_GDN_CHRG_AMT"] == DBNull.Value ? "0" : row["MR_GDN_CHRG_AMT"]),
                                MR_BFD_AMT = Convert.ToDecimal(row["MR_BFD_AMT"] == DBNull.Value ? "0" : row["MR_BFD_AMT"]),
                                MR_SUB_TOTAL_AMT = Convert.ToDecimal(row["MR_SUB_TOTAL_AMT"] == DBNull.Value ? "0" : row["MR_SUB_TOTAL_AMT"]),
                                MR_STAX_FRT = Convert.ToDecimal(row["MR_STAX_FRT"] == DBNull.Value ? "0" : row["MR_STAX_FRT"]),
                                MR_STAX = Convert.ToDecimal(row["MR_STAX"] == DBNull.Value ? "0" : row["MR_STAX"]),
                                MR_COD_CQNO_1 = Convert.ToString(row["MR_COD_CQNO"] == DBNull.Value ? "" : row["MR_COD_CQNO"]),
                                MR_COD_AMT = Convert.ToDecimal(row["MR_COD_AMT"] == DBNull.Value ? "0" : row["MR_COD_AMT"]),
                                MR_CN_BILL_STATUS = Convert.ToBoolean(row["MR_CN_BILL_STATUS"] == DBNull.Value ? 0 : row["MR_CN_BILL_STATUS"]),
                                CN_OR_BILL_NO = Convert.ToString(row["CN_OR_BILL_NO"] == DBNull.Value ? "" : row["CN_OR_BILL_NO"]),
                                CN_OR_BILL_DATE1 = Convert.ToString(row["CN_OR_BILL_DATE1"] == DBNull.Value ? "" : row["CN_OR_BILL_DATE1"]),
                                CN_OR_BILL_BR_NAME = Convert.ToString(row["CN_OR_BILL_BR_NAME"] == DBNull.Value ? "" : row["CN_OR_BILL_BR_NAME"]),
                                MRBFD_CLAM_AMT = Convert.ToDecimal(row["MRBFD_CLAM_AMT"] == DBNull.Value ? "0" : row["MRBFD_CLAM_AMT"]),
                                MRBFD_RECO_AMT = Convert.ToDecimal(row["MRBFD_RECO_AMT"] == DBNull.Value ? "0" : row["MRBFD_RECO_AMT"]),
                                MRBFD_NREC_AMT = Convert.ToDecimal(row["MRBFD_NREC_AMT"] == DBNull.Value ? "0" : row["MRBFD_NREC_AMT"]),
                                MRBFD_BFDR_ID = Convert.ToDecimal(row["MRBFD_BFDR_ID"] == DBNull.Value ? "0" : row["MRBFD_BFDR_ID"]),
                                MRBFD_EMD_AMT = Convert.ToDecimal(row["MRBFD_EMD_AMT"] == DBNull.Value ? "0" : row["MRBFD_EMD_AMT"]),
                                MRBFD_SD_AMT = Convert.ToDecimal(row["MRBFD_SD_AMT"] == DBNull.Value ? "0" : row["MRBFD_SD_AMT"]),
                                MR_TDS_PER = Convert.ToDecimal(row["MRBFD_TDS_PER"] == DBNull.Value ? "0" : row["MRBFD_TDS_PER"]),
                                MR_TDS_AMT = Convert.ToDecimal(row["MRBFD_TDS_AMT"] == DBNull.Value ? "0" : row["MRBFD_TDS_AMT"]),
                                MRBFD_BFDR_TXT = Convert.ToString(row["N_CODE_NAME"] == DBNull.Value ? "" : row["N_CODE_NAME"]),
                                MR_TOTAL_AMT = TOTAL_COLL_AMT,

                                TOT_DEDN_AMT = TOTAL_DED_AMT,

                                NET_RECD_AMT = NET_RECEIVED_AMT,
                            });
                        }
                        mr.BILL_CNS_DTL_LIST = cnsBillDtls;
                    }
                }

                mr.MR_DEMM_AMT = totalDCCAmt;
                mr.MR_HNDL_AMT = totalMHCAmt;
                mr.MR_OCT_AMT = totalOCTAmt;

                mr.MR_DLVCH_AMT = totalDelChgsAmt;
                mr.MR_MISC_AMT = totalMiscAmt;
                mr.MR_OTH_AMT = totalOTHAmt + totalOMCAmt + totalOCTAmt;
                mr.MR_OPMC_AMT = totalOPMCAmt;
                mr.MR_GTX_AMT = totalGTXAmt;
                mr.MR_CPE_AMT = totalCPEAmt;
                mr.MR_GDN_CHRG_AMT = totalGodownChgsAmt;
                mr.MR_BFD_AMT = totalDednAmt;

                mr.SUB_TOTAL = (mr.MR_DEMM_AMT + mr.MR_HNDL_AMT + mr.MR_DLVCH_AMT + mr.MR_MISC_AMT + mr.MR_OPMC_AMT + mr.MR_GDN_CHRG_AMT + mr.MR_CPE_AMT + mr.MR_GTX_AMT + mr.MR_OTH_AMT);
                mr.TOTAL_I = mr.SUB_TOTAL;

                mr.TOTAL_FRT_II = totalFRTAmt;
                mr.TOTAL_GRAND = Convert.ToDecimal(mr.TOTAL_I ?? 0) + Convert.ToDecimal(mr.TOTAL_FRT_II ?? 0);
                mr.MR_NET_RECEIVED = Convert.ToDecimal(totalCollAmt) - Convert.ToDecimal(totalDednAmt);
                mr.MR_SUB_TOTAL_AMT = Convert.ToDecimal(totalCollAmt);

                if (ds.Tables.Count == 3)
                {
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        List<MR_ADJ_DTL> _list = new List<MR_ADJ_DTL>();
                        foreach (DataRow row in ds.Tables[2].Rows)
                        {
                            _list.Add(new MR_ADJ_DTL
                            {
                                MR_ADV_NO = Convert.ToString(row["MR_ADV_NO"] == DBNull.Value ? "" : row["MR_ADV_NO"]),
                                MR_ADV_BR_ID = Convert.ToInt32(row["MR_ADV_BR_ID"] == DBNull.Value ? "0" : row["MR_ADV_BR_ID"]),
                                MR_ADV_BR_NAME = Convert.ToString(row["MR_ADV_BR_NAME"] == DBNull.Value ? "" : row["MR_ADV_BR_NAME"]),
                                MR_ADV_DATE = Convert.ToString(row["MR_ADV_DATE"] == DBNull.Value ? "" : row["MR_ADV_DATE"]),
                                MR_ADV_AMT = Convert.ToDecimal(row["MR_ADV_AMT"] == DBNull.Value ? "0" : row["MR_ADV_AMT"]),
                                MR_ADV_ADJ_AMT = Convert.ToDecimal(row["MR_ADV_ADJ_AMT"] == DBNull.Value ? "0" : row["MR_ADV_ADJ_AMT"]),
                                MR_ADV_P_NAME = Convert.ToString(row["MR_ADV_P_NAME"] == DBNull.Value ? "" : row["MR_ADV_P_NAME"])
                            });
                        }

                        mr.MR_ADJ_LIST = _list;
                    }
                }
            }
            return mr;
        }

        #endregion

        #region MR CBS DATE ADJUSTMENT

        public CBS_DATE_ADJUSTMENT GET_MR_CBS_INFO(int DOC_TYPE, string DOC_NO)
        {
            SqlParameter[] param = { new SqlParameter("@DOC_TYPE_ID", DOC_TYPE), new SqlParameter("@DOC_NO", DOC_NO) };
            using (DataTable dt = new DataAccess(sqlConnection.GetConnectionString()).GetDataTable("[iFMS].[USP_GET_CN_MR_INFO_FOR_CBS_DATE_ADJUST]", CommandType.StoredProcedure, param))
            {
                CBS_DATE_ADJUSTMENT mr = new CBS_DATE_ADJUSTMENT();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        mr.CN_ID = Convert.ToDecimal(row["CN_ID"] == DBNull.Value ? "0" : row["CN_ID"]);
                        mr.CN_NO = Convert.ToString(row["CN_NO"] == DBNull.Value ? "" : row["CN_NO"]);
                        mr.CN_DATE_1 = Convert.ToString(row["CN_DATE"] == DBNull.Value ? "" : row["CN_DATE"]);
                        mr.CN_BOOKING_STN_ID = Convert.ToInt32(row["CN_BOOKING_BR_ID"] == DBNull.Value ? "0" : row["CN_BOOKING_BR_ID"]);
                        mr.CN_BOOKING_STN = Convert.ToString(row["CN_BOOKING_BR_NAME"] == DBNull.Value ? "" : row["CN_BOOKING_BR_NAME"]);
                        mr.CN_STATUS = Convert.ToString(row["CN_STATUS"] == DBNull.Value ? "0" : row["CN_STATUS"]);
                        mr.MR_ID = Convert.ToDecimal(row["MR_ID"] == DBNull.Value ? "0" : row["MR_ID"]);
                        mr.MR_NO = Convert.ToString(row["MR_NO"] == DBNull.Value ? "" : row["MR_NO"]);
                        mr.MR_DATE_1 = Convert.ToString(row["MR_DATE"] == DBNull.Value ? "" : row["MR_DATE"]);
                        mr.MR_TYPE = Convert.ToString(row["MR_TYPE"] == DBNull.Value ? "0" : row["MR_TYPE"]);
                        mr.MR_BR_ID = Convert.ToInt32(row["MR_BR_ID"] == DBNull.Value ? "0" : row["MR_BR_ID"]);
                        mr.MR_BR_NAME = Convert.ToString(row["MR_BR_NAME"] == DBNull.Value ? "0" : row["MR_BR_NAME"]);
                        mr.MR_STMT_NO = Convert.ToInt32(row["MR_STMT_NO"] == DBNull.Value ? 0 : row["MR_STMT_NO"]);
                        mr.MRSD_MRS_ID = Convert.ToInt32(row["MRSD_MRS_ID"] == DBNull.Value ? 0 : row["MRSD_MRS_ID"]);
                        mr.MR_STATUS = Convert.ToBoolean(row["MR_STATUS"] == DBNull.Value ? 0 : row["MR_STATUS"]);
                    }
                }
                return mr;
            }
        }

        public CBS_DATE_ADJUSTMENT GET_PAID_MR_DTLS(decimal MR_ID)
        {
            SqlParameter[] param = { new SqlParameter("@MR_ID", MR_ID) };
            using (DataTable dt = new DataAccess(sqlConnection.GetConnectionString()).GetDataTable("[iFMS].[USP_GET_PAID_MR_DTLS]", CommandType.StoredProcedure, param))
            {
                CBS_DATE_ADJUSTMENT mr = new CBS_DATE_ADJUSTMENT();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        mr.CN_ID = Convert.ToDecimal(row["CN_ID"] == DBNull.Value ? "0" : row["CN_ID"]);
                        mr.CN_NO = Convert.ToString(row["CN_NO"] == DBNull.Value ? "" : row["CN_NO"]);
                        mr.CN_DATE_1 = Convert.ToString(row["CN_DATE"] == DBNull.Value ? "" : row["CN_DATE"]);
                        mr.CN_BOOKING_STN_ID = Convert.ToInt32(row["CN_BOOKING_STN_ID"] == DBNull.Value ? "0" : row["CN_BOOKING_STN_ID"]);
                        mr.CN_BOOKING_STN = Convert.ToString(row["CN_BOOKING_STN"] == DBNull.Value ? "" : row["CN_BOOKING_STN"]);
                        mr.CN_DEST_STN = Convert.ToString(row["CN_DEST_STN"] == DBNull.Value ? "" : row["CN_DEST_STN"]);
                        mr.CNOR_NAME = Convert.ToString(row["CNOR_NAME"] == DBNull.Value ? "" : row["CNOR_NAME"]);
                        mr.CNEE_NAME = Convert.ToString(row["CNEE_NAME"] == DBNull.Value ? "" : row["CNEE_NAME"]);
                        mr.CN_FRT_AMT = Convert.ToString(row["CN_AMT"] == DBNull.Value ? "" : row["CN_AMT"]);
                        mr.MR_ID = Convert.ToDecimal(row["MRID"] == DBNull.Value ? "0" : row["MRID"]);
                        mr.MR_NO = Convert.ToString(row["MRNO"] == DBNull.Value ? "" : row["MRNO"]);
                        mr.MR_DATE_1 = Convert.ToString(row["MR_DATE"] == DBNull.Value ? "" : row["MR_DATE"]);
                        mr.MR_BR_ID = Convert.ToInt32(row["MR_BR_ID"] == DBNull.Value ? "" : row["MR_BR_ID"]);
                        mr.MR_AMT = Convert.ToDecimal(row["MR_AMOUNT"] == DBNull.Value ? "0" : row["MR_AMOUNT"]);
                        mr.TDS_AMT = Convert.ToDecimal(row["MR_TDS_AMOUNT"] == DBNull.Value ? "0" : row["MR_TDS_AMOUNT"]);
                        mr.NET_MR_AMT = Convert.ToDecimal(row["MR_TOTAL_AMOUNT"] == DBNull.Value ? "0" : row["MR_TOTAL_AMOUNT"]);
                        mr.MR_STMT_NO = Convert.ToInt32(row["MR_STMT_NO"] == DBNull.Value ? 0 : row["MR_STMT_NO"]);
                        mr.MR_CBS_BR_ID = Convert.ToInt32(row["MR_CBS_BR_ID"] == DBNull.Value ? 0 : row["MR_CBS_BR_ID"]);
                        mr.MR_CBS_BR_CODE = Convert.ToString(row["MR_CBS_BR_CODE"] == DBNull.Value ? "" : row["MR_CBS_BR_CODE"]);
                        mr.MR_CBS_BR_NAME = Convert.ToString(row["MR_CBS_BR_NAME"] == DBNull.Value ? "" : row["MR_CBS_BR_NAME"]);
                        mr.MR_CBS_DATE_1 = Convert.ToString(row["MR_CBS_DATE"] == DBNull.Value ? "" : row["MR_CBS_DATE"]);
                        mr.MR_CBS_FROM_DATE_1 = Convert.ToString(row["MR_CBS_FROM_DATE"] == DBNull.Value ? "" : row["MR_CBS_FROM_DATE"]);
                        mr.MR_CBS_TO_DATE_1 = Convert.ToString(row["MR_CBS_TO_DATE"] == DBNull.Value ? "" : row["MR_CBS_TO_DATE"]);
                    }
                }
                return mr;
            }
        }
        public string UPDATE_PAID_MR_CBS_DATE(CBS_DATE_ADJUSTMENT mr)
        {
            string errorMsg = "";
            using (var connection = new SqlConnection(sqlConnection.GetConnectionString()))
            {
                connection.Open();
                SqlCommand command;
                SqlTransaction transactionScope = null;
                transactionScope = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    SqlParameter[] param =
                    {
                         new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)                       
                        ,new SqlParameter("@MR_ID",(mr.MR_ID == null) ? (object)DBNull.Value : mr.MR_ID) 
                        ,new SqlParameter("@MR_BR_ID", (mr.MR_BR_ID == null) ? (object)DBNull.Value : mr.MR_BR_ID) 
                        ,new SqlParameter("@MR_CBS_BR_ID",( mr.NEW_CBS_BR_ID == null) ? (object)DBNull.Value : mr.NEW_CBS_BR_ID) 
                        ,new SqlParameter("@MR_CBS_DATE",( mr.NEW_CBS_DATE == null) ? (object)DBNull.Value : mr.NEW_CBS_DATE) 
                        ,new SqlParameter("@MR_STMT_NO",( mr.MR_STMT_NO == null) ? (object)DBNull.Value : mr.MR_STMT_NO)  
                    };
                    param[0].Direction = ParameterDirection.Output;
                    new DataAccess().InsertWithTransaction("[iTMS].[USP_UPDATE_MR_CBS_DATE]", CommandType.StoredProcedure, out command, connection, transactionScope, param);
                    string error_1 = (string)command.Parameters["@ERRORSTR"].Value;
                    errorMsg = error_1;

                    if (errorMsg == "")
                    {

                        SqlParameter[] param1 = {
                                              new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                                             ,new SqlParameter("@HIS_ID", SqlDbType.Decimal) 
                                             ,new SqlParameter("@HIS_DATE",mr.MR_DATE_1 )
                                             ,new SqlParameter("@HIS_MR_ID",mr.MR_ID )
                                             ,new SqlParameter("@HIS_MR_NO", mr.MR_NO)
                                             ,new SqlParameter("@HIS_REMARKS", "CBS DATE UPPDATED")
                                             ,new SqlParameter("@HIS_PROCESS_ID", 2)
                                             ,new SqlParameter("@HIS_PROCESS_NAME", "CBS DATE UPPDATED")
                                             ,new SqlParameter("@HIS_ADDTYPE", mr.MR_ADD_BY_TYPE)
                                             ,new SqlParameter("@HIS_ADDBY", mr.MR_ADD_BY)
                                            };

                        param1[0].Direction = ParameterDirection.Output;
                        param1[1].Direction = ParameterDirection.Output;
                        new DataAccess().InsertWithTransaction("[iTMS].[INSERT_HIS_MR]", CommandType.StoredProcedure, out command, connection, transactionScope, param1);
                        string error_2 = (string)command.Parameters["@ERRORSTR"].Value;
                        decimal HIS_ID = (decimal)command.Parameters["@HIS_ID"].Value;
                        if (HIS_ID == -1) { errorMsg = error_2; }
                    }
                    if (errorMsg == "")
                    {
                        transactionScope.Commit();
                    }
                    else
                    {
                        transactionScope.Rollback();
                    }
                }
                catch (Exception)
                {
                    errorMsg = "Error: Exception occured.";
                    transactionScope.Rollback();
                }
                finally
                {
                    connection.Close();
                }
            }
            return errorMsg;
        }
        #endregion

        #region MR Delete

        public MR_DELETE GET_MR_DTLS_BY_MR_NO_FOR_DELETE(int BR_ID, string MR_NO)
        {
            SqlParameter[] param = { new SqlParameter("@BR_ID", BR_ID), new SqlParameter("@MR_NO", MR_NO) };
            using (DataTable dt = new DataAccess(sqlConnection.GetConnectionString()).GetDataTable("[iTMS].[USP_GET_MR_DTLS_BY_MR_NO_FOR_DELETE]", CommandType.StoredProcedure, param))
            {
                MR_DELETE _mrDel = new MR_DELETE();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        _mrDel.MR_ID = Convert.ToInt32(row["MR_ID"] == DBNull.Value ? "0" : row["MR_ID"]);
                        _mrDel.MR_NO = Convert.ToString(row["MR_NO"] == DBNull.Value ? "" : row["MR_NO"]);
                        _mrDel.MR_DATE = Convert.ToString(row["MR_DATE"] == DBNull.Value ? "" : row["MR_DATE"]);
                        _mrDel.MR_PTY_NAME = Convert.ToString(row["MR_PARTY_NAME"] == DBNull.Value ? "" : row["MR_PARTY_NAME"]);
                        _mrDel.MR_AMT = Convert.ToString(row["MR_TOTAL_AMT"] == DBNull.Value ? "" : row["MR_TOTAL_AMT"]);
                        _mrDel.MR_CBS_BR_NM = Convert.ToString(row["MR_CBS_BR"] == DBNull.Value ? "" : row["MR_CBS_BR"]);
                        _mrDel.MR_CBS_DATE = Convert.ToString(row["MR_CBS_DT"] == DBNull.Value ? "" : row["MR_CBS_DT"]);
                        _mrDel.MR_CBS_STATUS = Convert.ToString(row["MR_CBS_STATUS"] == DBNull.Value ? "" : row["MR_CBS_STATUS"]);
                        _mrDel.MR_PRINT_STATUS = Convert.ToString(row["MR_PRINT_STATUS"] == DBNull.Value ? "" : row["MR_PRINT_STATUS"]);

                        //Added by pramesh, 09-09-2022 
                        _mrDel.MR_CBS_BR_ID = Convert.ToInt32(row["MR_CBS_BR_ID"] == DBNull.Value ? "0" : row["MR_CBS_BR_ID"]);
                    }
                }
                return _mrDel;
            }
        }

        public string DELETE_TBL_MR_HDR(MR_DELETE _mrDel)
        {
            string errorMsg = "";
            using (var connection = new SqlConnection(sqlConnection.GetConnectionString()))
            {
                connection.Open();
                SqlCommand command;
                SqlTransaction transactionScope = null;
                transactionScope = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    SqlParameter[] param =
                                           {
                                              new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                                             ,new SqlParameter("@HIS_ID", SqlDbType.Decimal) 
                                             ,new SqlParameter("@HIS_DATE",_mrDel.MR_DATE )
                                             ,new SqlParameter("@HIS_MR_ID",_mrDel.MR_ID )
                                             ,new SqlParameter("@HIS_MR_NO", _mrDel.MR_NO)
                                             ,new SqlParameter("@HIS_REMARKS", _mrDel.MR_DELETE_REMARK)
                                             ,new SqlParameter("@HIS_PROCESS_ID", 3)
                                             ,new SqlParameter("@HIS_PROCESS_NAME", "MR DELETE")
                                             ,new SqlParameter("@HIS_ADDTYPE", _mrDel.MR_DELETE_BY_TYPE)
                                             ,new SqlParameter("@HIS_ADDBY", _mrDel.MR_DELETE_BY)
                                            };

                    param[0].Direction = ParameterDirection.Output;
                    param[1].Direction = ParameterDirection.Output;
                    new DataAccess().InsertWithTransaction("[iTMS].[INSERT_HIS_MR]", CommandType.StoredProcedure, out command, connection, transactionScope, param);
                    string error_1 = (string)command.Parameters["@ERRORSTR"].Value;
                    decimal HIS_ID = (decimal)command.Parameters["@HIS_ID"].Value;
                    errorMsg = error_1;

                    if (errorMsg == "")
                    {

                        SqlParameter[] param1 = {
                                              new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)                                           
                                             ,new SqlParameter("@MR_ID",_mrDel.MR_ID )
                                             ,new SqlParameter("@MR_HIS_ID", HIS_ID)                                             
                                            };

                        param1[0].Direction = ParameterDirection.Output;
                        new DataAccess().InsertWithTransaction("[iTMS].[USP_INSERT_TBL_DELETED_MR_HDR]", CommandType.StoredProcedure, out command, connection, transactionScope, param1);
                        string error_2 = (string)command.Parameters["@ERRORSTR"].Value;
                        errorMsg = error_2;


                        if (errorMsg == "")
                        {

                            SqlParameter[] param2 = {
                                              new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)                                           
                                             ,new SqlParameter("@MR_ID",_mrDel.MR_ID )                                                                                        
                                            };

                            param2[0].Direction = ParameterDirection.Output;
                            new DataAccess().InsertWithTransaction("[iTMS].[USP_DELETE_TBL_MR_HDR]", CommandType.StoredProcedure, out command, connection, transactionScope, param2);
                            string error_3 = (string)command.Parameters["@ERRORSTR"].Value;
                            errorMsg = error_3;
                        }
                    }


                    if (errorMsg == "")
                    {
                        transactionScope.Commit();
                    }
                    else
                    {
                        transactionScope.Rollback();
                    }
                }
                catch (Exception)
                {
                    errorMsg = "Error: Exception occured.";
                    transactionScope.Rollback();
                }
                finally
                {
                    connection.Close();
                }
            }
            return errorMsg;
        }

        #endregion

        // Added by Pramesh kumar Vishwakarma, Date:17-08-2022
        public string CHECK_HO_APPROVAL_FOR_MISC_PARTY(int brId, int mrType, string mrNo)
        {
            SqlParameter[] param = { new SqlParameter("@BR_ID", brId), new SqlParameter("@MR_TYPE_ID", mrType), new SqlParameter("@MR_NO", mrNo) };
            string msg = "";
            using (DataTable dt = new DataAccess(sqlConnection.GetConnectionString()).GetDataTable("[iTMS].[USP_CHECK_HO_APPROVAL_FOR_MISC_PARTY]", CommandType.StoredProcedure, param))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    msg = Convert.ToString(dt.Rows[0]["MSG_ERR"]);
                }
            }
            return msg;
        }

        // Added by Pramesh kumar Vishwakarma, Date:18-08-2022
        public MR_INFO_FOR_PARTY_UPDATE GET_MR_INFO_FOR_PARTY_UPDATE(string MR_NO)
        {
            SqlParameter[] param = { new SqlParameter("@MR_NO", MR_NO) };
            using (DataTable dt = new DataAccess(sqlConnection.GetConnectionString()).GetDataTable("[iTMS].[USP_GET_MR_INFO_FOR_PARTY_UPDATE]", CommandType.StoredProcedure, param))
            {
                MR_INFO_FOR_PARTY_UPDATE mr = new MR_INFO_FOR_PARTY_UPDATE();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        mr.MR_ID = Convert.ToDecimal(row["MR_ID"] == DBNull.Value ? "0" : row["MR_ID"]);
                        mr.MR_NO = Convert.ToString(row["MR_NO"] == DBNull.Value ? "" : row["MR_NO"]);
                        mr.MR_DATE = Convert.ToString(row["MR_DATE"] == DBNull.Value ? "" : row["MR_DATE"]);
                        mr.MR_BR_ID = Convert.ToInt32(row["MR_BR_ID"] == DBNull.Value ? "0" : row["MR_BR_ID"]);
                        mr.MR_BR_NAME = Convert.ToString(row["MR_BR_NAME"] == DBNull.Value ? "" : row["MR_BR_NAME"]);
                        mr.MR_STATUS = Convert.ToBoolean(row["MR_STATUS"] == DBNull.Value ? 0 : row["MR_STATUS"]);
                        mr.IS_MR_PRINT = Convert.ToInt32(row["IS_MR_PRINT"] == DBNull.Value ? "0" : row["IS_MR_PRINT"]);
                        mr.MR_TYPE = Convert.ToString(row["MR_TYPE"] == DBNull.Value ? "0" : row["MR_TYPE"]);
                        mr.MR_IS_ADJ = Convert.ToBoolean(row["MR_IS_ADJ"] == DBNull.Value ? 0 : row["MR_IS_ADJ"]);

                        mr.MR_PA_ID = Convert.ToInt32(row["MR_PA_ID"] == DBNull.Value ? "0" : row["MR_PA_ID"]);
                        mr.MR_TYPE_NAME = Convert.ToString(row["MR_TYPE_NAME"] == DBNull.Value ? "0" : row["MR_TYPE_NAME"]);
                    }
                }

                return mr;
            }
        }

        public string Update_From_Misc_To_Valid_Party_In_MR(MR_INFO_FOR_PARTY_UPDATE mr)
        {
            string errorMsg = "";
            using (var connection = new SqlConnection(sqlConnection.GetConnectionString()))
            {
                connection.Open();
                SqlCommand command;
                SqlTransaction transactionScope = null;
                transactionScope = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    SqlParameter[] param = {
                                              new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                                             ,new SqlParameter("@HIS_ID", SqlDbType.Decimal) 
                                             ,new SqlParameter("@HIS_DATE",mr.MR_DATE )
                                             ,new SqlParameter("@HIS_MR_ID",mr.MR_ID )
                                             ,new SqlParameter("@HIS_MR_NO", mr.MR_NO)
                                             ,new SqlParameter("@HIS_REMARKS","Misc To Valid Party")
                                             ,new SqlParameter("@HIS_PROCESS_ID", 4)
                                             ,new SqlParameter("@HIS_PROCESS_NAME", "MR UPDATE")
                                             ,new SqlParameter("@HIS_ADDTYPE", mr.MR_UPDATED_BY_TYPE)
                                             ,new SqlParameter("@HIS_ADDBY", mr.MR_UPDATED_BY)
                                           };

                    param[0].Direction = ParameterDirection.Output;
                    param[1].Direction = ParameterDirection.Output;
                    new DataAccess().InsertWithTransaction("[iTMS].[INSERT_HIS_MR]", CommandType.StoredProcedure, out command, connection, transactionScope, param);
                    string error_1 = (string)command.Parameters["@ERRORSTR"].Value;
                    decimal HIS_ID = (decimal)command.Parameters["@HIS_ID"].Value;
                    errorMsg = error_1;

                    if (errorMsg == "")
                    {
                        SqlParameter[] param2 = { 
                                                    new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                                                    , new SqlParameter("@MR_ID", mr.MR_ID)
                                                    , new SqlParameter("@MR_P_ID", mr.MR_P_ID)
                                                    , new SqlParameter("@MR_PA_ID", mr.MR_PA_ID) 
                                                    , new SqlParameter("@MR_PARTY_NAME", mr.MR_PARTY_NAME_ONLY) 
                                                };

                        param2[0].Direction = ParameterDirection.Output;
                        new DataAccess().InsertWithTransaction("[iTMS].[USP_UPDATE_FROM_MISC_TO_VALID_PARTY_IN_MR]", CommandType.StoredProcedure, out command, connection, transactionScope, param2);
                        string error_3 = (string)command.Parameters["@ERRORSTR"].Value;
                        errorMsg = error_3;
                    }

                    if (errorMsg == "")
                    {
                        transactionScope.Commit();
                    }
                    else
                    {
                        transactionScope.Rollback();
                    }
                }
                catch (Exception)
                {
                    errorMsg = "Error: Exception occured.";
                    transactionScope.Rollback();
                }
                finally
                {
                    connection.Close();
                }
            }
            return errorMsg;
        }

        #region Request Miscellaneous Party

        public string[] INSERT_TBL_MR_REQ_MISC_PRTY(TBL_MR_REQ_MISC_PRTY objTBL_MR_REQ_MISC_PRTY)
        {
            string[] result = new string[2];
            string errorMsg = "";

            using (var connection = new SqlConnection(sqlConnection.GetConnectionString()))
            {
                connection.Open();
                SqlCommand command;
                SqlTransaction transactionScope = null;
                transactionScope = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    SqlParameter[] param =
                    {
                      
                    new SqlParameter("@RQMSPY_ID", SqlDbType.Int),
                    new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200),
                    new SqlParameter("@RQMSPY_MR_TYPE", (objTBL_MR_REQ_MISC_PRTY.RQMSPY_MR_TYPE == null) ? (object)DBNull.Value : objTBL_MR_REQ_MISC_PRTY.RQMSPY_MR_TYPE),
                    new SqlParameter("@RQMSPY_MR_BR_ID", (objTBL_MR_REQ_MISC_PRTY.RQMSPY_MR_BR_ID == null) ? (object)DBNull.Value : objTBL_MR_REQ_MISC_PRTY.RQMSPY_MR_BR_ID),
                    new SqlParameter("@RQMSPY_MR_NO", (objTBL_MR_REQ_MISC_PRTY.RQMSPY_MR_NO == null) ? (object)DBNull.Value : objTBL_MR_REQ_MISC_PRTY.RQMSPY_MR_NO),
                    new SqlParameter("@RQMSPY_REMARKS", (objTBL_MR_REQ_MISC_PRTY.RQMSPY_REMARKS == null) ? (object)DBNull.Value : objTBL_MR_REQ_MISC_PRTY.RQMSPY_REMARKS),
                    new SqlParameter("@RQMSPY_ADD_BY_TYPE", objTBL_MR_REQ_MISC_PRTY.RQMSPY_ADD_BY_TYPE),
                    new SqlParameter("@RQMSPY_ADD_BY", objTBL_MR_REQ_MISC_PRTY.RQMSPY_ADD_BY)                 
                    };

                    param[0].Direction = ParameterDirection.Output;
                    param[1].Direction = ParameterDirection.Output;

                    new DataAccess().InsertWithTransaction("[iTMS].[USP_INSERT_TBL_MR_REQ_MISC_PRTY]", CommandType.StoredProcedure, out command, connection, transactionScope, param);
                    int intRQMSPY_ID = (int)command.Parameters["@RQMSPY_ID"].Value;
                    string error_1 = (string)command.Parameters["@ERRORSTR"].Value;
                    if (intRQMSPY_ID == -1) { errorMsg = error_1; }
                    objTBL_MR_REQ_MISC_PRTY.RQMSPY_ID = intRQMSPY_ID;


                    result[0] = errorMsg;
                    result[1] = intRQMSPY_ID.ToString();

                    if (errorMsg == "")
                    {
                        transactionScope.Commit();
                    }
                    else
                    {
                        transactionScope.Rollback();
                    }
                }
                catch (Exception)
                {
                    errorMsg = "Error: Exception occured.";
                    transactionScope.Rollback();
                }
                finally
                {
                    connection.Close();
                }
            }
            return result;
        }

        #endregion

        #region Request Miscellaneous Party Approval

        public List<MR_REQ_MISC_PRTY_LIST> SELECT_TBL_MR_REQ_MISC_PRTY_LIST(TBL_MR_REQ_MISC_PRTY objTBL_MR_REQ_MISC_PRTY)
        {
            SqlParameter[] param = {
                                       new SqlParameter("@RQMSPY_MR_BR_ID", objTBL_MR_REQ_MISC_PRTY.RQMSPY_MR_BR_ID),
                                       new SqlParameter("@RQMSPY_MR_NO", objTBL_MR_REQ_MISC_PRTY.RQMSPY_MR_NO),
                                       new SqlParameter("@RQMSPY_APPRV_STATUS", objTBL_MR_REQ_MISC_PRTY.RQMSPY_APPRV_STATUS)                                     
                                   };

            DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iTMS].[USP_SELECT_TBL_MR_REQ_MISC_PRTY_LIST]", CommandType.StoredProcedure, param);

            List<MR_REQ_MISC_PRTY_LIST> _list = new List<MR_REQ_MISC_PRTY_LIST>();
            DataTable dt = ds.Tables[0];
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    _list.Add(new MR_REQ_MISC_PRTY_LIST
                    {
                        RQMSPY_ID = Convert.ToInt32(row["RQMSPY_ID"]),
                        RQMSPY_MR_TYPE_NM = Convert.ToString(row["RQMSPY_MR_TYPE_NM"]),
                        RQMSPY_MR_BR_NM = Convert.ToString(row["RQMSPY_MR_BR_NM"]),
                        RQMSPY_MR_NO = Convert.ToString(row["RQMSPY_MR_NO"]),
                        RQMSPY_REMARKS = Convert.ToString(row["RQMSPY_REMARKS"]),
                        RQMSPY_APPRV_STATUS_NM = Convert.ToString(row["RQMSPY_APPRV_STATUS_NM"]),
                    });
                }
            }

            return _list;
        }

        public string UPDATE_APPROVE_REJECT_MR_REQ_MISC_PRTY(TBL_MR_REQ_MISC_PRTY objTBL_MR_REQ_MISC_PRTY)
        {

            SqlParameter[] parameters = new SqlParameter[5];

            parameters[0] = new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200);
            parameters[0].Direction = ParameterDirection.Output;

            parameters[1] = new SqlParameter("@RQMSPY_ID", objTBL_MR_REQ_MISC_PRTY.RQMSPY_ID);
            parameters[2] = new SqlParameter("@RQMSPY_APPRV_REMARKS", (objTBL_MR_REQ_MISC_PRTY.RQMSPY_APPRV_REMARKS == null) ? (object)DBNull.Value : objTBL_MR_REQ_MISC_PRTY.RQMSPY_APPRV_REMARKS);
            parameters[3] = new SqlParameter("@RQMSPY_APPRV_STATUS", objTBL_MR_REQ_MISC_PRTY.RQMSPY_APPRV_STATUS);
            parameters[4] = new SqlParameter("@RQMSPY_APPRV_ADD_BY", objTBL_MR_REQ_MISC_PRTY.RQMSPY_APPRV_ADD_BY);

            string _trERRORSTR = "";
            SqlCommand command;
            new DataAccess(sqlConnection.GetConnectionString()).Insert("[iTMS].[USP_UPDATE_APPROVE_REJECT_MR_REQ_MISC_PRTY]", CommandType.StoredProcedure, out command, parameters);
            _trERRORSTR = command.Parameters["@ERRORSTR"].Value.ToString();
            return _trERRORSTR;
        }

        #endregion

        #region Cash Pay MR Pay Mode Change

        public MR_PAY_MODE_CHNG GET_MR_DTL_FOR_PAYMODE_CHANGE(int MR_BR_ID, string MR_NO)
        {
            SqlParameter[] param = { new SqlParameter("@MR_BR_ID", MR_BR_ID), new SqlParameter("@MR_NO", MR_NO) };
            DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iTMS].[USP_GET_MR_DTL_FOR_PAYMODE_CHANGE]", CommandType.StoredProcedure, param);

            MR_PAY_MODE_CHNG objMR_PAY_MODE_CHNG = new MR_PAY_MODE_CHNG();

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    objMR_PAY_MODE_CHNG.MR_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["MR_ID"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["MR_ID"]);
                    objMR_PAY_MODE_CHNG.MR_NO = Convert.ToString(ds.Tables[0].Rows[0]["MR_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_NO"]);
                    objMR_PAY_MODE_CHNG.MR_DATE = Convert.ToString(ds.Tables[0].Rows[0]["MR_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_DATE"]);
                    objMR_PAY_MODE_CHNG.MR_CBS_DATE = Convert.ToString(ds.Tables[0].Rows[0]["CBS_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CBS_DATE"]);
                    objMR_PAY_MODE_CHNG.MR_STMT_NO = Convert.ToString(ds.Tables[0].Rows[0]["MR_STMT_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["MR_STMT_NO"]);
                    objMR_PAY_MODE_CHNG.MR_AMOUNT = Convert.ToString(ds.Tables[0].Rows[0]["MR_AMT"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["MR_AMT"]);
                    objMR_PAY_MODE_CHNG.CN_NO = Convert.ToString(ds.Tables[0].Rows[0]["CN_NO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CN_NO"]);
                    objMR_PAY_MODE_CHNG.CN_DATE = Convert.ToString(ds.Tables[0].Rows[0]["CN_DATE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["CN_DATE"]);
                    objMR_PAY_MODE_CHNG.MR_PA_ID = Convert.ToDecimal(ds.Tables[0].Rows[0]["PA_ID"] == DBNull.Value ? 0 : ds.Tables[0].Rows[0]["PA_ID"]);
                    objMR_PAY_MODE_CHNG.PARTY_NAME = Convert.ToString(ds.Tables[0].Rows[0]["PARTY_NAME"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["PARTY_NAME"]);
                    objMR_PAY_MODE_CHNG.PARTY_CODE = Convert.ToString(ds.Tables[0].Rows[0]["PARTY_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["PARTY_CODE"]);
                    objMR_PAY_MODE_CHNG.ERRORSTR = Convert.ToString(ds.Tables[0].Rows[0]["ERRORSTR"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["ERRORSTR"]);
                }
            }

            return objMR_PAY_MODE_CHNG;
        }

        public string UPDATE_MR_DTL_FOR_PAYMODE_CHANGE(MR_PAY_MODE_CHNG _objMR_PAY_MODE_CHNG)
        {

            //SqlParameter[] parameters = new SqlParameter[12];

            //parameters[0] =   new SqlParameter("@MR_NO", _objMR_PAY_MODE_CHNG.MR_NO == null ? (object)DBNull.Value : _objMR_PAY_MODE_CHNG.MR_NO);
            //parameters[1] =   new SqlParameter("@MR_ID", _objMR_PAY_MODE_CHNG.MR_ID == null ? (object)DBNull.Value : _objMR_PAY_MODE_CHNG.MR_ID);
            //parameters[2] =   new SqlParameter("@PAY_MODE", _objMR_PAY_MODE_CHNG.MR_PAY_MODE == null ? (object)DBNull.Value : _objMR_PAY_MODE_CHNG.MR_PAY_MODE);
            //parameters[3] =   new SqlParameter("@CHQ_ID", _objMR_PAY_MODE_CHNG.CHQ_ID == null ? (object)DBNull.Value : _objMR_PAY_MODE_CHNG.CHQ_ID);
            //parameters[4] =   new SqlParameter("@CHQ_RTGS_DD_NO", _objMR_PAY_MODE_CHNG.CHQ_RTGS_DD_NO == null ? (object)DBNull.Value : _objMR_PAY_MODE_CHNG.CHQ_RTGS_DD_NO);
            //parameters[5] =   new SqlParameter("@CHQ_RTGS_DD_DATE", _objMR_PAY_MODE_CHNG.CHQ_RTGS_DD_DATE1 == null ? (object)DBNull.Value : _objMR_PAY_MODE_CHNG.CHQ_RTGS_DD_DATE1);
            //parameters[6] =   new SqlParameter("@CHQ_RTGS_DD_BANK", _objMR_PAY_MODE_CHNG.CHQ_RTGS_DD_BANK == null ? (object)DBNull.Value : _objMR_PAY_MODE_CHNG.CHQ_RTGS_DD_BANK);
            //parameters[7] =   new SqlParameter("@CHQ_RTGS_DD_BANK_NAME", _objMR_PAY_MODE_CHNG.CHQ_RTGS_DD_BANK_NAME == null ? (object)DBNull.Value : _objMR_PAY_MODE_CHNG.CHQ_RTGS_DD_BANK_NAME);
            //parameters[8] =   new SqlParameter("@CHQ_RTGS_DD_AMOUNT", _objMR_PAY_MODE_CHNG.CHQ_RTGS_DD_AMT == null ? (object)DBNull.Value : _objMR_PAY_MODE_CHNG.CHQ_RTGS_DD_AMT);
            //parameters[9] =   new SqlParameter("@MODIFIED_BY", _objMR_PAY_MODE_CHNG.MODIFIED_BY);
            //parameters[10] =  new SqlParameter("@MODIFIED_TYPE", _objMR_PAY_MODE_CHNG.MODIFIED_TYPE);

            //parameters[11] = new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200);
            //parameters[11].Direction = ParameterDirection.Output;


            //string _tr_ERRORSTR = "";
            //SqlCommand command;
            //new DataAccess(sqlConnection.GetConnectionString()).Insert("[iTMS].[USP_UPDATE_MR_DTL_FOR_PAYMODE_CHANGE]", CommandType.StoredProcedure, out command, parameters);
            //_tr_ERRORSTR = command.Parameters["@ERRORSTR"].Value.ToString();
            //return _tr_ERRORSTR;

            string errorMsg = "";
            using (var connection = new SqlConnection(sqlConnection.GetConnectionString()))
            {
                connection.Open();
                SqlCommand command;
                SqlTransaction transactionScope = null;
                transactionScope = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    SqlParameter[] param =
                    {
                         new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                        ,new SqlParameter("@MR_NO", _objMR_PAY_MODE_CHNG.MR_NO == null ? (object)DBNull.Value : _objMR_PAY_MODE_CHNG.MR_NO)
                        ,new SqlParameter("@MR_ID", _objMR_PAY_MODE_CHNG.MR_ID == null ? (object)DBNull.Value : _objMR_PAY_MODE_CHNG.MR_ID)
                        ,new SqlParameter("@PAY_MODE", _objMR_PAY_MODE_CHNG.MR_PAY_MODE == null ? (object)DBNull.Value : _objMR_PAY_MODE_CHNG.MR_PAY_MODE)
                        ,new SqlParameter("@CHQ_ID", _objMR_PAY_MODE_CHNG.CHQ_ID == null ? (object)DBNull.Value : _objMR_PAY_MODE_CHNG.CHQ_ID)
                        ,new SqlParameter("@CHQ_RTGS_DD_NO", _objMR_PAY_MODE_CHNG.CHQ_RTGS_DD_NO == null ? (object)DBNull.Value : _objMR_PAY_MODE_CHNG.CHQ_RTGS_DD_NO)
                        ,new SqlParameter("@CHQ_RTGS_DD_DATE", _objMR_PAY_MODE_CHNG.CHQ_RTGS_DD_DATE1 == null ? (object)DBNull.Value : _objMR_PAY_MODE_CHNG.CHQ_RTGS_DD_DATE1)
                        ,new SqlParameter("@CHQ_RTGS_DD_BANK", _objMR_PAY_MODE_CHNG.CHQ_RTGS_DD_BANK == null ? (object)DBNull.Value : _objMR_PAY_MODE_CHNG.CHQ_RTGS_DD_BANK)
                        ,new SqlParameter("@CHQ_RTGS_DD_BANK_NAME", _objMR_PAY_MODE_CHNG.CHQ_RTGS_DD_BANK_NAME == null ? (object)DBNull.Value : _objMR_PAY_MODE_CHNG.CHQ_RTGS_DD_BANK_NAME)
                        ,new SqlParameter("@CHQ_RTGS_DD_AMOUNT", _objMR_PAY_MODE_CHNG.CHQ_RTGS_DD_AMT == null ? (object)DBNull.Value : _objMR_PAY_MODE_CHNG.CHQ_RTGS_DD_AMT)
                        ,new SqlParameter("@MODIFIED_BY", _objMR_PAY_MODE_CHNG.MODIFIED_BY)
                        ,new SqlParameter("@MODIFIED_TYPE", _objMR_PAY_MODE_CHNG.MODIFIED_TYPE) 
                    };

                    param[0].Direction = ParameterDirection.Output;
                    new DataAccess().InsertWithTransaction("[iTMS].[USP_UPDATE_MR_DTL_FOR_PAYMODE_CHANGE]", CommandType.StoredProcedure, out command, connection, transactionScope, param);
                    string error_1 = (string)command.Parameters["@ERRORSTR"].Value;
                    errorMsg = error_1;

                    if (errorMsg == "")
                    {
                        transactionScope.Commit();
                    }
                    else
                    {
                        transactionScope.Rollback();
                    }
                }
                catch (Exception)
                {
                    errorMsg = "Error: Exception occured.";
                    transactionScope.Rollback();
                }
                finally
                {
                    connection.Close();
                }
            }

            return errorMsg;
        }

        #endregion

        //Added By : Pramesh Kumar Vishwakarma,Date:23/09/2022
        public object GET_FLEET_DTL_BY_CODE(string MR_FLEET_CODE, int BR_ID)
        {
            SqlParameter[] param = { new SqlParameter("@FLEET_CODE", MR_FLEET_CODE), new SqlParameter("@BR_ID", BR_ID) };
            DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iTMS].[USP_GET_FLEET_DTL_FOR_MR_BY_FL_CODE]", CommandType.StoredProcedure, param);
            string FLEET_CODE = "";
            int FLEET_ID = 0;
            string VEH_NO = "";
            string ERR_MSG = "";
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    FLEET_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["FL_ID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["FL_ID"]);
                    FLEET_CODE = Convert.ToString(ds.Tables[0].Rows[0]["FL_CODE"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["FL_CODE"]);
                    VEH_NO = Convert.ToString(ds.Tables[0].Rows[0]["FL_VEHNO"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["FL_VEHNO"]);
                    ERR_MSG = Convert.ToString(ds.Tables[0].Rows[0]["ERR_MSG"] == DBNull.Value ? "" : ds.Tables[0].Rows[0]["ERR_MSG"]);
                }
            }
            return new { FLEET_CODE, FLEET_ID, VEH_NO, ERR_MSG };

        }

        //Added by Pramesh kumar Vishwakarma, Date:12-01-2023
        public string CHECK_MR_NO_FOR_MISC_PARTY(int brId, int mrType, string mrNo)
        {
            SqlParameter[] param = { new SqlParameter("@BR_ID", brId), new SqlParameter("@MR_TYPE_ID", mrType), new SqlParameter("@MR_NO", mrNo) };
            string msg = "";
            using (DataTable dt = new DataAccess(sqlConnection.GetConnectionString()).GetDataTable("[iTMS].[USP_CHECK_MR_NO_FOR_MISC_PARTY]", CommandType.StoredProcedure, param))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    msg = Convert.ToString(dt.Rows[0]["MSG_ERR"]);
                }
            }
            return msg;
        }


        #region On Account MR Freight Refund.

        public MR_OR_ADV_DTLS 
            SELECT_ONACCOUNT_MR_DATA(string mrNo, int brId)
        {
            SqlParameter[] param = {
                                      
                                       new SqlParameter("@MR_NO", mrNo),
                                       new SqlParameter("@BR_ID", brId),
                                      
                                   };

            DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iTMS].[USP_GET_ONACC_MR_DTLS]", CommandType.StoredProcedure, param);

            MR_OR_ADV_DTLS data = new MR_OR_ADV_DTLS();
            DataTable dt = ds.Tables[0];
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["MR_ADV_DATE"] != DBNull.Value)
                    {
                        data.MR_ADV_DATE = Convert.ToDateTime(row["MR_ADV_DATE"]);
                    }

                    data.MR_ADV_ID = Convert.ToDecimal(row["MR_ADV_ID"] == DBNull.Value ? "0" : row["MR_ADV_ID"]);
                    data.MR_ADV_NO = Convert.ToString(row["MR_ADV_NO"] == DBNull.Value ? "" : row["MR_ADV_NO"]);
                    data.MR_ADV_BR_NAME = Convert.ToString(row["MR_ADV_BR_NAME"] == DBNull.Value ? "" : row["MR_ADV_BR_NAME"]);
                    data.MR_ADV_BR_ID = Convert.ToInt32(row["MR_ADV_BR_ID"] == DBNull.Value ? "0" : row["MR_ADV_BR_ID"]);

                    data.MR_ADV_DATE1 = Convert.ToString(row["MR_ADV_DATE1"] == DBNull.Value ? "" : row["MR_ADV_DATE1"]);

                    data.MR_ADV_P_CODE = Convert.ToString(row["MR_ADV_P_CODE"] == DBNull.Value ? "" : row["MR_ADV_P_CODE"]);
                    data.MR_ADV_P_NAME = Convert.ToString(row["MR_ADV_P_NAM"] == DBNull.Value ? "" : row["MR_ADV_P_NAM"]);
                    data.MR_ADV_P_ID = Convert.ToDecimal(row["MR_ADV_P_ID"] == DBNull.Value ? "0" : row["MR_ADV_P_ID"]);
                    data.MR_ADV_PA_ID = Convert.ToDecimal(row["MR_ADV_PA_ID"] == DBNull.Value ? "0" : row["MR_ADV_PA_ID"]);
                    data.MR_ADV_BAL_AMT = Convert.ToDecimal(row["MR_ADV_BAL_AMT"] == DBNull.Value ? "0" : row["MR_ADV_BAL_AMT"]);
                    data.CRAS_STATUS = Convert.ToString(row["CRAS_STATUS"] == DBNull.Value ? "0" : row["CRAS_STATUS"]);
                    data.MSG = Convert.ToString(row["MSG"] == DBNull.Value ? "" : row["MSG"]);


                    data.MR_TOTAL_AMT = Convert.ToDecimal(row["MR_TOTAL_AMT"] == DBNull.Value ? "0" : row["MR_TOTAL_AMT"]);

                    data.CHQ_NO = Convert.ToString(row["CHQ_NO"] == DBNull.Value ? "" : row["CHQ_NO"]);
                    data.CHQ_DATE = Convert.ToString(row["CHQ_DATE"] == DBNull.Value ? "" : row["CHQ_DATE"]);
                    data.MR_PAYMODE = Convert.ToString(row["MR_PAYMODE"] == DBNull.Value ? "" : row["MR_PAYMODE"]);
                    data.MR_PAY_MODE_NAME = Convert.ToString(row["MR_PAY_MODE_NAME"] == DBNull.Value ? "" : row["MR_PAY_MODE_NAME"]);

                }
            }

            return data;
        }


        public string INSERT_TBL_MR_FRT_REFUND_REQ(OnAcc_MR_FRT_Refund_Req objMR_Ref_Req)
        {
            //string[] result = new string[2];

            decimal requestId = 0;
            string errorMsg = "";

            using (var connection = new SqlConnection(sqlConnection.GetConnectionString()))
            {
                connection.Open();
                SqlCommand command;
                SqlTransaction transactionScope = null;
                transactionScope = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    SqlParameter[] param =
                    {
                      
                    new SqlParameter("@RF_ID", SqlDbType.Decimal),
                    new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200),
                    new SqlParameter("@RF_MR_ID", (objMR_Ref_Req.MR_ID == null) ? (object)DBNull.Value : objMR_Ref_Req.MR_ID),
                    new SqlParameter("@RF_MR_BR_ID", (objMR_Ref_Req.RR_MR_BR_ID == null) ? (object)DBNull.Value : objMR_Ref_Req.RR_MR_BR_ID),
                    new SqlParameter("@RF_MR_NO", (objMR_Ref_Req.MR_NO == null) ? (object)DBNull.Value : objMR_Ref_Req.MR_NO),
                    new SqlParameter("@RF_MR_TRANSFER_AMT", (objMR_Ref_Req.MR_TRANSFER_AMT == null) ? (object)DBNull.Value : objMR_Ref_Req.MR_TRANSFER_AMT),
                    new SqlParameter("@RF_REMARKS",(objMR_Ref_Req.REMARKS == null) ? (object)DBNull.Value : objMR_Ref_Req.REMARKS),
                    new SqlParameter("@RF_ADDBY", objMR_Ref_Req.MR_REF_REQ_ADDBY)  ,
                     new SqlParameter("@RF_ADDBY_TYPE", objMR_Ref_Req.MR_REF_REQ_ADDBY_TYPE)  
                    };

                    param[0].Direction = ParameterDirection.Output;
                    param[1].Direction = ParameterDirection.Output;

                    new DataAccess().InsertWithTransaction("[iFMS].[USP_INSERT_MR_FRT_REFUND_REQ]", CommandType.StoredProcedure, out command, connection, transactionScope, param);
                    requestId = (decimal)command.Parameters["@RF_ID"].Value;
                    string error_1 = (string)command.Parameters["@ERRORSTR"].Value;
                    if (requestId == -1) { errorMsg = error_1; }
                    objMR_Ref_Req.MR_REF_REQ_ID = requestId;

                    if (errorMsg == "")
                    {
                        transactionScope.Commit();
                    }
                    else
                    {
                        transactionScope.Rollback();
                    }
                }
                catch (Exception)
                {
                    errorMsg = "Error: Exception occured.";
                    transactionScope.Rollback();
                }
                finally
                {
                    connection.Close();
                }
            }
            return errorMsg;
        }



        public List<MR_FRT_REF_LIST> SELECT_MR_FRT_REF_LIST(OnAcc_MR_FRT_Refund_Req objRR)
        {
            SqlParameter[] param = {
                                       new SqlParameter("@MR_BR_ID", objRR.RR_MR_BR_ID),
                                       new SqlParameter("@MR_NO", objRR.RR_MR_NO),
                                       new SqlParameter("@APPRV_STATUS", objRR.APPRV_STATUS_FILTER)                                     
                                   };

            DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iFMS].[USP_SELECT_MR_FRT_REFUND_LIST]", CommandType.StoredProcedure, param);

            List<MR_FRT_REF_LIST> _list = new List<MR_FRT_REF_LIST>();
            DataTable dt = ds.Tables[0];
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    _list.Add(new MR_FRT_REF_LIST
                    {
                        RF_ID = Convert.ToDecimal(row["RF_ID"]),
                        RF_MR_ID = Convert.ToDecimal(row["RF_MR_ID"]),
                        RF_MR_BR_NAME = Convert.ToString(row["MR_BRANCH"]),
                        RF_MR_NO = Convert.ToString(row["RF_MR_NO"]),
                        RF_DATE = Convert.ToString(row["RF_ADDON"]),
                        RF_MR_TRANSFER_AMT = Convert.ToDecimal(row["RF_MR_TRANSFER_AMT"]),
                        RF_REMARKS = Convert.ToString(row["RF_REMARKS"]),
                        RF_STATUS = Convert.ToString(row["RF_APPRV_STATUS_NAME"]),
                    });
                }
            }

            return _list;
        }


        public string UPDATE_APPROVE_REJECT_MR_FRT_REFUND_REQ_STATUS(OnAcc_MR_FRT_Refund_Req objMR_FRT_Refund)
        {

            SqlParameter[] parameters = new SqlParameter[6];

            parameters[0] = new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200);
            parameters[0].Direction = ParameterDirection.Output;

            parameters[1] = new SqlParameter("@RF_ID", (objMR_FRT_Refund.MR_REF_REQ_ID == null) ? (object)DBNull.Value : objMR_FRT_Refund.MR_REF_REQ_ID);
            parameters[2] = new SqlParameter("@RF_APPRV_STATUS", (objMR_FRT_Refund.APPRV_STATUS == null) ? (object)DBNull.Value : objMR_FRT_Refund.APPRV_STATUS);
            parameters[3] = new SqlParameter("@RF_APPRV_ADDBY", (objMR_FRT_Refund.MR_REF_REQ_ADDBY == null) ? (object)DBNull.Value : objMR_FRT_Refund.MR_REF_REQ_ADDBY);
            parameters[4] = new SqlParameter("@RF_APPRV_ADDBY_TYPE", (objMR_FRT_Refund.MR_REF_REQ_ADDBY_TYPE == null) ? (object)DBNull.Value : objMR_FRT_Refund.MR_REF_REQ_ADDBY_TYPE);
            parameters[5] = new SqlParameter("@RF_APPRV_REMARKS", (objMR_FRT_Refund.APPRV_REMARKS == null) ? (object)DBNull.Value : objMR_FRT_Refund.APPRV_REMARKS);

            string _trERRORSTR = "";
            SqlCommand command;
            new DataAccess(sqlConnection.GetConnectionString()).Insert("iFMS.UPDATE_MR_FRT_REFUND_REQ_STATUS", CommandType.StoredProcedure, out command, parameters);
            _trERRORSTR = command.Parameters["@ERRORSTR"].Value.ToString();
            return _trERRORSTR;
        }


        #endregion
    

        #region OnAcc MR Party Change

        public OnAcc_MR_Party_Change SELECT_OnAcc_MR_Party_Change(string mrNo, int brId) 
        {
            SqlParameter[] param = {
                                      
                                       new SqlParameter("@MR_NO", mrNo),
                                       new SqlParameter("@BR_ID", brId),
                                      
                                   };

            DataSet ds = new DataAccess(sqlConnection.GetConnectionString()).GetDataSet("[iTMS].[USP_GET_ONACC_MR_DTLS_FOR_PARTY_CHANGE]", CommandType.StoredProcedure, param);

            OnAcc_MR_Party_Change data = new OnAcc_MR_Party_Change();
            DataTable dt = ds.Tables[0];
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    //if (row["MR_ADV_DATE"] != DBNull.Value)
                    //{
                    //    data.MR_ADV_DATE = Convert.ToDateTime(row["MR_ADV_DATE"]);
                    //}

                    data.MR_ID = Convert.ToDecimal(row["MR_ADV_ID"] == DBNull.Value ? "0" : row["MR_ADV_ID"]);
                    data.MR_NO = Convert.ToString(row["MR_ADV_NO"] == DBNull.Value ? "" : row["MR_ADV_NO"]);
                    data.MR_ADV_BR_NAME = Convert.ToString(row["MR_ADV_BR_NAME"] == DBNull.Value ? "" : row["MR_ADV_BR_NAME"]);
                    data.MR_ADV_BR_ID = Convert.ToInt32(row["MR_ADV_BR_ID"] == DBNull.Value ? "0" : row["MR_ADV_BR_ID"]);

                    data.MR_ADV_DATE1 = Convert.ToString(row["MR_ADV_DATE1"] == DBNull.Value ? "" : row["MR_ADV_DATE1"]);

                    data.MR_ADV_P_CODE = Convert.ToString(row["MR_ADV_P_CODE"] == DBNull.Value ? "" : row["MR_ADV_P_CODE"]);
                    data.MR_ADV_P_NAME = Convert.ToString(row["MR_ADV_P_NAM"] == DBNull.Value ? "" : row["MR_ADV_P_NAM"]);
                    data.MR_ADV_P_ID = Convert.ToDecimal(row["MR_ADV_P_ID"] == DBNull.Value ? "0" : row["MR_ADV_P_ID"]);
                    data.MR_ADV_PA_ID = Convert.ToDecimal(row["MR_ADV_PA_ID"] == DBNull.Value ? "0" : row["MR_ADV_PA_ID"]);
                    //data.MR_ADV_BAL_AMT = Convert.ToDecimal(row["MR_ADV_BAL_AMT"] == DBNull.Value ? "0" : row["MR_ADV_BAL_AMT"]);
                    //data.CRAS_STATUS = Convert.ToString(row["CRAS_STATUS"] == DBNull.Value ? "0" : row["CRAS_STATUS"]);
                    data.MSG = Convert.ToString(row["MSG"] == DBNull.Value ? "" : row["MSG"]);
         

                    data.MR_TOTAL_AMT = Convert.ToDecimal(row["MR_TOTAL_AMT"] == DBNull.Value ? "0" : row["MR_TOTAL_AMT"]);

                    //data.CHQ_NO = Convert.ToString(row["CHQ_NO"] == DBNull.Value ? "" : row["CHQ_NO"]);
                    //data.CHQ_DATE = Convert.ToString(row["CHQ_DATE"] == DBNull.Value ? "" : row["CHQ_DATE"]);
                    //data.MR_PAYMODE = Convert.ToString(row["MR_PAYMODE"] == DBNull.Value ? "" : row["MR_PAYMODE"]);
                    //data.MR_PAY_MODE_NAME = Convert.ToString(row["MR_PAY_MODE_NAME"] == DBNull.Value ? "" : row["MR_PAY_MODE_NAME"]);

                    data.P_NAME = Convert.ToString(row["P_NAME"] == DBNull.Value ? "" : row["P_NAME"]);
                    data.P_CODE = Convert.ToString(row["P_CODE"] == DBNull.Value ? "" : row["P_CODE"]);
                    data.MR_CBS_DATE = Convert.ToString(row["MR_CBS_DATE"] == DBNull.Value ? "" : row["MR_CBS_DATE"]);
                    data.PA_GST_NO = Convert.ToString(row["PA_GST_NO"] == DBNull.Value ? "" : row["PA_GST_NO"]);
                    data.MR_TYPE = Convert.ToInt32(row["MR_TYPE"] == DBNull.Value ? "0" : row["MR_TYPE"]);
                

                }
            }
        

            return data;
        
    }

        public string INSERT_TBL_MR_ONACC_PARTY_CHANGE(OnAcc_MR_Party_Change objMR_PARTY_CHNAGE)
        {


            decimal requestId = 0;
            string errorMsg = "";

            using (var connection = new SqlConnection(sqlConnection.GetConnectionString()))
            {
                connection.Open();
                SqlCommand command;
                SqlTransaction transactionScope = null;
                transactionScope = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    SqlParameter[] param =
                    {
                      
                    new SqlParameter("@MRPC_ID", SqlDbType.Decimal),
                    new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200),
                    new SqlParameter("@MRPC_MR_ID", (objMR_PARTY_CHNAGE.MR_ID == null) ? (object)DBNull.Value : objMR_PARTY_CHNAGE.MR_ID),
                    new SqlParameter("@MRPC_MR_BR_ID  ", (objMR_PARTY_CHNAGE.RR_MR_BR_ID   == null) ? (object)DBNull.Value : objMR_PARTY_CHNAGE.RR_MR_BR_ID),
                    new SqlParameter("@MRPC_MR_NO", (objMR_PARTY_CHNAGE.MR_NO == null) ? (object)DBNull.Value : objMR_PARTY_CHNAGE.MR_NO),     
       
                    new SqlParameter("@MRPC_MR_TYPE_ID", (objMR_PARTY_CHNAGE.MR_TYPE == null) ? (object)DBNull.Value : objMR_PARTY_CHNAGE.MR_TYPE),     
                      new SqlParameter("@MRPC_OLD_PA_ID", (objMR_PARTY_CHNAGE.MR_ADV_P_ID == null) ? (object)DBNull.Value : objMR_PARTY_CHNAGE.MR_ADV_P_ID),
                    new SqlParameter("@MRPC_NEW_PA_ID", (objMR_PARTY_CHNAGE.MR_NEW_PA_ID == null) ? (object)DBNull.Value : objMR_PARTY_CHNAGE.MR_NEW_PA_ID),
                    new SqlParameter("@MRPC_NEW_GST_NO ", (objMR_PARTY_CHNAGE.MRPC_NEW_GST_NO   == null) ? (object)DBNull.Value : objMR_PARTY_CHNAGE.MRPC_NEW_GST_NO ),
                    new SqlParameter("@MRPC_REMARKS ",(objMR_PARTY_CHNAGE.REMARKS   == null) ? (object)DBNull.Value : objMR_PARTY_CHNAGE.REMARKS ),
                    new SqlParameter("@MRPC_ADDBY", objMR_PARTY_CHNAGE.MR_ADDBY),
                    new SqlParameter("@MRPC_ADDBY_TYPE", objMR_PARTY_CHNAGE.MR_ADDBY_TYPE)
                    };

                    param[0].Direction = ParameterDirection.Output;
                    param[1].Direction = ParameterDirection.Output;

                    new DataAccess().InsertWithTransaction("[iFMS].[USP_INSERT_MR_ONACC_PARTY_CHANGE]", CommandType.StoredProcedure, out command, connection, transactionScope, param);
                    requestId = (decimal)command.Parameters["@MRPC_ID"].Value;
                    string error_1 = (string)command.Parameters["@ERRORSTR"].Value;
                    if (requestId == -1) { errorMsg = error_1; }
                    objMR_PARTY_CHNAGE.MRPC_ID = requestId;

                    if (errorMsg == "")
                    {

                        SqlParameter[] param1 = {
                                              new SqlParameter("@ERRORSTR", SqlDbType.VarChar, 200)
                                             ,new SqlParameter("@HIS_ID", SqlDbType.Decimal) 
                                             ,new SqlParameter("@HIS_DATE",DateTime.Now.ToString("dd/MM/yyyy") )
                                             ,new SqlParameter("@HIS_MR_ID",objMR_PARTY_CHNAGE.MR_ID )
                                             ,new SqlParameter("@HIS_MR_NO", objMR_PARTY_CHNAGE.MR_NO)
                                             ,new SqlParameter("@HIS_REMARKS", "CBS DATE UPPDATED")
                                             ,new SqlParameter("@HIS_PROCESS_ID", 5)
                                             ,new SqlParameter("@HIS_PROCESS_NAME", "MR PARTY UPDATED")
                                             ,new SqlParameter("@HIS_ADDTYPE", objMR_PARTY_CHNAGE.MR_ADDBY_TYPE)
                                             ,new SqlParameter("@HIS_ADDBY", objMR_PARTY_CHNAGE.MR_ADDBY)
                                            };

                        param1[0].Direction = ParameterDirection.Output;
                        param1[1].Direction = ParameterDirection.Output;
                        new DataAccess().InsertWithTransaction("[iTMS].[INSERT_HIS_MR]", CommandType.StoredProcedure, out command, connection, transactionScope, param1);
                        string error_2 = (string)command.Parameters["@ERRORSTR"].Value;
                        decimal HIS_ID = (decimal)command.Parameters["@HIS_ID"].Value;
                        if (HIS_ID == -1) { errorMsg = error_2; }
                    }


                    if (errorMsg == "")
                    {
                        transactionScope.Commit();
                    }
                    else
                    {
                        transactionScope.Rollback();
                    }
                }
                catch (Exception)
                {
                    errorMsg = "Error: Exception occured.";
                    transactionScope.Rollback();
                }
                finally
                {
                    connection.Close();
                }
            }
            return errorMsg;
        }




    }
}
#endregion