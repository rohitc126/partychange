using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BusinessLayer.Entity
{
    public class OnAcc_MR_FRT_Refund_Req
    {
        public int? RR_MR_BR_ID { get; set; }
        public string RR_MR_BR_NM { get; set; }
 
        //[Required(ErrorMessage = "Enter MR No")]
        public string RR_MR_NO { get; set; }


        public decimal? MR_ID { get; set; }
        public string MR_NO { get; set; }
        public string MR_DATE { get; set; }
        public string MR_P_ID { get; set; }
        public string MR_P_NAME { get; set; }
        public Nullable<decimal> MR_PA_ID { get; set; }

        public string MR_P_CODE { get; set; }

        public string MR_PAY_MODE { get; set; }
        public string MR_PAY_MODE_NAME { get; set; }


        public string MR_CHQ_NO { get; set; }
        public string MR_CHQ_DATE { get; set; }
        public decimal? MR_AMT { get; set; }
        public decimal? MR_BAL_AMT { get; set; }
        public decimal? MR_TRANSFER_AMT { get; set; }
        public string REMARKS { get; set; }

        public Nullable<decimal> MR_REF_REQ_ID { get; set; }
        public Nullable<int> MR_REF_REQ_ADDBY { get; set; }
        public string MR_REF_REQ_ADDBY_TYPE { get; set; }


        public SelectList BR_LIST { get; set; }

        public Nullable<int> APPRV_STATUS { get; set; }
        public string APPRV_REMARKS { get; set; }
        public SelectList APPRV_STATUS_LIST { get; set; }

        public Nullable<int> APPRV_STATUS_FILTER { get; set; }
        public SelectList APPRV_STATUS_FILTER_LIST { get; set; }

        public OnAcc_MR_FRT_Refund_Req()
        {

            APPRV_STATUS_LIST = new SelectList(new List<SelectListItem>
                                    { 
                                        new SelectListItem { Text = "APPROVED", Value = "1"},
                                        new SelectListItem { Text = "REJECT", Value = "2"},
                                    }, "Value", "Text");


            APPRV_STATUS_FILTER_LIST = new SelectList(new List<SelectListItem>
                                    { 
                                        new SelectListItem { Text = "ALL", Value = "0"},                                        
                                        new SelectListItem { Text = "APPROVED", Value = "1"},
                                        new SelectListItem { Text = "REJECTED", Value = "2"},
                                        new SelectListItem { Text = "PENDING", Value = "3"},
                                    }, "Value", "Text");
        }

    }

    public class MR_FRT_REF_LIST
    {
        public Nullable<decimal>   RF_ID{ get; set; }
        public Nullable<decimal>  RF_MR_ID	{ get; set; }
        public string RF_MR_BR_NAME	{ get; set; }
        public string RF_MR_NO	{ get; set; }
        public string RF_DATE { get; set; }
        public Nullable<decimal>  RF_MR_TRANSFER_AMT	{ get; set; }
        public string RF_REMARKS	{ get; set; }
        public string RF_STATUS	{ get; set; }
    }

    public class OnAcc_MR_Party_Change
    { 
        
    public int? RR_MR_BR_ID { get; set; }
    public string RR_MR_NO { get; set; }
   
    public string MR_NO { get; set; }
    public decimal? MR_ID { get; set; }  
    public decimal? MR_ADV_BR_ID { get; set; }
    public string MR_ADV_BR_NAME { get; set; }
    public decimal? MR_TYPE { get; set; }
    public string MR_ADV_DATE1 { get; set; }
    public string MR_ADV_P_CODE { get; set; }
    public string MR_ADV_P_NAME { get; set; }
    public decimal? MR_ADV_P_ID{ get; set; }
    public decimal? MR_ADV_PA_ID { get; set; }   
    public string MSG { get; set; }
    public decimal? MR_TOTAL_AMT { get; set; }   
    public string P_NAME { get; set; }
    public decimal? P_ID { get; set; }
    public decimal? PA_ID { get; set; }
    public string P_CODE { get; set; }
    public string MR_CBS_DATE { get; set; }
    public string PA_GST_NO { get; set; }
    public string MRPC_NEW_GST_NO { get; set; }
    public decimal? MR_NEW_PA_ID { get; set; }
    public Nullable<bool> PA_GST_VALIDATE { get; set; }
    public string REMARKS { get; set; }
    public Nullable<int> MR_ADDBY { get; set; }
    public string MR_ADDBY_TYPE { get; set; }
    public Nullable<decimal> MRPC_ID { get; set; }
    public SelectList BR_LIST { get; set; }

    }
}
