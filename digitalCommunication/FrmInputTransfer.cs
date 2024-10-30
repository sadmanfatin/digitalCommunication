using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace digitalCommunication
{
    public partial class FrmInputTransfer : Form
    {
        private decimal NT = 0, OT = 0, ExOT = 0, TCurrentHOP = 0, StOpHp = 0, stbsRevNo = 0, stbs = 0, ProdDayCount=0;
        private string HourType, TGTPcs,StripID="";
        private int ProdDay = 0;
        private int CheckBackDate=0;


        public FrmInputTransfer()
        {
            InitializeComponent();
        }
        csDataProcess cls = new csDataProcess();
  
        public string GetMaxInputReqNo()
        {
            string maxchallan = "";
//JAL-1
//JAL-2
//JAL-3
//FFL-1
//FFL-2
//FFL-3
//JFL-1
//JFL-2
//JKL-1
//JKL-2
//JKL-3
//JKL-4
//JKL-5
//DBL
//JKL-U2-1
//JKL-U2-2
//JKL-U2-3
//JKL-U2-4
//JKL-U2-5
//MFL-Old
//MFL-1
//MFL-2
//MFL-3
//MFL-4
//FFL2-1
//FFL2-2
//FFL2-3
//FFL2-4
//FFL2-5

            string sql = @"Select max(Req)+1 Req, MaxNo From  ( 
 SELECT  isnull( max(cast(substring(fldReqNo,charindex('/',fldReqNo)+1,len(fldReqNo))as bigint)),0) Req ,getdate() MaxNo
FROM  tblSewing_CuttingInput_Sizewise Where fldID> 1137911 And  month(fldInputDate)=month(getdate()) 
AND Year(fldInputDate)=Year(getdate()))A Group by MaxNo";
            
            DataTable dt = cls.getDataTable(sql);

            if (dt.Rows[0][0].ToString() == "1")
            {

                DateTime yy = Convert.ToDateTime(dt.Rows[0][1].ToString());
                string dd = yy.Month.ToString();
                string y = yy.Year.ToString();
                string year = y.Substring(2);
                string Mont = dd.PadLeft(2, '0');
                string frast = year + Mont + "00001";
                maxchallan = frast;

            }
            else
            {
                maxchallan = dt.Rows[0][0].ToString();
            }
            return maxchallan;
        }
        private void btnInputTrnsFer_Click(object sender, EventArgs e)
        {
            string Challan="", ChallanNo="",checkDocNo="",checkCut="",PreDocNo="",unitSearch;
            DataTable CUT_DT = null, unitSearch_DT = null;

            string SQLCheckBack = @"Select dateAdd(dd," + CheckBackDate + @",Cast(Getdate() as date))SysDate,'" + dtpIputdate.Text + @"' ProdDate
Where dateAdd(dd," + CheckBackDate + @",Cast(Getdate() as date))<='" + dtpIputdate.Text + @"'";
            DataTable DtBack = cls.getDataTable(SQLCheckBack);
            if (DtBack.Rows.Count == 0)
            {
                MessageBox.Show("This date data are not allow to transfer", "Error");
                return;
            }

            string SearchKey = "";
            if (cmbUnit.Text == "")
            {
                return;
            }
            if (txtTrackingNo.Text == "")
            {
                SearchKey = " unit = '" + cmbUnit.Text + @"' ";
            }
            if (cmbUnit.Text != "" && txtTrackingNo.Text != "")
            {
                SearchKey = " unit = '" + cmbUnit.Text + @"' And TRACKING_NO='" + txtTrackingNo.Text + @"' ";
            }



            string strOrcl = @" Select CUTISSUE_NO,TO_CHAR(Input_Date,'MM/DD/YYYY')Input_Date,unit,Line,Cut_No,MiS_CutNo,Size_Value,T_INPUT,TRACKING_NO From (Select Cut_Issue_H.CUTISSUE_NO,TRUNC (Cut_Issue_H.CUTISSUE_DT)Input_Date,Cut_Issue_H.DIVISION_ID,ISSUE_TO_ORG,TRACKING_NO,HDR_BUYERNAME,COLOR,
LINE_NO,
reverse(substr(reverse(LINE_NO),INSTR(reverse(LINE_NO),'-',1,1)+1,length(LINE_NO))) unit, 
reverse(substr(reverse(LINE_NO),0,INSTR(reverse(LINE_NO),'-',1,1)-1)) Line, 
reverse(substr(reverse(Cut_Issue_L.ATTRIBUTE1),0,INSTR(reverse(Cut_Issue_L.ATTRIBUTE1 ),'/',1,1)-1)) MiS_CutNo, 
Cut_Issue_L.ATTRIBUTE1 Cut_No,
 Cut_Issue_H.RATION_NO,
 Cut_Issue_SZL.SIZ_BND_ID,
 substr(Cut_Issue_SZL.SIZ_BND_ID,0,length(Cut_Issue_SZL.SIZ_BND_ID)-1)Size_Value,
floor(Sum(Cut_Issue_SZL.ACCEPTED_QTY))T_INPUT
From 
PWC_MFG_CUTISSUANCE Cut_Issue_H,
PWC_MFG_CUTISSUANCE_LINE Cut_Issue_L,
PWC_MFG_CUTISSUANCE_SZLINE Cut_Issue_SZL
Where Cut_Issue_H.CUTISSUE_ID=Cut_Issue_L.CUTISSUE_ID 
And   Cut_Issue_L.CUTISSUE_LINE_ID=Cut_Issue_SZL.CUTISSUE_LINE_ID                                     
 And ISSUE_TO = 'Issue to Sewing Line'  And POSTED_STATUS='Y'
 --And TRUNC (Cut_Issue_L.CREATION_DATE) = trunc(to_date('15-Nov-2023')) 
 --And Cut_Issue_L.ATTRIBUTE1 like '2023/4436/4/5' 
 Group by  Cut_Issue_H.CUTISSUE_NO,TRUNC (Cut_Issue_H.CUTISSUE_DT),Cut_Issue_H.DIVISION_ID,ISSUE_TO_ORG,TRACKING_NO,HDR_BUYERNAME,COLOR,LINE_NO,Cut_Issue_H.RATION_NO,Cut_Issue_SZL.SIZ_BND_ID,Cut_Issue_L.ATTRIBUTE1
)A Where TRUNC(Input_Date)=TRUNC (to_date('" + dtpIputdate.Text + "')) And " + SearchKey + " order by CUTISSUE_NO,Cut_No,unit,Line";
            DataTable srcDt = cls.getDataTableOrcl(strOrcl);

            if (srcDt.Rows.Count > 0)
            {
               

                PreDocNo = srcDt.Rows[0][0].ToString();
                string PreDocC="";

                unitSearch=@"SELECT Top 1 Priority, UnitName From (SELECT  Priority, UnitName=case when cuttingUnit='MFL' then 'MFL-Old' else cuttingUnit End  FROM     tblCompanyUnit
Where CompType='sewing')A Where UnitName='" + cmbUnit.Text + @"'";
                unitSearch_DT=cls.getDataTable(unitSearch);
              
               
                for (int i = 0; i < srcDt.Rows.Count; i++)
               {

                    
                   if (txtTrackingNo.Text != "" && PreDocC!= srcDt.Rows[i][0].ToString() )                   
                   {

                       string checkExistCut_Delete = "Delete From tblSewing_CuttingInput_Sizewise Where Orcl_Input_Doc='" + srcDt.Rows[i][0].ToString() + "'";
                       cls.executeSQL(checkExistCut_Delete);
                       PreDocC = srcDt.Rows[i][0].ToString();
                   }
                   string checkExistCut_str = "Select Orcl_Input_Doc,fldReqNo From tblSewing_CuttingInput_Sizewise Where isnull(Orcl_Input_Doc,'')='" + srcDt.Rows[i][0].ToString() + "' And fldDeletedBy is null";
                     DataTable checkExistCut_DT=cls.getDataTable(checkExistCut_str);
                     if (checkExistCut_DT.Rows.Count == 0)
                     {
                         ChallanNo = cmbUnit.Text + "-SIN/" + GetMaxInputReqNo();
                         checkCut = @"Select RequisitionNo,Unit,Buyer,OrderNo,Color,CUT_NO,fldUnit,fldCutNo,fldRefNo From tblCuttingMarkerRequisitionHead M Inner join tblSewing_Cutting C on M.RequisitionNo=C.MerReqNo  Where M.CUT_NO='" + srcDt.Rows[i][4].ToString() + "'";
                         CUT_DT = cls.getDataTable(checkCut);
                         if (CUT_DT.Rows.Count < 1)
                         {
                             MessageBox.Show("Cut NO " + srcDt.Rows[i][4].ToString() + " Tracking No " + srcDt.Rows[i][8].ToString() + " Need to cutting Itegareted in MIS");
                             return;
                         }




                         string SQL = @"INSERT INTO tblSewing_CuttingInput_Sizewise (fldInputDate, fldBuyer, fldOrderNo, fldcolor, fldReqNo, fldcuttingFloor, 
fldInputFloor, fldLine, fldSize, fldqty,fldCreatedBy,fldOffday,CutNo,Orcl_Input_Doc,Orcl_CutNo) 
                                VALUES(cast('" + srcDt.Rows[i][1].ToString() + @"' as date),'" + CUT_DT.Rows[0][2].ToString() + @"',
'" + CUT_DT.Rows[0][3].ToString() + "','" + CUT_DT.Rows[0][4].ToString() + "','" + ChallanNo + "','" + CUT_DT.Rows[0][6].ToString() + "','" + unitSearch_DT.Rows[0][0].ToString() + "','" + srcDt.Rows[i][3].ToString() + "','" + srcDt.Rows[i][6].ToString() + "','" + srcDt.Rows[i][7].ToString() + "','Orcl',1,'" + CUT_DT.Rows[0][7].ToString() + "','" + srcDt.Rows[i][0].ToString() + "','" + srcDt.Rows[i][4].ToString() + "')";
                         cls.executeSQL(SQL);
                         PreDocNo = srcDt.Rows[i][0].ToString();
                     }
                     else if (PreDocNo == srcDt.Rows[i][0].ToString() && CUT_DT !=null)
                     {
                         //ChallanNo = checkExistCut_DT.Rows[0][1].ToString();
                         //checkCut = @"Select RequisitionNo,Unit,Buyer,OrderNo,Color,CUT_NO,fldUnit,fldCutNo,fldRefNo From tblCuttingMarkerRequisitionHead M Inner join tblSewing_Cutting C on M.RequisitionNo=C.MerReqNo  Where M.CUT_NO='" + srcDt.Rows[i][4].ToString() + "'";
                         //CUT_DT = cls.getDataTable(checkCut);                       
                     

                         string SQL = @"INSERT INTO tblSewing_CuttingInput_Sizewise (fldInputDate, fldBuyer, fldOrderNo, fldcolor, fldReqNo, fldcuttingFloor, 
fldInputFloor, fldLine, fldSize, fldqty,fldCreatedBy,fldOffday,CutNo,Orcl_Input_Doc,Orcl_CutNo) 
                                VALUES(cast('" + srcDt.Rows[i][1].ToString() + @"' as date),'"+CUT_DT.Rows[0][2].ToString()+@"',
'" + CUT_DT.Rows[0][3].ToString() + "','" + CUT_DT.Rows[0][4].ToString() + "','" + ChallanNo + "','" + CUT_DT.Rows[0][6].ToString() + "','" + unitSearch_DT.Rows[0][0].ToString() + "','" + srcDt.Rows[i][3].ToString() + "','" + srcDt.Rows[i][6].ToString() + "','" + srcDt.Rows[i][7].ToString() + "','Orcl',1,'" + CUT_DT.Rows[0][7].ToString() + "','" + srcDt.Rows[i][0].ToString() + "','" + srcDt.Rows[i][4].ToString() + "')";
                         cls.executeSQL(SQL);
                         PreDocNo = srcDt.Rows[i][0].ToString();
                     }
               }
                MessageBox.Show("Input Added");
                dgvORCL.DataSource = null;

            }


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string SearchKey = "";
            if (cmbUnit.Text == "" && txtTrackingNo.Text == "")
            {
                return;
            }
            if (cmbUnit.Text != "" && txtTrackingNo.Text != "")
            {
                SearchKey =  " unit = '" + cmbUnit.Text + @"' And TRACKING_NO='" + txtTrackingNo.Text + @"' ";
            }
            if (cmbUnit.Text != "" && txtTrackingNo.Text == "")
            {
                SearchKey = " unit = '" + cmbUnit.Text + @"' ";
            }
            if (cmbUnit.Text == "" && txtTrackingNo.Text != "")
            {
                SearchKey = " TRACKING_NO='" + txtTrackingNo.Text + @"'  ";
            }


            string strOrcl = @" Select TRACKING_NO,CUTISSUE_NO DOC_NO,TO_CHAR(Input_Date,'MM/DD/YYYY')Input_Date,unit,Line,Cut_No,MiS_CutNo,Size_Value,T_INPUT QTY From (Select Cut_Issue_H.CUTISSUE_NO,TRUNC (Cut_Issue_H.CUTISSUE_DT)Input_Date,Cut_Issue_H.DIVISION_ID,ISSUE_TO_ORG,TRACKING_NO,HDR_BUYERNAME,COLOR,
