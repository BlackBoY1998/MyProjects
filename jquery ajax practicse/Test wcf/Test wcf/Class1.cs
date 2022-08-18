using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

namespace Test_wcf
{
    [DataContract]
    public class Calc 
    {
        int FirstNo;
        public Calc()
        {
            FirstNo=0;
        }
        public Calc(int First,int second)
        {
            FirstNo = First;
        }
        [DataMember]
        public int First
        {
            get { return FirstNo;}
            set { FirstNo = value; }
        }
    }
    [ServiceContract]
    public interface ICalcArithmeticservice
    {
        [OperationContract]
        Calc Add(Calc p1, Calc p2);

        [OperationContract]
        Calc Sub(Calc p1, Calc p2);

        [OperationContract]
        Calc Mul(Calc p1, Calc p2);

    }
}