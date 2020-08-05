using FreeSql.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;


namespace YH.EAM.Entity.CodeGenerator
{
    /// <summary>
    ///  
    ///</summary>
    public class   Tsdm_customer
    {

       public Tsdm_customer()
       {
      
       }

        ///<summary>
        ///描述：
        ///</summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        ///<summary>
        ///描述：公司名称（中文）
        ///</summary>
        public string Companychinese { get; set; }
        ///<summary>
        ///描述：公司名称（英文）
        ///</summary>
        public string Companyenglish { get; set; }
        ///<summary>
        ///描述：法人代表
        ///</summary>
        public string Thelegalrepresentative { get; set; }
        ///<summary>
        ///描述：注册地址
        ///</summary>
        public string Registeraddress { get; set; }
        ///<summary>
        ///描述：办公地址
        ///</summary>
        public string Officeaddress { get; set; }
        ///<summary>
        ///描述：订单联系人
        ///</summary>
        public string Ordercontact { get; set; }
        ///<summary>
        ///描述：联系电话1
        ///</summary>
        public string Telphone1 { get; set; }
        ///<summary>
        ///描述：邮政编码
        ///</summary>
        public string Zipcode { get; set; }
        ///<summary>
        ///描述：联系传真1
        ///</summary>
        public string Fax1 { get; set; }
        ///<summary>
        ///描述：收货地址
        ///</summary>
        public string Receiptaddress { get; set; }
        ///<summary>
        ///描述：收货联系人
        ///</summary>
        public string Receiptcontact { get; set; }
        ///<summary>
        ///描述：联系电话2
        ///</summary>
        public string Telphone2 { get; set; }
        ///<summary>
        ///描述：正常收货时间
        ///</summary>
        public string Receiptdate { get; set; }
        ///<summary>
        ///描述：联系传真2
        ///</summary>
        public string Fax2 { get; set; }
        ///<summary>
        ///描述：开票地址
        ///</summary>
        public string Billingaddress { get; set; }
        ///<summary>
        ///描述：开户银行
        ///</summary>
        public string Accountbank { get; set; }
        ///<summary>
        ///描述：银行账号
        ///</summary>
        public string Account { get; set; }
        ///<summary>
        ///描述：税号
        ///</summary>
        public string Taxnumber { get; set; }
        ///<summary>
        ///描述：是否一般纳税人资格
        ///</summary>
        public string Taxpayereligibility { get; set; }
        ///<summary>
        ///描述：商家所属省份
        ///</summary>
        public string Province { get; set; }
        ///<summary>
        ///描述：单体电芯型号
        ///</summary>
        public string Singlecellmodel { get; set; }
        ///<summary>
        ///描述：单体电芯容量
        ///</summary>
        public string Singlecellcapacity { get; set; }
        ///<summary>
        ///描述：规划产量
        ///</summary>
        public string Planyield { get; set; }
        ///<summary>
        ///描述：产品用途
        ///</summary>
        public string Productuse { get; set; }
        ///<summary>
        ///描述：材料体系
        ///</summary>
        public string Materialssys { get; set; }
        ///<summary>
        ///描述：合浆方式和周期
        ///</summary>
        public string Hejiangway { get; set; }
        ///<summary>
        ///描述：打胶需求
        ///</summary>
        public string Glueneed { get; set; }
        ///<summary>
        ///描述：浆料粘度及固含量
        ///</summary>
        public string Theviscosityandsolidcontentoftheslurry { get; set; }
        ///<summary>
        ///描述：涂布需求宽度
        ///</summary>
        public long Coatingrequireswidth { get; set; }
        ///<summary>
        ///描述：铜、铝箔材厚度
        ///</summary>
        public long Foilthickness { get; set; }
        ///<summary>
        ///描述：正负极涂覆干厚度、面密度
        ///</summary>
        public long Coatdrythickness { get; set; }
        ///<summary>
        ///描述：涂布方式
        ///</summary>
        public string Coating { get; set; }
        ///<summary>
        ///描述：辊轧方式
        ///</summary>
        public string Rollway { get; set; }
        ///<summary>
        ///描述：极耳分布方式
        ///</summary>
        public string Polareardistribution { get; set; }
        ///<summary>
        ///描述：正极片尺寸
        ///</summary>
        public string Positivesize { get; set; }
        ///<summary>
        ///描述：负极片尺寸
        ///</summary>
        public string Negativesize { get; set; }
        ///<summary>
        ///描述：正、负极耳材质
        ///</summary>
        public string Polarearmaterial { get; set; }
        ///<summary>
        ///描述：正、负极耳层数
        ///</summary>
        public int Polarearlayers { get; set; }
        ///<summary>
        ///描述：电芯装配流程
        ///</summary>
        public string Cellassembly { get; set; }
        ///<summary>
        ///描述：烘烤时间
        ///</summary>
        public int Bakingtime { get; set; }
        ///<summary>
        ///描述：注液量
        ///</summary>
        public long Fluidinjection { get; set; }
        ///<summary>
        ///描述：化成方式
        ///</summary>
        public string Hucheng { get; set; }
        ///<summary>
        ///描述：老化时间
        ///</summary>
        public long Agingtime { get; set; }
        ///<summary>
        ///描述：常温静置时间
        ///</summary>
        public long Ambienttemperaturetime { get; set; }
        ///<summary>
        ///描述：分容时间
        ///</summary>
        public long Splittime { get; set; }
        ///<summary>
        ///描述：分选档次要求
        ///</summary>
        public string Sortgrade { get; set; }
        ///<summary>
        ///描述：厂房平面底图
        ///</summary>
        public string Plant { get; set; }
        ///<summary>
        ///描述：项目里程碑
        ///</summary>
        public DateTime Projectstage { get; set; }
        ///<summary>
        ///描述：整线或工序段
        ///</summary>
        public string Processstage { get; set; }
        ///<summary>
        ///描述：其他
        ///</summary>
        public string Remark { get; set; }

        ///<summary>
        ///描述：其他
        ///</summary>
        public DateTime Createtime { get; set; }
    }
 }