LINE_NO,
reverse(substr(reverse(LINE_NO),INSTR(reverse(LINE_NO),'-',1,1)+1,length(LINE_NO))) unit, 
reverse(substr(reverse(LINE_NO),0,INSTR(reverse(LINE_NO),'-',1,1)-1)) Line, 
reverse(substr(reverse(Cut_Issue_L.ATTRIBUTE1),0,INSTR(reverse(Cut_Issue_L.ATTRIBUTE1 ),'/',1,1)-1)) MiS_CutNo, 
Cut_Issue_L.ATTRIBUTE1 Cut_No,
 Cut_Issue_H.RATION_NO,
 Cut_Issue_SZL.SIZ_BND_ID,
 substr(Cut_Issue_SZL.SIZ_BND_ID,0,length(Cut_Issue_SZL.SIZ_BND_ID)-1)Size_Value,
floor(Sum(Cut_Issue_SZL.ACCEPTED_QTY))T_INPUT
From 
PWC_MFG_CUTISSUANCE Cut_Issue_H,
PWC_MFG_CUTISSUANCE_LINE Cut_Issue_L,
PWC_MFG_CUTISSUANCE_SZLINE Cut_Issue_SZL
Where Cut_Issue_H.CUTISSUE_ID=Cut_Issue_L.CUTISSUE_ID 
And   Cut_Issue_L.CUTISSUE_LINE_ID=Cut_Issue_SZL.CUTISSUE_LINE_ID                                     
 And ISSUE_TO = 'Issue to Sewing Line'  And POSTED_STATUS='Y'
 --And TRUNC (Cut_Issue_L.CREATION_DATE) = trunc(to_date('15-Nov-2023')) 
 --And Cut_Issue_L.ATTRIBUTE1 like '2023/3145/2/3' 
 Group by  Cut_Issue_H.CUTISSUE_NO,TRUNC (Cut_Issue_H.CUTISSUE_DT),Cut_Issue_H.DIVISION_ID,ISSUE_TO_ORG,TRACKING_NO,HDR_BUYERNAME,COLOR,LINE_NO,Cut_Issue_H.RATION_NO,Cut_Issue_SZL.SIZ_BND_ID,Cut_Issue_L.ATTRIBUTE1
)A Where TRUNC(Input_Date)=TRUNC (to_date('" + dtpIputdate.Text + "')) And  " + SearchKey + " order by CUTISSUE_NO,Cut_No,unit,Line";
            DataTable srcDt = cls.getDataTableOrcl(strOrcl);
            dgvORCL.DataSource = srcDt;

        }

        private void btnOutputSearch_Click(object sender, EventArgs e)
        {
            string SearchKey = "", NDate = "", InpuHour = "", SearchKeyD=" 1=1 ";
            DateTime LastTime, EarlyTyme;

          
            if (cmbHour.Text == "24")
            {
                InpuHour = "01-Jan-1900 23:50:00";
            }
            else if(cmbHour.Text!="")
            {
                InpuHour = "01-Jan-1900 "+cmbHour.Text+":00:00";
            }


            if (cmbUnit.Text == "" || InpuHour == "")
            {
                MessageBox.Show("pls Select perameter properly");
                return;
            }
            if (cmbUnit.Text != "" && txtTrackingNo.Text != "")
            {
                SearchKey = " Output_L.OUTPUT_LINE_NO = '" + cmbUnit.Text + @"%' And TRACKING_NO='" + txtTrackingNo.Text + @"' ";
            }
            if (cmbUnit.Text != "" && InpuHour != "" && txtLine.Text == "")
            {
                SearchKey = " Output_L.OUTPUT_LINE_NO  Like  '" + cmbUnit.Text + @"%' And  TRUNC (OUTREC_DATE) = trunc(to_date('" + dtpIputdate.Text + @"', 'DD-Mon-YYYY')) And fldHour= to_date('" + InpuHour + @"', 'DD-Mon-YYYY hh24:mi:ss')";
               
            }
            if (cmbUnit.Text != "" && InpuHour != "" && txtLine.Text != "")
            {
                SearchKey = " Output_L.OUTPUT_LINE_NO  Like  '" + cmbUnit.Text + @"%' And  TRUNC (OUTREC_DATE) = trunc(to_date('" + dtpIputdate.Text + @"', 'DD-Mon-YYYY')) And fldHour= to_date('" + InpuHour + @"', 'DD-Mon-YYYY hh24:mi:ss') And Output_L.OUTPUT_LINE_NO Like '%-" + txtLine.Text + "'";
                SearchKeyD +=" And OUTPUT_LINE_NO  like '%-" + txtLine.Text + "'";
            }



            string strOrcl = @"  Select to_char(OUTREC_DATE, 'DD-Mon-YYYY')ProdDate,Output_H.DOC_NO,
Output_L.SUBLINE_NO,Output_L.OUTPUT_LINE_NO,Output_L.TRACKING_NO,Output_L.PIECE_NAME,Output_L.COLOR,
fldSize,fldHour,fldOutput,Sew_Job_Name,Cut_Panel_ISSUED,Sewing_COMPLETED,Output_D.OUT_SUBLINE_ID From 
(Select OUT_SUBLINE_ID,fldSize,fldHour,sum(fldOutput)fldOutput From 
(Select OUT_SUBLINE_ID,SIZE_VAL fldSize,To_date ('01-Jan-1900 09:00:00','DD-Mon-YYYY hh24:mi:ss') fldHour,R1 fldOutput ,'NT' fldTime From PWC_MFG_LINEOUTPUT_DETAIL
Where 1=1 And R1 is not null
Union ALL
Select OUT_SUBLINE_ID,SIZE_VAL fldSize,To_date ('01-Jan-1900 10:00:00','DD-Mon-YYYY hh24:mi:ss')fldHour,R2 fldOutput,'NT' fldTime From PWC_MFG_LINEOUTPUT_DETAIL
Where 1=1 And R2 is not null
Union ALL
Select OUT_SUBLINE_ID,SIZE_VAL fldSize,To_date ('01-Jan-1900 11:00:00','DD-Mon-YYYY hh24:mi:ss')fldHour,R3 fldOutput,'NT' fldTime From PWC_MFG_LINEOUTPUT_DETAIL
Where 1=1 And R3 is not null
Union ALL
Select OUT_SUBLINE_ID,SIZE_VAL fldSize,To_date ('01-Jan-1900 12:00:00','DD-Mon-YYYY hh24:mi:ss')fldHour,R4 fldOutput,'NT' fldTime From PWC_MFG_LINEOUTPUT_DETAIL
Where 1=1 And R4 is not null
Union ALL
Select OUT_SUBLINE_ID,SIZE_VAL fldSize,To_date ('01-Jan-1900 13:00:00','DD-Mon-YYYY hh24:mi:ss')fldHour,R5 fldOutput,'NT' fldTime From PWC_MFG_LINEOUTPUT_DETAIL
Where 1=1 And R5 is not null
Union ALL
Select OUT_SUBLINE_ID,SIZE_VAL fldSize,To_date ('01-Jan-1900 15:00:00','DD-Mon-YYYY hh24:mi:ss')fldHour,R7 fldOutput,'NT' fldTime From PWC_MFG_LINEOUTPUT_DETAIL
Where 1=1 And R7  is not null
Union ALL
Select OUT_SUBLINE_ID,SIZE_VAL fldSize,To_date ('01-Jan-1900 16:00:00','DD-Mon-YYYY hh24:mi:ss')fldHour,R8 fldOutput,'NT' fldTime From PWC_MFG_LINEOUTPUT_DETAIL
Where 1=1 And R8 is not null
Union ALL
Select OUT_SUBLINE_ID,SIZE_VAL fldSize,To_date ('01-Jan-1900 17:00:00','DD-Mon-YYYY hh24:mi:ss')fldHour,R9 fldOutput,'NT' fldTime From PWC_MFG_LINEOUTPUT_DETAIL
Where 1=1 And R9 is not null
Union ALL
Select OUT_SUBLINE_ID,SIZE_VAL fldSize,To_date ('01-Jan-1900 18:00:00','DD-Mon-YYYY hh24:mi:ss')fldHour,R10 fldOutput,'NT' fldTime From PWC_MFG_LINEOUTPUT_DETAIL
Where 1=1 And R10 is not null
Union ALL
Select OUT_SUBLINE_ID,SIZE_VAL fldSize,To_date ('01-Jan-1900 19:00:00','DD-Mon-YYYY hh24:mi:ss')fldHour,R11 fldOutput,'NT' fldTime From PWC_MFG_LINEOUTPUT_DETAIL
Where 1=1 And R11 is not null
Union ALL
Select OUT_SUBLINE_ID,SIZE_VAL fldSize,To_date ('01-Jan-1900 20:00:00','DD-Mon-YYYY hh24:mi:ss')fldHour,R12 fldOutput,'NT' fldTime From PWC_MFG_LINEOUTPUT_DETAIL
Where 1=1 And R12 is not null
Union ALL
Select OUT_SUBLINE_ID,SIZE_VAL fldSize,To_date ('01-Jan-1900 21:00:00','DD-Mon-YYYY hh24:mi:ss')fldHour,R13 fldOutput,'NT' fldTime From PWC_MFG_LINEOUTPUT_DETAIL
Where 1=1 And R13 is not null
Union ALL
Select OUT_SUBLINE_ID,SIZE_VAL fldSize,To_date ('01-Jan-1900 22:00:00','DD-Mon-YYYY hh24:mi:ss')fldHour,R14 fldOutput,'NT' fldTime From PWC_MFG_LINEOUTPUT_DETAIL
Where 1=1 And R14 is not null
Union ALL
Select OUT_SUBLINE_ID,SIZE_VAL fldSize,To_date ('01-Jan-1900 23:00:00','DD-Mon-YYYY hh24:mi:ss')fldHour,R15 fldOutput,'NT' fldTime From PWC_MFG_LINEOUTPUT_DETAIL
Where 1=1 And R15 is not null
Union ALL
Select OUT_SUBLINE_ID,SIZE_VAL fldSize,To_date ('01-Jan-1900 23:50:00','DD-Mon-YYYY hh24:mi:ss')fldHour,R16 fldOutput,'NT' fldTime From PWC_MFG_LINEOUTPUT_DETAIL
Where 1=1 And R16 is not null
Union ALL
Select OUT_SUBLINE_ID,SIZE_VAL fldSize,To_date ('01-Jan-1900 01:00:00','DD-Mon-YYYY hh24:mi:ss')fldHour,R17 fldOutput,'NT' fldTime From PWC_MFG_LINEOUTPUT_DETAIL
Where 1=1 And R17 is not null
Union ALL
Select OUT_SUBLINE_ID,SIZE_VAL fldSize,To_date ('01-Jan-1900 02:00:00','DD-Mon-YYYY hh24:mi:ss')fldHour,R18 fldOutput,'NT' fldTime From PWC_MFG_LINEOUTPUT_DETAIL
Where 1=1 And R18 is not null
Union ALL
Select OUT_SUBLINE_ID,SIZE_VAL fldSize,To_date ('01-Jan-1900 03:00:00','DD-Mon-YYYY hh24:mi:ss')fldHour,R19 fldOutput,'NT' fldTime From PWC_MFG_LINEOUTPUT_DETAIL
Where 1=1 And R19 is not null
Union ALL
Select OUT_SUBLINE_ID,SIZE_VAL fldSize,To_date ('01-Jan-1900 04:00:00','DD-Mon-YYYY hh24:mi:ss')fldHour,R20 fldOutput,'NT' fldTime From PWC_MFG_LINEOUTPUT_DETAIL
Where 1=1 And R20 is not null
Union ALL
Select OUT_SUBLINE_ID,SIZE_VAL fldSize,To_date ('01-Jan-1900 05:00:00','DD-Mon-YYYY hh24:mi:ss')fldHour,R21 fldOutput,'NT' fldTime From PWC_MFG_LINEOUTPUT_DETAIL
Where 1=1 And R21 is not null
Union ALL
Select OUT_SUBLINE_ID,SIZE_VAL fldSize,To_date ('01-Jan-1900 06:00:00','DD-Mon-YYYY hh24:mi:ss')fldHour,R22 fldOutput,'NT' fldTime From PWC_MFG_LINEOUTPUT_DETAIL
Where 1=1 And R22 is not null
)A group by OUT_SUBLINE_ID,fldSize,fldHour)Output_D,
(Select DIVISION_ID,PROD_TRANS_ID,OUT_SUBLINE_ID,SUBLINE_NO,OUTPUT_LINE_NO,TRACKING_NO,PIECE_NAME,COLOR,JOB_ID From PWC_MFG_LINEOUTPUT_SBLINE)Output_L,
PWC_MFG_LINEOUTPUT Output_H,
(Select a.WIP_ENTITY_ID,a.CLASS_CODE,min(A.DATE_RELEASED)Sew_Job_RELEASED,b.organization_id,a.ATTRIBUTE2,a.ATTRIBUTE4,a.ATTRIBUTE7,WIP_ENTITY_NAME Sew_Job_Name,sum(B.QUANTITY_ISSUED)Cut_Panel_ISSUED,sum(a.QUANTITY_COMPLETED)Sewing_COMPLETED,A.STATUS_TYPE_DISP Sewing_Job_Status From  WIP_DISCRETE_JOBS_V a,
        wip_requirement_operations b
        Where a.WIP_ENTITY_ID = b.WIP_ENTITY_ID(+)
      -- And  a.WIP_ENTITY_ID=2628536
        And a.CLASS_CODE='Sewing' And a.ATTRIBUTE_CATEGORY='RMG' Group by a.WIP_ENTITY_ID,a.CLASS_CODE,b.organization_id,a.ATTRIBUTE2,a.ATTRIBUTE4,a.ATTRIBUTE7,WIP_ENTITY_NAME,A.STATUS_TYPE_DISP)Job

Where Output_D.OUT_SUBLINE_ID=Output_L.OUT_SUBLINE_ID
And Output_L.PROD_TRANS_ID=Output_H.PROD_TRANS_ID And " + SearchKey+"And Output_L.JOB_ID=Job.WIP_ENTITY_ID(+) order by Output_L.OUTPUT_LINE_NO,fldHour desc";


            DataTable srcDt = cls.getDataTableOrcl(strOrcl);

            if (srcDt.Rows.Count > 0)
            {
                string Sqld = " Delete From tblDailyOrclOutput Where ProdDate='" + dtpIputdate.Text + @"' And fldHour='" + srcDt.Rows[0][8].ToString() + "' And OUTPUT_LINE_NO  like  '" + cmbUnit.Text + @"%' And " + SearchKeyD + " ";
                cls.executeSQL(Sqld);
                for (int i = 0; i < srcDt.Rows.Count; i++)
                {
                    string SQL = @"INSERT INTO tblDailyOrclOutput (ProdDate, DOC_NO, SUBLINE_NO, OUTPUT_LINE_NO, TRACKING_NO, PIECE_NAME, COLOR, fldSize,fldHour, fldOutput, Sew_Job_Name, Cut_Panel_ISSUED, Sewing_COMPLETED, OUT_SUBLINE_ID) 
                                VALUES(cast('" + srcDt.Rows[i][0].ToString() + @"' as date),'" + srcDt.Rows[i][1].ToString() + @"',
'" + srcDt.Rows[i][2].ToString() + "','" + srcDt.Rows[i][3].ToString() + "','" + srcDt.Rows[i][4].ToString() + "','" + srcDt.Rows[i][5].ToString() + "','" + srcDt.Rows[i][6].ToString() + "','" + srcDt.Rows[i][7].ToString() + "','" + srcDt.Rows[i][8].ToString() + "','" + srcDt.Rows[i][9].ToString() + "','" + srcDt.Rows[i][10].ToString() + "','" + srcDt.Rows[i][11].ToString() + "','" + srcDt.Rows[i][12].ToString() + "','" + srcDt.Rows[i][13].ToString() + "')";
                    cls.executeSQL(SQL);
                }  
            }


            dgvORCL.DataSource = srcDt;
        }

        private void btnOutputTrns_Click(object sender, EventArgs e)
        {
            string InpuHour = "",ProdInsSQL="",ProdDelSql="";
            DateTime Hour;
            string SearchKey = "", NDate = "",lineSearch= " Where 1=1 ";
            DateTime LastTime, EarlyTyme;

            string SQLCheckBack = @"Select dateAdd(dd," + CheckBackDate + @",Cast(Getdate() as date))SysDate,'" + dtpIputdate.Text + @"' ProdDate
Where dateAdd(dd," + CheckBackDate + @",Cast(Getdate() as date))<='" + dtpIputdate.Text + @"'";
            DataTable DtBack = cls.getDataTable(SQLCheckBack);
            if (DtBack.Rows.Count == 0)
            {
                MessageBox.Show("This date data are not allow to transfer","Error");
                return;
            }

            if (cmbHour.Text == "24")
            {
                InpuHour = "01-Jan-1900 23:50:00";
            }
            else if (cmbHour.Text != "")
            {
                InpuHour = "01-Jan-1900 " + cmbHour.Text + ":00:00";
            }
            if (cmbUnit.Text == "" || InpuHour == "")
            {
                MessageBox.Show("pls Select perameter properly");
                return;
            }

            if (cmbUnit.Text != "" && InpuHour != "")
            {
                SearchKey = " OUTPUT_LINE_NO  Like  '" + cmbUnit.Text + @"%' And fldHour= cast('" + InpuHour + @"' as datetime)";
            }
            if (txtLine.Text != "")
            {
                lineSearch += " And substring(OUTPUT_LINE_NO,len(OutUnit)+2,len(OUTPUT_LINE_NO))='" + txtLine.Text + "'";
            }
            string StrSql = @"Select convert(varchar,ProdDate,106)ProdDate,FABRIC_REQ_NO+' '+B.fldstname+'::'+PIECE_NAME PRODUCT_NAME, OUTPUT_LINE_NO,OutUnit=case when OutUnit='MFL-Old' then 'MFL' else OutUnit end,substring(OUTPUT_LINE_NO,len(OutUnit)+2,len(OUTPUT_LINE_NO))OutLine,TRACKING_NO,PIECE_NAME,COLOR,fldSize,fldHour,fldOutput,SUBLINE_NO,
InUnit,substring(SUBLINE_NO,len(InUnit)+2,len(SUBLINE_NO))InLine From 
(Select ProdDate,OUTPUT_LINE_NO,reverse(substring(reverse(OUTPUT_LINE_NO),charindex('-',reverse(OUTPUT_LINE_NO))+1,len(OUTPUT_LINE_NO)))OutUnit,
TRACKING_NO,PIECE_NAME,COLOR,fldSize,fldHour,fldOutput,SUBLINE_NO,
reverse(substring(reverse(SUBLINE_NO),charindex('-',reverse(SUBLINE_NO))+1,len(SUBLINE_NO)))InUnit
From tblDailyOrclOutput 
Where ProdDate='" + dtpIputdate.Text + @"' And  " + SearchKey + @")A
Left outer join
(Select Distinct BUYERNAME,TRACKING_NUMBER,FABRIC_REQ_NO From
(Select BUYERNAME,TRACKING_NUMBER,FABRIC_REQ_NO From tbl_PWC_FRS_HEADERS where  BOOKINGTYPE='RMG Bulk Fabric Order'
Union ALL
Select BUYER_NAME,TRACKING_NO,FABRIC_REQ_NO From tbl_PWC_CUT_HEAD)A)H
On A.TRACKING_NO=H.TRACKING_NUMBER 
left outer join tblBuyer B on H.BUYERNAME=B.ERPName " + lineSearch+" order by substring(OUTPUT_LINE_NO,len(OutUnit)+2,len(OUTPUT_LINE_NO))";
            DataTable srcOrclDt = cls.getDataTable(StrSql);

            if (txtLine.Text == "")
            {
                ProdDelSql = "Delete  From tblDailySeweingNew Where fldDate='" + srcOrclDt.Rows[0][0].ToString() + "' And fldCompanyCode='" + srcOrclDt.Rows[0][3].ToString() + @"' 
            And fldHour='" + InpuHour + "'  And OpType=0 And fldCreateBY='Orcl'";
            }
            else if (txtLine.Text != "")
            {
                ProdDelSql = "Delete  From tblDailySeweingNew Where fldDate='" + srcOrclDt.Rows[0][0].ToString() + "' And fldCompanyCode='" + srcOrclDt.Rows[0][3].ToString() + @"' 
            And fldLine='" + txtLine.Text + "' And fldHour='" + InpuHour + "'  And OpType=0 And fldCreateBY='Orcl'";
            }

            cls.executeSQL(ProdDelSql);

            for (int i = 0; i < srcOrclDt.Rows.Count; i++)
            {

                string HelperOperatorSQL = @"SELECT helper + operator AS StMan FROM tblDailyHelperOperatorNew 
                                WHERE(_date = '" + srcOrclDt.Rows[i][0].ToString() + "') AND (otStTime = '1/1/1900 9:00:00 AM') AND (line = '" + srcOrclDt.Rows[i][4].ToString() + "') AND (DeletedBy IS NULL OR DeletedBy = '') AND (factoryUnit = '" + srcOrclDt.Rows[i][3].ToString() + "') And OpType='0'";
                DataTable DTFM = cls.getDataTable(HelperOperatorSQL);
                if (DTFM.Rows.Count > 0)
                {
                    StOpHp = Convert.ToDecimal(DTFM.Rows[0][0].ToString());

                }

                DateTime FF = DateTime.Parse(srcOrclDt.Rows[i][0].ToString()).AddHours(Convert.ToInt16(cmbHour.Text));
                string otStTime = FF.ToString("dd-MMM-yyyy HH:mm:ss");

                string CurrentManSql = @"SELECT (operator+helper)ManPower From (SELECT  top 1 operator,helper From (SELECT  orderNo,operator,helper,
                otStTime=Case when otStTime between '01-01-1900 00:00:00' AND '01-01-1900 6:00:00' Then dateadd(dd,1,_date)+otStTime Else _date+otStTime End
                FROM  tblDailyHelperOperatorNew Where _date='" + srcOrclDt.Rows[i][0].ToString() + "' AND factoryUnit='" + srcOrclDt.Rows[i][3].ToString() + "'  AND line='" + srcOrclDt.Rows[i][4].ToString() + "' AND (DeletedBy is null OR DeletedBy='')  And OpType='0')A Where otStTime<='" + otStTime + "' order by otStTime Desc)X ";

                DataTable DTM = cls.getDataTable(CurrentManSql);
                if (DTM.Rows.Count > 0)
                {
                    TCurrentHOP = Convert.ToDecimal(DTM.Rows[0][0].ToString());
                }

                SomeAricals(Convert.ToDateTime(InpuHour), TCurrentHOP, StOpHp,srcOrclDt.Rows[i][0].ToString());
                GMT_prod_TGT(srcOrclDt.Rows[i][0].ToString(), srcOrclDt.Rows[i][1].ToString(), srcOrclDt.Rows[i][3].ToString(), srcOrclDt.Rows[i][4].ToString());

                string StripSql="exec [dbo].[LoadGMTFRStrip_New] '"+srcOrclDt.Rows[i][1].ToString()+"','"+srcOrclDt.Rows[i][7].ToString()+"','"+srcOrclDt.Rows[i][3].ToString()+"','"+srcOrclDt.Rows[i][4].ToString()+"'";
                 DataTable DTStrip = cls.getDataTable(StripSql);
                 if (DTStrip.Rows.Count == 0 || DTStrip.Rows[0][0].ToString().Length<10)
                 {
                     MessageBox.Show("Strip Not Found");
                         return;
                         dgvORCL.DataSource = null;
                 }
                 StripID = DTStrip.Rows[0][0].ToString();
              

                 ProdInsSQL = @"Insert Into tblDailySeweingNew(fldDate, PRODUCT_NAME, fldCompanyCode, fldColour, fldLine, fldHour, fldSize, fldTarget, fldOutput, fldTime,fldNT, fldOT, fldExtraOT,fldCreateBY,fldInputLine,fldSTBS,fldProDay,fldInsTime,OpType,fldOrigOrderSeweing,fldFRShipID)Values (
'" + srcOrclDt.Rows[i][0].ToString() + "','" + srcOrclDt.Rows[i][1].ToString() + "','" + srcOrclDt.Rows[i][3].ToString() + "','" + srcOrclDt.Rows[i][7].ToString() + "','" + srcOrclDt.Rows[i][4].ToString() + "','" + InpuHour + "','" + srcOrclDt.Rows[i][8].ToString() + "'," + TGTPcs + ",'" + srcOrclDt.Rows[i][10].ToString() + "','" + HourType + "','" + NT + "','" + OT + "','" + ExOT + "','Orcl','" + srcOrclDt.Rows[i][13].ToString() + "','" + stbsRevNo + "','" + ProdDayCount + "',getdate(),0,left('" + srcOrclDt.Rows[i][1].ToString() + "', charindex(' ', '" + srcOrclDt.Rows[i][1].ToString() + "') - 1),'" + StripID + "')";
                 cls.executeSQL(ProdInsSQL);
            }
            MessageBox.Show("Added Successfully");
            dgvORCL.DataSource = null;
        }
private void SomeAricals(DateTime d, decimal TCurrentHOP, decimal StOpHp,string EntryDAte)
                    {

                        NT = 0;
                        OT = 0;
                        ExOT = 0;


                        string SQL = @"Select Sum(Offday)Offday From (
            Select case when datepart(dw,'" + dtpIputdate.Text + @"')=6 then 1 else 0 end Offday
            union All 
            Select count(*)Offday from tblCalendar where fldDateTime=cast('" + dtpIputdate.Text + @"' as date))A";
                        DataTable DT = cls.getDataTable(SQL);

                        if (DT.Rows[0][0].ToString()=="0")
                        {
                            if ((d == Convert.ToDateTime("01/01/1900 6:00:00 PM") || d == Convert.ToDateTime("01/01/1900 7:00:00 PM")))
                            {
                                HourType = "OT";
                                if (TCurrentHOP == 0)
                                {
                                    OT = 1;
                                }
                                else
                                {
                                    OT = Math.Round((TCurrentHOP / StOpHp), 2);
                                }
                            }
                            else if ((d > Convert.ToDateTime("01/01/1900 8:00:00 AM") && d < Convert.ToDateTime("01/01/1900 6:00:00 PM")))
                            {
                                HourType = "NT";
                                if (TCurrentHOP == 0)
                                {
                                    NT = 1;
                                }
                                else
                                {
                                    NT = Math.Round((TCurrentHOP / StOpHp), 2);
                                }
                            }
                            else
                            {
                                HourType = "EX-OT";
                                if (TCurrentHOP == 0)
                                {
                                    ExOT = 1;
                                }
                                else
                                {
                                    ExOT = Math.Round((TCurrentHOP / StOpHp), 2);
                                }

                            }
                        }
                        else
                        {
                            HourType = "EX-OT";
                            if (TCurrentHOP == 0)
                            {
                                ExOT = 1;
                            }
                            else
                            {
                                ExOT = Math.Round((TCurrentHOP / StOpHp), 2);
                            }
                        }

                    }

      public void GMT_prod_TGT(string ProdDate, string orderNo, string unit, string  Outline)
            {

                string TGTS = "0", TGTCM = "0", SQL; int XX;

                DateTime Hour = Convert.ToDateTime("01/01/1900 00:00:00");

                decimal totalSMV = 0, TGT;

                if (cmbHour.Text == "24")
                {
                    XX = 0;
                }
                else
                {
                    XX = Convert.ToInt16(cmbHour.Text);
                }
                if (XX >= 1 && XX < 7)
                {
                    Hour = Convert.ToDateTime(ProdDate).AddDays(1).AddHours(Convert.ToInt16(cmbHour.Text));
                }
                else
                {
                    Hour = Convert.ToDateTime(ProdDate).AddHours(Convert.ToInt16(cmbHour.Text));
                }



                SQL = @"SELECT (operator+helper)ManPower From (SELECT  top 1 operator,helper From (SELECT  macchine,operator,helper,
otStTime=Case when otStTime between '01-01-1900 00:00:00' AND '01-01-1900 6:00:00' Then dateadd(dd,1,_date)+otStTime Else _date+otStTime End
FROM  tblDailyHelperOperatorNew Where _date='"+ProdDate+"' AND factoryUnit='"+unit+@"'
 AND line='" + Outline + "' AND (DeletedBy is null OR DeletedBy='')  And OpType=0)A Where otStTime<=cast('" + Hour.ToString("yyyy-MM-dd HH:mm:ss") + "' as datetime) order by otStTime Desc)X ";
                DataTable DTM = cls.getDataTable(SQL);


                SQL = @"SELECT  isnull(Sum(fldSMV),0)Smv,ReviseStatus From
(SELECT fldSyleNo, fldIDS,ReviseStatus From
(SELECT  Convert(varchar,fldInsDate,101)fldInsDate,fldSyleNo, fldIDS,isnull(ReviseStatus,0)ReviseStatus FROM DBL_WS.dbo.tblSTBS 
Where fldSyleNo='"+orderNo+"' and  OpType=0)A Where  CAST(fldInsDate as datetime)<='"+ProdDate+@"'
AND ReviseStatus IN ( SELECT MAX(ReviseStatus)ReviseStatus From(
SELECT  Convert(varchar,fldInsDate,101)fldInsDate,fldSyleNo, fldIDS,isnull(ReviseStatus,0)ReviseStatus FROM DBL_WS.dbo.tblSTBS 
Where fldSyleNo='" + orderNo + "'  and  OpType=0 AND Status=1)A Where  CAST(fldInsDate as datetime)<='" + ProdDate + @"'))A Left outer join (SELECT     fldSMV, fldID FROM DBL_WS.dbo.tbl_SMV_BANK)B On A.fldIDS=B.fldID Group by ReviseStatus";
                DataTable DTSMV = cls.getDataTable(SQL);



                if (DTSMV.Rows.Count > 0)
                {
                    totalSMV = Convert.ToDecimal(DTSMV.Rows[0][0].ToString());
                    stbs = Convert.ToDecimal(DTSMV.Rows[0][1].ToString());
                }

                if (totalSMV == 0 || DTM.Rows.Count == 0)
                {
                    TGTS = "0";
                    //stbs =Convert.ToDecimal(null);
                }
                else
                {
                    if (DTM.Rows.Count > 0)
                    {
                        TCurrentHOP = Convert.ToDecimal(DTM.Rows[0][0].ToString());
                    }

                    TGT = ((60 / totalSMV) * (TCurrentHOP) * 100) / 100;
                    //txtTarget.Text = Math.Round(Convert.ToDecimal(TGT), 2).ToString();
                    TGT = Math.Round(Convert.ToDecimal(Learning_Curve_TGT(ProdDate,orderNo, unit, Outline, TGT, totalSMV)), 2);

                    TGTS = Convert.ToString(TGT);

                }


                TGTPcs = TGTS;
                stbsRevNo = stbs;
                ProdDayCount = ProdDay;

            }
            private decimal Learning_Curve_TGT(string date, string LOrder, string LUnit, string LLine, decimal Ltgt, decimal LSMV)
            {
                decimal LearningTGT = 0;
                string checkSQL,SQL="";

                SQL = @"SELECT top 1 fldStDate, fldLikeOrder FROM tblDailySeweingOrderSetup 
      WHERE fldStDate<=dateadd(dd,0,'" + date + "') AND  (fldUnit ='" + LUnit + "') AND (fldLine = '" + LLine + "') AND (fldOrder = '" + LOrder + "') order by fldID desc";
                DataTable LDTTGTHU = cls.getDataTable(SQL);

                if (LDTTGTHU.Rows.Count == 0)
                {
                    SQL = @"Select Datee,Day=case when Day is null then Datee else Day end, 
Day_Percentage=case when Day_Percentage is null then (SELECT Max(Day_Percentage) FROM tbl_Learning_Curve 
WHERE (" + LSMV + " BETWEEN MIN_SMV AND MAX_SMV)) else Day_Percentage end From (Select count(fldDate)+1 Datee From(SELECT    distinct fldDate FROM tblDailySeweingNew WHERE fldDate<=dateadd(dd,-1,'" + date + "') AND ((fldDeletedBy IS NULL) OR (fldDeletedBy = ''))  AND (PRODUCT_NAME = '" + LOrder + "') AND fldCompanyCode='" + LUnit + "' AND fldLine='" + LLine + "')X)DS Left outer join (SELECT Day, Day_Percentage FROM tbl_Learning_Curve WHERE (" + LSMV + " BETWEEN MIN_SMV AND MAX_SMV))S on S.Day=DS.Datee";
                }
                else if (LDTTGTHU.Rows[0][1].ToString() == "")
                {
                    SQL = @"Select Datee,Day=case when Day is null then Datee else Day end, 
Day_Percentage=case when Day_Percentage is null then (SELECT Max(Day_Percentage) FROM tbl_Learning_Curve 
WHERE (" + LSMV + " BETWEEN MIN_SMV AND MAX_SMV)) else Day_Percentage end From (Select fldStDate Datee From(SELECT  top 1 datediff(dd,fldStDate,'" + date + @"')fldStDate FROM tblDailySeweingOrderSetup 
WHERE (fldOrder = '" + LOrder + "') AND fldUnit='" + LUnit + "' AND fldLine='" + LLine + "')X)DS Left outer join (SELECT Day, Day_Percentage FROM tbl_Learning_Curve WHERE (" + LSMV + " BETWEEN MIN_SMV AND MAX_SMV))S on S.Day=DS.Datee";
                }
                else if (LDTTGTHU.Rows[0][1].ToString() != "")
                {
                    SQL = @"Select Datee,Day=case when Day is null then Datee else Day end, 
Day_Percentage=case when Day_Percentage is null then (SELECT Max(Day_Percentage) FROM tbl_Learning_Curve 
WHERE (" + LSMV + " BETWEEN MIN_SMV AND MAX_SMV)) else Day_Percentage end From (Select count(fldDate)+1 Datee From(SELECT    distinct fldDate FROM tblDailySeweingNew WHERE fldDate<=dateadd(dd,-1,'" + date + "') AND ((fldDeletedBy IS NULL) OR (fldDeletedBy = ''))  AND (PRODUCT_NAME = '" + LDTTGTHU.Rows[0][1].ToString() + "') AND fldCompanyCode='" + LUnit + "' AND fldLine Like '%" + LLine + "%')X)DS Left outer join (SELECT Day, Day_Percentage FROM tbl_Learning_Curve WHERE (" + LSMV + " BETWEEN MIN_SMV AND MAX_SMV))S on S.Day=DS.Datee";
                }
                DataTable LDTTGT = cls.getDataTable(SQL);
                ProdDay = Convert.ToInt16(LDTTGT.Rows[0][0].ToString());
                if (LDTTGT.Rows.Count == 1 && LDTTGT.Rows[0][1].ToString() == "")
                {
                    LearningTGT = Ltgt;
                }
                else
                {
                    try
                    {
                        LearningTGT = (Convert.ToDecimal(LDTTGT.Rows[0][2].ToString()) / 100) * Ltgt;
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return LearningTGT;
            }

            private void BtnFinSearch_Click(object sender, EventArgs e)
            {
                string SearchKey = "";
                if (cmbUnit.Text == "" && txtTrackingNo.Text == "")
                {
                    return;
                }
                if (cmbUnit.Text != "" && txtTrackingNo.Text != "")
                {
                    SearchKey = " unit = '" + cmbUnit.Text + @"' And TRACKING_NO='" + txtTrackingNo.Text + @"' ";
                }
                if (cmbUnit.Text != "" && txtTrackingNo.Text == "")
                {
                    SearchKey = " unit = '" + cmbUnit.Text + @"' ";
                }
                if (cmbUnit.Text == "" && txtTrackingNo.Text != "")
                {
                    SearchKey = " TRACKING_NO='" + txtTrackingNo.Text + @"'  ";
                }

                string strOrcl = @" Select  DOC_NO,TRACKING_NO,TO_CHAR(PROD_DATE,'MM/DD/YYYY')PROD_DATE,UNIT,Line,PIECE_NAME,COLOR,SIZE_INSEAM,QUANTITY From
(Select 
FIN_H.TRACKING_NO,
FIN_H.RECEIVE_NO DOC_NO,
FIN_H.UPDATE_ON PROD_DATE,
FIN_H.ATTRIBUTE1 UnitName,
FIN_H.LINE_NO unit,
''Line, 
FIN_L.ATTRIBUTE3 COLOR,
FIN_SIZ.SIZE_INSEAM,
FIN_SIZ.QUANTITY,
FIN_L.PIECE_NAME
From
PWC_MFG_ISSUE_TO_FINISH_H FIN_H,
PWC_MFG_ISSUE_TO_FINISH_L FIN_L,
PWC_MFG_ISSUE_FIN_INSEAM_SIZE FIN_SIZ
Where 1=1
AND FIN_H.HEADER_ID=FIN_L.HEADER_ID
AND FIN_L.LINE_ID=FIN_SIZ.LINE_ID)A Where  TRUNC(PROD_DATE)=TRUNC (to_date('" + dtpIputdate.Text + "')) And  " + SearchKey + " Order by TRACKING_NO,DOC_NO,Line";
                DataTable srcDt = cls.getDataTableOrcl(strOrcl);
                dgvORCL.DataSource = srcDt;
            }

            private void btnFinTrns_Click(object sender, EventArgs e)
            {
                string Challan = "", ChallanNo = "", checkDocNo = "", checkCut = "", PreDocNo = "", unitSearch;
                DataTable CUT_DT = null, unitSearch_DT = null;

                string SQLCheckBack = @"Select dateAdd(dd," + CheckBackDate + @",Cast(Getdate() as date))SysDate,'" + dtpIputdate.Text + @"' ProdDate
Where dateAdd(dd," + CheckBackDate + @",Cast(Getdate() as date))<='" + dtpIputdate.Text + @"'";
                DataTable DtBack = cls.getDataTable(SQLCheckBack);
                if (DtBack.Rows.Count == 0)
                {
                    MessageBox.Show("This date data are not allow to transfer", "Error");
                    return;
                }

                string SearchKey = "";
                if (cmbUnit.Text == "" && txtTrackingNo.Text == "")
                {
                    return;
                }
                if (cmbUnit.Text != "" && txtTrackingNo.Text != "")
                {
                    SearchKey = " unit = '" + cmbUnit.Text + @"' And TRACKING_NO='" + txtTrackingNo.Text + @"' ";
                }
                if (cmbUnit.Text != "" && txtTrackingNo.Text == "")
                {
                    SearchKey = " unit = '" + cmbUnit.Text + @"' ";
                }
                if (cmbUnit.Text == "" && txtTrackingNo.Text != "")
                {
                    SearchKey = " TRACKING_NO='" + txtTrackingNo.Text + @"'  ";
                }

                string strOrcl = @" Select  DOC_NO,TRACKING_NO,TO_CHAR(PROD_DATE,'MM/DD/YYYY')PROD_DATE,UNIT,Line,PIECE_NAME,COLOR,SIZE_INSEAM,QUANTITY From
                                    (Select 
                                    FIN_H.TRACKING_NO,
                                    FIN_H.RECEIVE_NO DOC_NO,
                                    FIN_H.UPDATE_ON PROD_DATE,
                                    FIN_H.ATTRIBUTE1 UnitName,
                                    FIN_H.LINE_NO unit,
                                    '' Line, 
                                    FIN_L.ATTRIBUTE3 COLOR,
                                    FIN_SIZ.SIZE_INSEAM,
                                    FIN_SIZ.QUANTITY,
                                    FIN_L.PIECE_NAME
                                    From
                                    PWC_MFG_ISSUE_TO_FINISH_H FIN_H,
                                    PWC_MFG_ISSUE_TO_FINISH_L FIN_L,
                                    PWC_MFG_ISSUE_FIN_INSEAM_SIZE FIN_SIZ
                                    Where 1=1
                                    AND FIN_H.HEADER_ID=FIN_L.HEADER_ID
                                    AND FIN_L.LINE_ID=FIN_SIZ.LINE_ID)A Where  TRUNC(PROD_DATE)=TRUNC (to_date('" + dtpIputdate.Text + "')) And  " + SearchKey + " Order by TRACKING_NO,DOC_NO,Line";
                DataTable srcDt = cls.getDataTableOrcl(strOrcl);


                if (srcDt.Rows.Count > 0)
                {


                    PreDocNo = srcDt.Rows[0][0].ToString();
                    string PreDocC = "";

                    unitSearch = @"SELECT Top 1 Priority, UnitName From (SELECT  Priority, UnitName=case when cuttingUnit='MFL' then 'MFL-Old' else cuttingUnit End  FROM     tblCompanyUnit
Where CompType='sewing')A Where UnitName='" + cmbUnit.Text + @"'";
                    unitSearch_DT = cls.getDataTable(unitSearch);


                    for (int i = 0; i < srcDt.Rows.Count; i++)
                    {


                        if (txtTrackingNo.Text != "" && PreDocC != srcDt.Rows[i][0].ToString())
                        {

                            string checkExistCut_Delete = "Delete From  tblDailyFinishingReceiveSizeWise Where ORC_DOC_NO='" + srcDt.Rows[i][0].ToString() + "'";
                            cls.executeSQL(checkExistCut_Delete);
                            PreDocC = srcDt.Rows[i][0].ToString();
                        }
                        string checkExistCut_str = "Select ORC_DOC_NO,TRACKING_NO From tblDailyFinishingReceiveSizeWise Where isnull(ORC_DOC_NO,'')='" + srcDt.Rows[i][0].ToString() + "' And fldDeletedBy is null";
                        DataTable checkExistCut_DT = cls.getDataTable(checkExistCut_str);
                        if (checkExistCut_DT.Rows.Count == 0)
                        {
                            //ChallanNo = cmbUnit.Text + "-SIN/" + GetMaxInputReqNo();
                            checkCut = @"Select FABRIC_REQ_NO+' '+B.fldstname+'::'+PIECE_NAME PRODUCT_NAME From (Select distinct BUYER_NAME,TRACKING_NO,FABRIC_REQ_NO,'" + srcDt.Rows[i][5].ToString() + "'PIECE_NAME From tbl_PWC_CUT_HEAD where  TRACKING_NO='" + srcDt.Rows[i][1].ToString() + @"')H
                                     left outer join tblBuyer B on H.BUYER_NAME=B.ERPName";
                            CUT_DT = cls.getDataTable(checkCut);
                            if (CUT_DT.Rows.Count < 1)
                            {
                                MessageBox.Show("tbl_PWC_CUT_HEAD " + srcDt.Rows[i][4].ToString() + " Tracking No " + srcDt.Rows[i][8].ToString() + " Need to Itegareted in MIS");
                                return;
                            }




                            string SQL = @"INSERT INTO tblDailyFinishingReceiveSizeWise (fldFRcDate, PRODUCT_NAME, fldColour, fldOutputFloor, fldOutputLine, fldFRcFloor, fldFRcLine, fldHour, fldSize, fldFRc, fldDisplay, fldCreateBY,ORC_DOC_NO, TRACKING_NO) 
                                VALUES(cast('" + srcDt.Rows[i][2].ToString() + @"' as date),'" + CUT_DT.Rows[0][0].ToString() + @"',
'" + srcDt.Rows[i][6].ToString() + "','" + unitSearch_DT.Rows[0][0].ToString() + "','" + srcDt.Rows[i][4].ToString() + "','" + unitSearch_DT.Rows[0][0].ToString() + "','" + srcDt.Rows[i][4].ToString() + "','01/01/1900 09:00:00','" + srcDt.Rows[i][7].ToString() + "','" + srcDt.Rows[i][8].ToString() + "','0','Orcl','" + srcDt.Rows[i][0].ToString() + "','" + srcDt.Rows[i][1].ToString() + "')";
                            cls.executeSQL(SQL);
                            PreDocNo = srcDt.Rows[i][0].ToString();
                        }
                        else if (PreDocNo == srcDt.Rows[i][0].ToString() && CUT_DT != null)
                        {
                            checkCut = @"Select FABRIC_REQ_NO+' '+B.fldstname+'::'+PIECE_NAME PRODUCT_NAME From (Select distinct BUYER_NAME,TRACKING_NO,FABRIC_REQ_NO,'" + srcDt.Rows[i][5].ToString() + "'PIECE_NAME From tbl_PWC_CUT_HEAD where  TRACKING_NO='" + srcDt.Rows[i][1].ToString() + @"')H
                                     left outer join tblBuyer B on H.BUYER_NAME=B.ERPName";
                            CUT_DT = cls.getDataTable(checkCut);                    


                            string SQL = @"INSERT INTO tblDailyFinishingReceiveSizeWise (fldFRcDate, PRODUCT_NAME, fldColour, fldOutputFloor, fldOutputLine, fldFRcFloor, fldFRcLine, fldHour, fldSize, fldFRc, fldDisplay, fldCreateBY,ORC_DOC_NO, TRACKING_NO) 
                                VALUES(cast('" + srcDt.Rows[i][2].ToString() + @"' as date),'" + CUT_DT.Rows[0][0].ToString() + @"',
'" + srcDt.Rows[i][6].ToString() + "','" + unitSearch_DT.Rows[0][0].ToString() + "','" + srcDt.Rows[i][4].ToString() + "','" + unitSearch_DT.Rows[0][0].ToString() + "','" + srcDt.Rows[i][4].ToString() + "','01/01/1900 09:00:00','" + srcDt.Rows[i][7].ToString() + "','" + srcDt.Rows[i][8].ToString() + "','0','Orcl','" + srcDt.Rows[i][0].ToString() + "','" + srcDt.Rows[i][1].ToString() + "')";
                            cls.executeSQL(SQL);
                            PreDocNo = srcDt.Rows[i][0].ToString();
                        }
                    }
                    MessageBox.Show("Finishing Added");
                    dgvORCL.DataSource = null;

                }

            }

            private void btnPolySearch_Click(object sender, EventArgs e)
            {
                string SearchKey = "";
                if (cmbUnit.Text == "" && txtTrackingNo.Text == "")
                {
                    return;
                }
                if (cmbUnit.Text != "" && txtTrackingNo.Text != "")
                {
                    SearchKey = " unit = '" + cmbUnit.Text + @"' And TRACKING_NO='" + txtTrackingNo.Text + @"' ";
                }
                if (cmbUnit.Text != "" && txtTrackingNo.Text == "")
                {
                    SearchKey = " unit = '" + cmbUnit.Text + @"' ";
                }
                if (cmbUnit.Text == "" && txtTrackingNo.Text != "")
                {
                    SearchKey = " TRACKING_NO='" + txtTrackingNo.Text + @"'  ";
                }

                string strOrcl = @"SELECT HEADER_NO,TRACKING_NO,DOC_DATE,unit,Line,FP_PIECE_NAME, COLOR,     
         FLDHOUR, fldSize,fldOutput From(SELECT HEADER_NO,TRACKING_NO,
         TO_CHAR(Prod_H.DOC_DATE,'MM/DD/YYYY')DOC_DATE,
        Prod_L.LINE_NO unit, 
        ''Line, 
         FP_PIECE_NAME,
         COLOR,
         fldSize,
         FLDHOUR,
         SUM (fldOutput)     fldOutput
    FROM (
           SELECT LINE_ID, SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 09:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R1 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R1 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 10:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R2 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R2 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 11:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R3 fldOutput
          FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R3 IS NOT NULL
          UNION ALL
          SELECT LINE_ID, SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 12:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R4 fldOutput
          FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R4 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 13:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R5 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R5 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 15:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R7 fldOutput
            FROM PWC_MFG_FINISH_PROD_D
           WHERE 1 = 1 AND R7 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 16:00:00', 'DD-Mon-YYYY hh24:mi:ss') fldHour,R8 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R8 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 17:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R9 fldOutput 
          FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R9 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 18:00:00', 'DD-Mon-YYYY hh24:mi:ss') fldHour,R10 fldOutput 
          FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R10 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 19:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R11 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R11 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 20:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R12 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R12 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 21:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R13 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R13 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 22:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour, R14 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R14 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 23:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R15 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R15 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 23:50:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R16 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R16 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 01:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R17 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R17 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 02:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R18 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R18 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE  fldSize,TO_DATE ('01-Jan-1900 03:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour, R19 fldOutput
          FROM PWC_MFG_FINISH_PROD_D
           WHERE 1 = 1 AND R19 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 04:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R20 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R20 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 05:00:00', 'DD-Mon-YYYY hh24:mi:ss') fldHour, R21 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R21 IS NOT NULL
          UNION ALL
          SELECT LINE_ID, SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 06:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,  R22  fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R22 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 07:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R1  fldOutput
            FROM PWC_MFG_FINISH_PROD_D  WHERE 1 = 1 AND R23 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 08:00:00', 'DD-Mon-YYYY hh24:mi:ss') fldHour,R1  fldOutput
          FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R24 IS NOT NULL
          
          )Prod_D,
         PWC_MFG_FINISH_PROD_H Prod_H,
         PWC_MFG_FINISH_PROD_L Prod_L
   WHERE     Prod_H.HEADER_ID = Prod_L.HEADER_ID
         AND Prod_L.LINE_ID = Prod_D.LINE_ID
         And TRUNC (Prod_H.DOC_DATE) = trunc(to_date('" + dtpIputdate.Text + @"', 'DD-Mon-YYYY')) 

GROUP BY TRACKING_NO,
         HEADER_NO,
         DOC_DATE,
         ATTRIBUTE1,
         LINE_NO,
         FP_PIECE_NAME,
         COLOR,
         fldSize,
         fldHour)A Where " + SearchKey + @" 
Order by TRACKING_NO,HEADER_NO,Line,fldSize";
                DataTable srcDt = cls.getDataTableOrcl(strOrcl);
                dgvORCL.DataSource = srcDt;
            }

            private void btnTrnsPoly_Click(object sender, EventArgs e)
            {
                string Challan = "", ChallanNo = "", checkDocNo = "", checkCut = "", PreDocNo = "", unitSearch;
                DataTable CUT_DT = null, unitSearch_DT = null;

                string SQLCheckBack = @"Select dateAdd(dd," + CheckBackDate + @",Cast(Getdate() as date))SysDate,'" + dtpIputdate.Text + @"' ProdDate
Where dateAdd(dd," + CheckBackDate + @",Cast(Getdate() as date))<='" + dtpIputdate.Text + @"'";
                DataTable DtBack = cls.getDataTable(SQLCheckBack);
                if (DtBack.Rows.Count == 0)
                {
                    MessageBox.Show("This date data are not allow to transfer", "Error");
                    return;
                }

                string SearchKey = "";
                if (cmbUnit.Text == "" && txtTrackingNo.Text == "")
                {
                    return;
                }
                if (cmbUnit.Text != "" && txtTrackingNo.Text != "")
                {
                    SearchKey = " unit = '" + cmbUnit.Text + @"' And TRACKING_NO='" + txtTrackingNo.Text + @"' ";
                }
                if (cmbUnit.Text != "" && txtTrackingNo.Text == "")
                {
                    SearchKey = " unit = '" + cmbUnit.Text + @"' ";
                }
                if (cmbUnit.Text == "" && txtTrackingNo.Text != "")
                {
                    SearchKey = " TRACKING_NO='" + txtTrackingNo.Text + @"'  ";
                }

                string strOrcl = @" SELECT HEADER_NO,TRACKING_NO,DOC_DATE,unit,Line,FP_PIECE_NAME, COLOR,     
         FLDHOUR, fldSize,fldOutput From(SELECT HEADER_NO,TRACKING_NO,
         TO_CHAR(Prod_H.DOC_DATE,'MM/DD/YYYY')DOC_DATE,
        Prod_L.LINE_NO unit, 
        ''Line, 
         FP_PIECE_NAME,
         COLOR,
         fldSize,
         FLDHOUR,
         SUM (fldOutput)     fldOutput
    FROM (
           SELECT LINE_ID, SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 09:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R1 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R1 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 10:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R2 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R2 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 11:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R3 fldOutput
          FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R3 IS NOT NULL
          UNION ALL
          SELECT LINE_ID, SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 12:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R4 fldOutput
          FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R4 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 13:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R5 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R5 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 15:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R7 fldOutput
            FROM PWC_MFG_FINISH_PROD_D
           WHERE 1 = 1 AND R7 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 16:00:00', 'DD-Mon-YYYY hh24:mi:ss') fldHour,R8 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R8 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 17:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R9 fldOutput 
          FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R9 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 18:00:00', 'DD-Mon-YYYY hh24:mi:ss') fldHour,R10 fldOutput 
          FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R10 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 19:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R11 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R11 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 20:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R12 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R12 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 21:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R13 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R13 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 22:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour, R14 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R14 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 23:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R15 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R15 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 23:50:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R16 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R16 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 01:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R17 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R17 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 02:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R18 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R18 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE  fldSize,TO_DATE ('01-Jan-1900 03:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour, R19 fldOutput
          FROM PWC_MFG_FINISH_PROD_D
           WHERE 1 = 1 AND R19 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 04:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R20 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R20 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 05:00:00', 'DD-Mon-YYYY hh24:mi:ss') fldHour, R21 fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R21 IS NOT NULL
          UNION ALL
          SELECT LINE_ID, SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 06:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,  R22  fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R22 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 07:00:00', 'DD-Mon-YYYY hh24:mi:ss')fldHour,R1  fldOutput
            FROM PWC_MFG_FINISH_PROD_D  WHERE 1 = 1 AND R23 IS NOT NULL
          UNION ALL
          SELECT LINE_ID,SIZE_VALUE fldSize,TO_DATE ('01-Jan-1900 08:00:00', 'DD-Mon-YYYY hh24:mi:ss') fldHour,R1  fldOutput
          FROM PWC_MFG_FINISH_PROD_D WHERE 1 = 1 AND R24 IS NOT NULL
          
          )Prod_D,
         PWC_MFG_FINISH_PROD_H Prod_H,
         PWC_MFG_FINISH_PROD_L Prod_L
   WHERE     Prod_H.HEADER_ID = Prod_L.HEADER_ID
         AND Prod_L.LINE_ID = Prod_D.LINE_ID
         And TRUNC (Prod_H.DOC_DATE) = trunc(to_date('" + dtpIputdate.Text + @"', 'DD-Mon-YYYY')) 

GROUP BY TRACKING_NO,
         HEADER_NO,
         DOC_DATE,
         ATTRIBUTE1,
         LINE_NO,
         FP_PIECE_NAME,
         COLOR,
         fldSize,
         fldHour)A Where " + SearchKey + @" 
Order by TRACKING_NO,HEADER_NO,Line,fldSize";
                DataTable srcDt = cls.getDataTableOrcl(strOrcl);


                if (srcDt.Rows.Count > 0)
                {


                    PreDocNo = srcDt.Rows[0][0].ToString();
                    string PreDocC = "";

                    unitSearch = @"SELECT Top 1 Priority, UnitName From (SELECT  Priority, UnitName=case when cuttingUnit='MFL' then 'MFL-Old' else cuttingUnit End  FROM     tblCompanyUnit
Where CompType='sewing')A Where UnitName='" + cmbUnit.Text + @"'";
                    unitSearch_DT = cls.getDataTable(unitSearch);


                    for (int i = 0; i < srcDt.Rows.Count; i++)
                    {


                        if (txtTrackingNo.Text != "" && PreDocC != srcDt.Rows[i][0].ToString())
                        {

                            string checkExistCut_Delete = "Delete From  tblDailyPolySizeWise Where ORC_DOC_NO='" + srcDt.Rows[i][0].ToString() + "'";
                            cls.executeSQL(checkExistCut_Delete);
                            PreDocC = srcDt.Rows[i][0].ToString();
                        }
                        string checkExistCut_str = "Select ORC_DOC_NO,TRACKING_NO From tblDailyPolySizeWise Where isnull(ORC_DOC_NO,'')='" + srcDt.Rows[i][0].ToString() + "' And fldDeletedBy is null";
                        DataTable checkExistCut_DT = cls.getDataTable(checkExistCut_str);
                        if (checkExistCut_DT.Rows.Count == 0)
                        {
                            //ChallanNo = cmbUnit.Text + "-SIN/" + GetMaxInputReqNo();
                            checkCut = @"Select FABRIC_REQ_NO+' '+B.fldstname+'::'+PIECE_NAME PRODUCT_NAME From (Select Distinct BUYERNAME,TRACKING_NUMBER,FABRIC_REQ_NO,PIECE_NAME From
(Select BUYERNAME,TRACKING_NUMBER,FABRIC_REQ_NO,'" + srcDt.Rows[i][5].ToString() +@"'PIECE_NAME 
From tbl_PWC_FRS_HEADERS where  BOOKINGTYPE='RMG Bulk Fabric Order' And TRACKING_NUMBER='" + srcDt.Rows[i][1].ToString() + @"'
Union ALL
Select BUYER_NAME,TRACKING_NO,FABRIC_REQ_NO,'" + srcDt.Rows[i][5].ToString() + "'PIECE_NAME  From tbl_PWC_CUT_HEAD Where TRACKING_NO='" + srcDt.Rows[i][1].ToString() + @"')A)H
                                     left outer join tblBuyer B on H.BUYERNAME=B.ERPName";
                            CUT_DT = cls.getDataTable(checkCut);
                            if (CUT_DT.Rows.Count < 1)
                            {
                                MessageBox.Show("Cut NO " + srcDt.Rows[i][4].ToString() + " Tracking No " + srcDt.Rows[i][8].ToString() + " Need to cutting Itegareted in MIS");
                                return;
                            }




                            string SQL = @"INSERT INTO tblDailyPolySizeWise (fldpolyDate, PRODUCT_NAME, fldColour, fldOutputFloor, fldPolyFloor, fldSize, fldPoly, fldDisplay, fldCreateBY,fldHour,ORC_DOC_NO, TRACKING_NO) 
                                VALUES(cast('" + srcDt.Rows[i][2].ToString() + @"' as date),'" + CUT_DT.Rows[0][0].ToString() + @"',
'" + srcDt.Rows[i][6].ToString() + "','" + unitSearch_DT.Rows[0][0].ToString() + "','" + unitSearch_DT.Rows[0][0].ToString() + "','" + srcDt.Rows[i][8].ToString() + "','" + srcDt.Rows[i][9].ToString() + "','0','Orcl','" + srcDt.Rows[i][7].ToString() + "','" + srcDt.Rows[i][0].ToString() + "','" + srcDt.Rows[i][1].ToString() + "')";
                            cls.executeSQL(SQL);
                            PreDocNo = srcDt.Rows[i][0].ToString();

                        }
                        else if (PreDocNo == srcDt.Rows[i][0].ToString() && CUT_DT != null)
                        {
                            checkCut = @"Select FABRIC_REQ_NO+' '+B.fldstname+'::'+PIECE_NAME PRODUCT_NAME From (Select Distinct BUYERNAME,TRACKING_NUMBER,FABRIC_REQ_NO,PIECE_NAME From
(Select BUYERNAME,TRACKING_NUMBER,FABRIC_REQ_NO,'" + srcDt.Rows[i][5].ToString() + @"'PIECE_NAME 
From tbl_PWC_FRS_HEADERS where  BOOKINGTYPE='RMG Bulk Fabric Order' And TRACKING_NUMBER='" + srcDt.Rows[i][1].ToString() + @"'
Union ALL
Select BUYER_NAME,TRACKING_NO,FABRIC_REQ_NO,'" + srcDt.Rows[i][5].ToString() + "'PIECE_NAME  From tbl_PWC_CUT_HEAD Where TRACKING_NO='" + srcDt.Rows[i][1].ToString() + @"')A)H
                                     left outer join tblBuyer B on H.BUYERNAME=B.ERPName";
                            CUT_DT = cls.getDataTable(checkCut);
                                 


                            string SQL = @"INSERT INTO tblDailyPolySizeWise (fldpolyDate, PRODUCT_NAME, fldColour, fldOutputFloor, fldPolyFloor, fldSize, fldPoly, fldDisplay, fldCreateBY,fldHour,ORC_DOC_NO, TRACKING_NO) 
                                VALUES(cast('" + srcDt.Rows[i][2].ToString() + @"' as date),'" + CUT_DT.Rows[0][0].ToString() + @"',
'" + srcDt.Rows[i][6].ToString() + "','" + unitSearch_DT.Rows[0][0].ToString() + "','" + unitSearch_DT.Rows[0][0].ToString() + "','" + srcDt.Rows[i][8].ToString() + "','" + srcDt.Rows[i][9].ToString() + "','0','Orcl','" + srcDt.Rows[i][7].ToString() + "','" + srcDt.Rows[i][0].ToString() + "','" + srcDt.Rows[i][1].ToString() + "')";
                            cls.executeSQL(SQL);
                            PreDocNo = srcDt.Rows[i][0].ToString();
                        }
                    }
                    MessageBox.Show("Finishing Added");
                    dgvORCL.DataSource = null;

                }
            }

            private void FrmInputTransfer_Load(object sender, EventArgs e)
            {
                CheckBackDate = -365;
            }

            private void btnSearchSewRej_Click(object sender, EventArgs e)
            {
                string SearchKey = "";
                if (cmbUnit.Text == "" && txtTrackingNo.Text == "")
                {
                    return;
                }
                if (cmbUnit.Text != "" && txtTrackingNo.Text != "")
                {
                    SearchKey = " Unit = '" + cmbUnit.Text + @"' And TRACKING_NO='" + txtTrackingNo.Text + @"' ";
                }
                if (cmbUnit.Text != "" && txtTrackingNo.Text == "")
                {
                    SearchKey = " Unit = '" + cmbUnit.Text + @"' ";
                }
                if (cmbUnit.Text == "" && txtTrackingNo.Text != "")
                {
                    SearchKey = " TRACKING_NO='" + txtTrackingNo.Text + @"'  ";
                }



                string strOrcl = @"Select ProdDate,DOC_NO,unit,Line,TRACKING_NO,PIECE_NAME,COLOR,fldSize,fldOutput From (Select to_char(OUTREC_DATE, 'DD-Mon-YYYY')ProdDate,Output_H.DOC_NO,
reverse(substr(reverse(Output_L.SUBLINE_NO),INSTR(reverse(Output_L.SUBLINE_NO),'-',1,1)+1,length(Output_L.SUBLINE_NO))) unit, 
reverse(substr(reverse(Output_L.SUBLINE_NO),0,INSTR(reverse(Output_L.SUBLINE_NO),'-',1,1)-1)) Line,
Output_L.SUBLINE_NO,Output_L.OUTPUT_LINE_NO,Output_L.TRACKING_NO,Output_L.PIECE_NAME,Output_L.COLOR,
fldSize,fldOutput,Output_D.OUT_SUBLINE_ID From 
(Select OUT_SUBLINE_ID,fldSize,sum(REJECTION)fldOutput From 
(Select OUT_SUBLINE_ID,SIZE_VAL fldSize,REJECTION From PWC_MFG_LINEOUTPUT_DETAIL
Where nvl(REJECTION,0)>0

)A group by OUT_SUBLINE_ID,fldSize)Output_D,
(Select DIVISION_ID,PROD_TRANS_ID,OUT_SUBLINE_ID,SUBLINE_NO,OUTPUT_LINE_NO,TRACKING_NO,PIECE_NAME,COLOR,JOB_ID From PWC_MFG_LINEOUTPUT_SBLINE)Output_L,
PWC_MFG_LINEOUTPUT Output_H

Where Output_D.OUT_SUBLINE_ID=Output_L.OUT_SUBLINE_ID
And Output_L.PROD_TRANS_ID=Output_H.PROD_TRANS_ID)A Where ProdDate=TRUNC (to_date('" + dtpIputdate.Text + "')) And  " + SearchKey + " order by DOC_NO,unit,Line";


                DataTable srcDt = cls.getDataTableOrcl(strOrcl);

                dgvORCL.DataSource = srcDt;
            }

            private void btnTRNSewRej_Click(object sender, EventArgs e)
            {
                string Challan = "", ChallanNo = "", checkDocNo = "", checkCut = "", PreDocNo = "", unitSearch;
                DataTable CUT_DT = null, unitSearch_DT = null;

                string SQLCheckBack = @"Select dateAdd(dd," + CheckBackDate + @",Cast(Getdate() as date))SysDate,'" + dtpIputdate.Text + @"' ProdDate
Where dateAdd(dd," + CheckBackDate + @",Cast(Getdate() as date))<='" + dtpIputdate.Text + @"'";
                DataTable DtBack = cls.getDataTable(SQLCheckBack);
                if (DtBack.Rows.Count == 0)
                {
                    MessageBox.Show("This date data are not allow to transfer", "Error");
                    return;
                }

                string SearchKey = "";
                if (cmbUnit.Text == "")
                {
                    return;
                }
                if (cmbUnit.Text != "")
                {
                    SearchKey = " unit = '" + cmbUnit.Text + @"' ";
                }
                //if (cmbUnit.Text == "" && txtTrackingNo.Text != "")
                //{
                //    SearchKey = " TRACKING_NO='" + txtTrackingNo.Text + @"'  ";
                //}

                string strOrcl = @"Select ProdDate,DOC_NO,unit,Line,TRACKING_NO,PIECE_NAME,COLOR,fldSize,fldOutput From (Select to_char(OUTREC_DATE, 'DD-Mon-YYYY')ProdDate,Output_H.DOC_NO,
reverse(substr(reverse(Output_L.SUBLINE_NO),INSTR(reverse(Output_L.SUBLINE_NO),'-',1,1)+1,length(Output_L.SUBLINE_NO))) unit, 
reverse(substr(reverse(Output_L.SUBLINE_NO),0,INSTR(reverse(Output_L.SUBLINE_NO),'-',1,1)-1)) Line,
Output_L.SUBLINE_NO,Output_L.OUTPUT_LINE_NO,Output_L.TRACKING_NO,Output_L.PIECE_NAME,Output_L.COLOR,
fldSize,fldOutput,Output_D.OUT_SUBLINE_ID From 
(Select OUT_SUBLINE_ID,fldSize,sum(REJECTION)fldOutput From 
(Select OUT_SUBLINE_ID,SIZE_VAL fldSize,REJECTION From PWC_MFG_LINEOUTPUT_DETAIL
Where nvl(REJECTION,0)>0

)A group by OUT_SUBLINE_ID,fldSize)Output_D,
(Select DIVISION_ID,PROD_TRANS_ID,OUT_SUBLINE_ID,SUBLINE_NO,OUTPUT_LINE_NO,TRACKING_NO,PIECE_NAME,COLOR,JOB_ID From PWC_MFG_LINEOUTPUT_SBLINE)Output_L,
PWC_MFG_LINEOUTPUT Output_H

Where Output_D.OUT_SUBLINE_ID=Output_L.OUT_SUBLINE_ID
And Output_L.PROD_TRANS_ID=Output_H.PROD_TRANS_ID)A Where ProdDate=TRUNC (to_date('" + dtpIputdate.Text + "')) And  " + SearchKey + " order by DOC_NO,unit,Line";
                DataTable srcDt = cls.getDataTableOrcl(strOrcl);


                if (srcDt.Rows.Count > 0)
                {


                    PreDocNo = srcDt.Rows[0][1].ToString();
                    string PreDocC = "";

                    unitSearch = @"SELECT Top 1 Priority, UnitName From (SELECT  Priority, UnitName=case when cuttingUnit='MFL' then 'MFL-Old' else cuttingUnit End  FROM     tblCompanyUnit
Where CompType='sewing')A Where UnitName='" + cmbUnit.Text + @"'";
                    unitSearch_DT = cls.getDataTable(unitSearch);


                    for (int i = 0; i < srcDt.Rows.Count; i++)
                    {


                        if (txtTrackingNo.Text != "" && PreDocC != srcDt.Rows[i][0].ToString())
                        {

                            string checkExistCut_Delete = "Delete From  tbl_Wastag Where Date='" + dtpIputdate.Text + "' And Unit='" + unitSearch_DT.Rows[0][1].ToString() + "'";
                            cls.executeSQL(checkExistCut_Delete);
                            PreDocC = srcDt.Rows[i][0].ToString();
                        }
                        string checkExistCut_str = "Select Orcl_Sew_Rej_Doc,TRACKING_NO From tbl_Wastag Where  Date='" + dtpIputdate.Text + "' And Unit='" + unitSearch_DT.Rows[0][1].ToString() + "' And isnull(Orcl_Sew_Rej_Doc,'')='" + srcDt.Rows[i][1].ToString() + "' And DeleteBy is null";
                        DataTable checkExistCut_DT = cls.getDataTable(checkExistCut_str);
                        if (checkExistCut_DT.Rows.Count == 0)
                        {
                            //ChallanNo = cmbUnit.Text + "-SIN/" + GetMaxInputReqNo();
                            checkCut = @"Select FABRIC_REQ_NO+' '+B.fldstname+'::'+PIECE_NAME PRODUCT_NAME From (Select Distinct BUYERNAME,TRACKING_NUMBER,FABRIC_REQ_NO,PIECE_NAME From
(Select BUYERNAME,TRACKING_NUMBER,FABRIC_REQ_NO,'" + srcDt.Rows[i][5].ToString() + @"'PIECE_NAME 
From tbl_PWC_FRS_HEADERS where  BOOKINGTYPE='RMG Bulk Fabric Order' And TRACKING_NUMBER='" + srcDt.Rows[i][4].ToString() + @"'
Union ALL
Select BUYER_NAME,TRACKING_NO,FABRIC_REQ_NO,'" + srcDt.Rows[i][5].ToString() + "'PIECE_NAME  From tbl_PWC_CUT_HEAD Where TRACKING_NO='" + srcDt.Rows[i][4].ToString() + @"')A)H
                                     left outer join tblBuyer B on H.BUYERNAME=B.ERPName";
                            CUT_DT = cls.getDataTable(checkCut);
                            if (CUT_DT.Rows.Count < 1)
                            {
                                MessageBox.Show("FRS Not enttred in MIS");
                                return;
                            }




                            string SQL = @"INSERT INTO tbl_Wastag (Date, WastagType, Unit, Line, OrderNo, Color, WastagQty, Size, RejType, InsertBy, Orcl_Sew_Rej_Doc, TRACKING_NO) 
                                VALUES(cast('" + srcDt.Rows[i][0].ToString() + @"' as date),'Sewing','" + unitSearch_DT.Rows[0][1].ToString() + "','" + srcDt.Rows[i][3].ToString() + "','" + CUT_DT.Rows[0][0].ToString() + @"',
'" + srcDt.Rows[i][6].ToString() + "','" + srcDt.Rows[i][8].ToString() + "','" + srcDt.Rows[i][7].ToString() + "','Challan Qty','Orcl','" + srcDt.Rows[i][1].ToString() + "','" + srcDt.Rows[i][4].ToString() + "')";
                            cls.executeSQL(SQL);
                            PreDocNo = srcDt.Rows[i][1].ToString();
                        }
                        else if (PreDocNo == srcDt.Rows[i][1].ToString() && CUT_DT != null)
                        {
                            //ChallanNo = checkExistCut_DT.Rows[0][1].ToString();
                            checkCut = @"Select FABRIC_REQ_NO+' '+B.fldstname+'::'+PIECE_NAME PRODUCT_NAME From (Select Distinct BUYERNAME,TRACKING_NUMBER,FABRIC_REQ_NO,PIECE_NAME From
(Select BUYERNAME,TRACKING_NUMBER,FABRIC_REQ_NO,'" + srcDt.Rows[i][5].ToString() + @"'PIECE_NAME 
From tbl_PWC_FRS_HEADERS where  BOOKINGTYPE='RMG Bulk Fabric Order' And TRACKING_NUMBER='" + srcDt.Rows[i][4].ToString() + @"'
Union ALL
Select BUYER_NAME,TRACKING_NO,FABRIC_REQ_NO,'" + srcDt.Rows[i][5].ToString() + "'PIECE_NAME  From tbl_PWC_CUT_HEAD Where TRACKING_NO='" + srcDt.Rows[i][4].ToString() + @"')A)H
                                     left outer join tblBuyer B on H.BUYERNAME=B.ERPName";
                            CUT_DT = cls.getDataTable(checkCut);


                            string SQL = @"INSERT INTO tbl_Wastag (Date, WastagType, Unit, Line, OrderNo, Color, WastagQty, Size, RejType, InsertBy, Orcl_Sew_Rej_Doc, TRACKING_NO) 
                                VALUES(cast('" + srcDt.Rows[i][0].ToString() + @"' as date),'Sewing','" + unitSearch_DT.Rows[0][1].ToString() + "','" + srcDt.Rows[i][3].ToString() + "','" + CUT_DT.Rows[0][0].ToString() + @"',
'" + srcDt.Rows[i][6].ToString() + "','" + srcDt.Rows[i][8].ToString() + "','" + srcDt.Rows[i][7].ToString() + "','Challan Qty','Orcl','" + srcDt.Rows[i][1].ToString() + "','" + srcDt.Rows[i][4].ToString() + "')";
                            cls.executeSQL(SQL);
                            PreDocNo = srcDt.Rows[i][1].ToString();
                        }
                    }
                    MessageBox.Show("Sew Rejecte Added");
                    dgvORCL.DataSource = null;

                }
            }

            private void btnSearchFinRej_Click(object sender, EventArgs e)
            {


                string SearchKey = "";
                if (cmbUnit.Text == "" && txtTrackingNo.Text == "")
                {
                    return;
                }
                if (cmbUnit.Text != "" && txtTrackingNo.Text != "")
                {
                    SearchKey = " unit = '" + cmbUnit.Text + @"' And TRACKING_NO='" + txtTrackingNo.Text + @"' ";
                }
                if (cmbUnit.Text != "" && txtTrackingNo.Text == "")
                {
                    SearchKey = " unit = '" + cmbUnit.Text + @"' ";
                }
                if (cmbUnit.Text == "" && txtTrackingNo.Text != "")
                {
                    SearchKey = " TRACKING_NO='" + txtTrackingNo.Text + @"'  ";
                }

                string strOrcl = @"SELECT HEADER_NO,TRACKING_NO,DOC_DATE,unit,Line,FP_PIECE_NAME, COLOR, fldSize,fldOutput From(SELECT HEADER_NO,TRACKING_NO,
         TO_CHAR(Prod_H.DOC_DATE,'MM/DD/YYYY')DOC_DATE,
        Prod_L.LINE_NO unit, 
        ''Line, 
         FP_PIECE_NAME,
         COLOR,
         fldSize,

         SUM (fldOutput)     fldOutput
    FROM (
SELECT LINE_ID, SIZE_VALUE fldSize,DAY_REJECTION fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE nvl(DAY_REJECTION,0)>0 
          
          )Prod_D,
         PWC_MFG_FINISH_PROD_H Prod_H,
         PWC_MFG_FINISH_PROD_L Prod_L
   WHERE     Prod_H.HEADER_ID = Prod_L.HEADER_ID
         AND Prod_L.LINE_ID = Prod_D.LINE_ID
         And TRUNC (Prod_H.DOC_DATE) = trunc(to_date('" + dtpIputdate.Text + @"', 'DD-Mon-YYYY')) 

GROUP BY TRACKING_NO,
         HEADER_NO,
         DOC_DATE,
         ATTRIBUTE1,
         LINE_NO,
         FP_PIECE_NAME,
         COLOR,
         fldSize)A Where " + SearchKey + @" 
Order by TRACKING_NO,HEADER_NO,Line,fldSize";
                DataTable srcDt = cls.getDataTableOrcl(strOrcl);
                dgvORCL.DataSource = srcDt;
            }

            private void button4_Click(object sender, EventArgs e)
            {
                string Challan = "", ChallanNo = "", checkDocNo = "", checkCut = "", PreDocNo = "", unitSearch;
                DataTable CUT_DT = null, unitSearch_DT = null;

                string SQLCheckBack = @"Select dateAdd(dd," + CheckBackDate + @",Cast(Getdate() as date))SysDate,'" + dtpIputdate.Text + @"' ProdDate
Where dateAdd(dd," + CheckBackDate + @",Cast(Getdate() as date))<='" + dtpIputdate.Text + @"'";
                DataTable DtBack = cls.getDataTable(SQLCheckBack);
                if (DtBack.Rows.Count == 0)
                {
                    MessageBox.Show("This date data are not allow to transfer", "Error");
                    return;
                }

                string SearchKey = "";
                if (cmbUnit.Text == "" && txtTrackingNo.Text == "")
                {
                    return;
                }
                if (cmbUnit.Text != "")
                {
                    SearchKey = " unit = '" + cmbUnit.Text + @"' ";
                }

                string strOrcl = @" SELECT HEADER_NO,TRACKING_NO,DOC_DATE,unit,Line,FP_PIECE_NAME, COLOR,     
         fldSize,fldOutput From(SELECT HEADER_NO,TRACKING_NO,
         TO_CHAR(Prod_H.DOC_DATE,'MM/DD/YYYY')DOC_DATE,
        Prod_L.LINE_NO unit, 
        ''Line, 
         FP_PIECE_NAME,
         COLOR,
         fldSize,
         SUM (fldOutput)     fldOutput
    FROM (
         SELECT LINE_ID, SIZE_VALUE fldSize,DAY_REJECTION fldOutput
            FROM PWC_MFG_FINISH_PROD_D WHERE nvl(DAY_REJECTION,0)>0          
          )Prod_D,
         PWC_MFG_FINISH_PROD_H Prod_H,
         PWC_MFG_FINISH_PROD_L Prod_L
   WHERE     Prod_H.HEADER_ID = Prod_L.HEADER_ID
         AND Prod_L.LINE_ID = Prod_D.LINE_ID
         And TRUNC (Prod_H.DOC_DATE) = trunc(to_date('" + dtpIputdate.Text + @"', 'DD-Mon-YYYY')) 

GROUP BY TRACKING_NO,
         HEADER_NO,
         DOC_DATE,
         ATTRIBUTE1,
         LINE_NO,
         FP_PIECE_NAME,
         COLOR,
         fldSize)A Where " + SearchKey + @" 
Order by TRACKING_NO,HEADER_NO,Line,fldSize";
                DataTable srcDt = cls.getDataTableOrcl(strOrcl);


                if (srcDt.Rows.Count > 0)
                {


                    PreDocNo = srcDt.Rows[0][0].ToString();
                    string PreDocC = "";

                    unitSearch = @"SELECT Top 1 Priority, UnitName From (SELECT  Priority, UnitName=case when cuttingUnit='MFL' then 'MFL-Old' else cuttingUnit End  FROM     tblCompanyUnit
Where CompType='sewing')A Where UnitName='" + cmbUnit.Text + @"'";
                    unitSearch_DT = cls.getDataTable(unitSearch);


                    for (int i = 0; i < srcDt.Rows.Count; i++)
                    {


                        if (txtTrackingNo.Text != "" && PreDocC != srcDt.Rows[i][0].ToString())
                        {

                            string checkExistCut_Delete = "Delete From  tbl_Wastag Where Date='" + dtpIputdate.Text + "' And Unit='" + unitSearch_DT.Rows[0][1].ToString() + "'";
                            cls.executeSQL(checkExistCut_Delete);
                            PreDocC = srcDt.Rows[i][0].ToString();
                        }
                        string checkExistCut_str = "Select Orcl_Sew_Rej_Doc,TRACKING_NO From tbl_Wastag Where isnull(Orcl_Sew_Rej_Doc,'')='" + srcDt.Rows[i][0].ToString() + "' And DeleteBy is null";
                        DataTable checkExistCut_DT = cls.getDataTable(checkExistCut_str);
                        if (checkExistCut_DT.Rows.Count == 0)
                        {
                            //ChallanNo = cmbUnit.Text + "-SIN/" + GetMaxInputReqNo();
                            checkCut = @"Select FABRIC_REQ_NO+' '+B.fldstname+'::'+PIECE_NAME PRODUCT_NAME From (Select Distinct BUYERNAME,TRACKING_NUMBER,FABRIC_REQ_NO,PIECE_NAME From
(Select BUYERNAME,TRACKING_NUMBER,FABRIC_REQ_NO,'" + srcDt.Rows[i][5].ToString() + @"'PIECE_NAME 
From tbl_PWC_FRS_HEADERS where  BOOKINGTYPE='RMG Bulk Fabric Order' And TRACKING_NUMBER='" + srcDt.Rows[i][1].ToString() + @"'
Union ALL
Select BUYER_NAME,TRACKING_NO,FABRIC_REQ_NO,'" + srcDt.Rows[i][5].ToString() + "'PIECE_NAME  From tbl_PWC_CUT_HEAD Where TRACKING_NO='" + srcDt.Rows[i][1].ToString() + @"')A)H
                                     left outer join tblBuyer B on H.BUYERNAME=B.ERPName";
                            CUT_DT = cls.getDataTable(checkCut);
                            if (CUT_DT.Rows.Count < 1)
                            {
                                MessageBox.Show("FRS Not entred in MIS");
                                return;
                            }

                            string SQL = @"INSERT INTO tbl_Wastag (Date, WastagType, Unit, Line, OrderNo, Color, WastagQty, Size, RejType, InsertBy, Orcl_Sew_Rej_Doc, TRACKING_NO) 
                                VALUES(cast('" + srcDt.Rows[i][2].ToString() + @"' as date),'Finishing','" + unitSearch_DT.Rows[0][1].ToString() + "','" + srcDt.Rows[i][4].ToString() + "','" + CUT_DT.Rows[0][0].ToString() + @"',
'" + srcDt.Rows[i][6].ToString() + "','" + srcDt.Rows[i][8].ToString() + "','" + srcDt.Rows[i][7].ToString() + "','Challan Qty','Orcl','" + srcDt.Rows[i][0].ToString() + "','" + srcDt.Rows[i][1].ToString() + "')";
                            cls.executeSQL(SQL);
                            PreDocNo = srcDt.Rows[i][0].ToString();

                        }
                        else if (PreDocNo == srcDt.Rows[i][0].ToString() && CUT_DT != null)
                        {
                            //ChallanNo = checkExistCut_DT.Rows[0][1].ToString();
                            checkCut = @"Select FABRIC_REQ_NO+' '+B.fldstname+'::'+PIECE_NAME PRODUCT_NAME From (Select Distinct BUYERNAME,TRACKING_NUMBER,FABRIC_REQ_NO,PIECE_NAME From
(Select BUYERNAME,TRACKING_NUMBER,FABRIC_REQ_NO,'" + srcDt.Rows[i][5].ToString() + @"'PIECE_NAME 
From tbl_PWC_FRS_HEADERS where  BOOKINGTYPE='RMG Bulk Fabric Order' And TRACKING_NUMBER='" + srcDt.Rows[i][1].ToString() + @"'
Union ALL
Select BUYER_NAME,TRACKING_NO,FABRIC_REQ_NO,'" + srcDt.Rows[i][5].ToString() + "'PIECE_NAME  From tbl_PWC_CUT_HEAD Where TRACKING_NO='" + srcDt.Rows[i][1].ToString() + @"')A)H
                                     left outer join tblBuyer B on H.BUYERNAME=B.ERPName";
                            CUT_DT = cls.getDataTable(checkCut);
                            string SQL = @"INSERT INTO tbl_Wastag (Date, WastagType, Unit, Line, OrderNo, Color, WastagQty, Size, RejType, InsertBy, Orcl_Sew_Rej_Doc, TRACKING_NO) 
                                VALUES(cast('" + srcDt.Rows[i][2].ToString() + @"' as date),'Finishing','" + unitSearch_DT.Rows[0][1].ToString() + "','" + srcDt.Rows[i][4].ToString() + "','" + CUT_DT.Rows[0][0].ToString() + @"',
'" + srcDt.Rows[i][6].ToString() + "','" + srcDt.Rows[i][8].ToString() + "','" + srcDt.Rows[i][7].ToString() + "','Challan Qty','Orcl','" + srcDt.Rows[i][0].ToString() + "','" + srcDt.Rows[i][1].ToString() + "')";
                            cls.executeSQL(SQL);
                            PreDocNo = srcDt.Rows[i][0].ToString();
                        }
                    }
                    MessageBox.Show("Finishing REJ Added");
                    dgvORCL.DataSource = null;

                }
            }

            private void button1_Click(object sender, EventArgs e)
            {
                
                string conn_eShop = @"data source=192.168.1.251;database=DBL_Group;uid=Prod;password=DbLPr0dDB";
                SqlConnection conn = new SqlConnection(conn_eShop);

                string SQL = @"Select fldHpID,PRODUCT_NAME, fldColour,fldCompanyCode,fldLine,fldFRShipID 
FROM tblDailySeweingNew Where fldDate='" + dtpIputdate.Text + "' And OpType=0 And fldCompanyCode='" + cmbUnit.Text + @"'";
               DataTable SeTD= cls.getDataTable(SQL);
               for (int j = 0; j < SeTD.Rows.Count; ++j) 
               {
                   SqlCommand comm = new SqlCommand("LoadGMTFRStrip_New", conn);
                   comm.CommandType = CommandType.StoredProcedure;
                   comm.Parameters.AddWithValue("@order", SeTD.Rows[j][1].ToString());
                   comm.Parameters.AddWithValue("@color", SeTD.Rows[j][2].ToString());
                   comm.Parameters.AddWithValue("@unit", SeTD.Rows[j][3].ToString());
                   comm.Parameters.AddWithValue("@line", SeTD.Rows[j][4].ToString());

                   DataTable dt = new DataTable();
                   SqlDataAdapter da = new SqlDataAdapter(comm);
                   da.Fill(dt);

                   string SQL1 = @"Update tblDailySeweingNew SET fldFRShipID='" + dt.Rows[0][0].ToString() + "'  Where   fldHpID='" + SeTD.Rows[j][0].ToString() + "'";
                   cls.executeSQL(SQL1);


               }
               MessageBox.Show("OK");
            }
        
    }
}
