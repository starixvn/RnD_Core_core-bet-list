namespace Fanex.PTEngine.DataAccess
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    using Fanex.Data;
    using Fanex.Data.DNAide;
    using Fanex.PTEngine.BusinessObjects;

    public class CustomerDataAccess : DataAccessBase, ICustomerDataAccess
    {
        #region Get Customer Information

        /// <summary>
        /// Get Customer For AddNew Master
        /// </summary>
        /// <param name="context">The context of Add new Master</param>
        /// <returns>Return a ICustomer contains value default of Master</returns>
        public ICustomer Acc_CustomerSelSAgentInfo(IRequestContext requestContext)
        {
            IDataReader reader;
            ICustomer customer = new Customer();
            using (ObjectDb db = new ObjectDb("Acc_CustomerSelSAgentInfo", 120))
            {
                db.AddInParameter("custid", DbType.Int32, requestContext.CustId);
                reader = db.ExecuteReader();
                reader.Read();
                customer.ReadFromDataRecord(requestContext, reader);
            }

            return customer;
        }

        /// <summary>
        /// Get Customer For AddNew Agent
        /// </summary>
        /// <param name="context">The context of Add new Agent</param>
        /// <returns>Return a ICustomer contains value default of Agent</returns>
        public ICustomer Acc_CustomerSelMAgentInfo(IRequestContext requestContext)
        {
            IDataReader reader;
            ICustomer customer = new Customer();
            using (ObjectDb db = new ObjectDb("Acc_CustomerSelMAgentInfo", 120))
            {
                db.AddInParameter("custid", DbType.Int32, requestContext.CustId);
                reader = db.ExecuteReader();
                reader.Read();
                customer.ReadFromDataRecord(requestContext, reader);
            }

            return customer;
        }

        /// <summary>
        /// Get Customer For AddNew Member
        /// </summary>
        /// <param name="context">The context of Add new Memeber</param>
        /// <returns>Return a ICustomer contains value default of Member</returns>
        public ICustomer Acc_CustomerSelAgentInfo(IRequestContext requestContext)
        {
            IDataReader reader;
            ICustomer customer = new Customer();
            using (ObjectDb db = new ObjectDb("Acc_CustomerSelAgentInfo", 120))
            {
                db.AddInParameter("custid", DbType.Int32, requestContext.CustId);
                reader = db.ExecuteReader();
                reader.Read();
                customer.ReadFromDataRecord(requestContext, reader);
            }

            return customer;
        }

        /// <summary>
        /// Get Customer for Super Edit Master
        /// </summary>
        /// <param name="requestContext">The context of Super Edit Master</param>
        /// <returns>Return a ICustomer contains current value of Master</returns>
        public ICustomer Acc_CustomerSelSEditCustomer(IRequestContext requestContext)
        {
            IDataReader reader;
            ICustomer customer = new Customer();
            using (ObjectDb db = new ObjectDb("Acc_CustomerSelSEditCustomer", 120))
            {
                db.AddInParameter("custid", DbType.Int32, requestContext.TargetCustId);
                db.AddInParameter("srecommend", DbType.Int32, requestContext.CustId);
                reader = db.ExecuteReader();
                reader.Read();
                customer.ReadFromDataRecord(requestContext, reader);
            }

            return customer;
        }

        /// <summary>
        /// Get Customer for Super Edit Member
        /// </summary>
        /// <param name="requestContext">The context of Super Edit Member</param>
        /// <returns>Return a ICustomer contains current value of Member</returns>
        public ICustomer Acc_CustomerSelEditCustomer_SAg(IRequestContext requestContext)
        {
            IDataReader reader;
            ICustomer customer = new Customer();
            using (ObjectDb db = new ObjectDb("Acc_CustomerSelEditCustomer_SAg", 120))
            {
                db.AddInParameter("custid", DbType.Int32, requestContext.TargetCustId);
                db.AddInParameter("agentid", DbType.Int32, requestContext.AgentId);
                reader = db.ExecuteReader();
                reader.Read();
                customer.ReadFromDataRecord(requestContext, reader);
            }

            return customer;
        }

        /// <summary>
        /// Get Customer for Master Edit Agent
        /// </summary>
        /// <param name="requestContext">The context of Master Edit Agent</param>
        /// <returns>Return a ICustomer contains current value of Agent</returns>
        public ICustomer Acc_CustomerSelMEditCustomer(IRequestContext requestContext)
        {
            IDataReader reader;
            ICustomer customer = new Customer();
            using (ObjectDb db = new ObjectDb("Acc_CustomerSelMEditCustomer", 120))
            {
                db.AddInParameter("custid", DbType.Int32, requestContext.TargetCustId);
                db.AddInParameter("mrecommend", DbType.Int32, requestContext.CustId);
                reader = db.ExecuteReader();
                reader.Read();
                customer.ReadFromDataRecord(requestContext, reader);
            }

            return customer;
        }

        /// <summary>
        /// Get Customer for Master Edit Member
        /// </summary>
        /// <param name="requestContext">The context of Master Edit Member</param>
        /// <returns>Return a ICustomer contains current value of Member</returns>
        public ICustomer Acc_CustomerSelEditCustomer_MAg(IRequestContext requestContext)
        {
            IDataReader reader;
            ICustomer customer = new Customer();
            using (ObjectDb db = new ObjectDb("Acc_CustomerSelEditCustomer_MAg", 120))
            {
                db.AddInParameter("custid", DbType.Int32, requestContext.TargetCustId);
                db.AddInParameter("agentid", DbType.Int32, requestContext.AgentId);
                reader = db.ExecuteReader();
                reader.Read();
                customer.ReadFromDataRecord(requestContext, reader);
            }

            return customer;
        }

        /// <summary>
        /// Get Customer for Agent Edit Member
        /// </summary>
        /// <param name="requestContext">The context of Agent Edit Member</param>
        /// <returns>Return a ICustomer contains current value of Member</returns>
        public ICustomer Acc_CustomerSelEditCustomer(IRequestContext requestContext)
        {
            IDataReader reader;
            ICustomer customer = new Customer();
            using (ObjectDb db = new ObjectDb("Acc_CustomerSelEditCustomer", 120))
            {
                db.AddInParameter("custid", DbType.Int32, requestContext.TargetCustId);
                reader = db.ExecuteReader();
                reader.Read();
                customer.ReadFromDataRecord(requestContext, reader);
            }

            return customer;
        }

        #endregion Get Customer Information

        #region Super Edit Member: Get Information in Multiple Edit case

        public ICustomer Acc_CustomerSelSAgentInfo_Multiple(int custId, int masterId)
        {
            Customer cusInfo = new Customer();

            using (ObjectDb db = new ObjectDb("Acc_CustomerSelSAgentInfo_Multiple", 120))
            {
                cusInfo = db.QueryEntity<Customer>(new { custId = custId, mrecommend = masterId });
            }

            return cusInfo;
        }

        #endregion Super Edit Member: Get Information in Multiple Edit case

        #region Master Edit Member: Get Information in Multiple Edit case

        public ICustomer Acc_CustomerSelMAgentInfo_Multiple(int custId, int agentId, string arrCustId)
        {
            Customer cusInfo = new Customer();

            using (ObjectDb db = new ObjectDb("Acc_CustomerSelEditCustomer_MAg", 120))
            {
                cusInfo = db.QueryEntity<Customer>(new { custId = custId, recommend = agentId, strCustId = arrCustId });
            }

            return cusInfo;
        }

        #endregion Master Edit Member: Get Information in Multiple Edit case

        #region Get Commission

        public ICustomer Age_CustomerSelUserComm(int custId, int userId)
        {
            Customer cusInfo = new Customer();

            using (ObjectDb db = new ObjectDb("Age_CustomerSelUserComm", 120))
            {
                cusInfo = db.QueryEntity<Customer>(new { custId = custId, userId = userId });
            }

            return cusInfo;
        }

        #endregion Get Commission

        #region Get/Update Bet Setting

        public IBettingLimit<BettingLimitItem> Age_CustomerSelBetSetting(IRequestContext requestContext)
        {
            IDataReader reader;
            IBettingLimit<BettingLimitItem> bettingLimit = new BettingLimit();

            using (ObjectDb db = new ObjectDb("Age_CustomerSelBetSetting", 120))
            {
                db.AddInParameter("custid", DbType.Int32, requestContext.TargetCustId);
                db.AddInParameter("userid", DbType.Int32, requestContext.CustId);
                reader = db.ExecuteReader();
                reader.Read();
                bettingLimit.ReadFromDataRecord(requestContext, reader);
            }

            return bettingLimit;
        }

        /// <summary>
        /// Update Bet Setting of current user
        /// </summary>
        /// <param name="parameters">Context of user</param>
        /// <returns></returns>
        public Result Age_CustomerUpdMemBetSetting(Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Age_CustomerUpdMemBetSetting", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }

        #endregion Get/Update Bet Setting

        #region Get Transfer Condition

        public string Acc_CustomerSelTransferOption(IRequestContext requestContext)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("custid", requestContext.CustId);
            parameters.Add("transferoption", string.Empty);
            DbResult result = DbHelper.ExecuteDbResult("Acc_CustomerSelTransferOption", parameters);
            return result.Outputs["transferoption"].ToString();
        }

        #endregion Get Transfer Condition

        #region Super Get Min PT Master

        public ICustomer Acc_CustomerSelComEditOverride_MAg(IRequestContext requestContext)
        {
            IDataReader reader;
            ICustomer customer = new Customer();
            using (ObjectDb db = new ObjectDb("Acc_CustomerSelComEditOverride_MAg", 120))
            {
                db.AddInParameter("custid", DbType.Int32, requestContext.TargetCustId);
                reader = db.ExecuteReader();
                reader.Read();
                customer.ReadFromDataRecord(requestContext, reader);
            }

            return customer;
        }

        #endregion Super Get Min PT Master

        #region Master Get Min PT Agent

        public ICustomer Acc_CustomerSelComEditOverride_Ag(IRequestContext requestContext)
        {
            IDataReader reader;
            ICustomer customer = new Customer();
            using (ObjectDb db = new ObjectDb("Acc_CustomerSelComEditOverride_Ag", 120))
            {
                db.AddInParameter("custid", DbType.Int32, requestContext.TargetCustId);
                db.AddInParameter("mrecommend", DbType.Int32, requestContext.CustId);
                reader = db.ExecuteReader();
                reader.Read();
                customer.ReadFromDataRecord(requestContext, reader);
            }

            return customer;
        }

        #endregion Master Get Min PT Agent

        #region Insert New Customer

        /// <summary>
        /// Add New Master
        /// </summary>
        /// <param name="parameters">Context of Add New Master</param>
        /// <param name="masterId">Id output after inserted</param>
        /// <returns></returns>
        public Result Acc_CustomerInsMember_SAg(Hashtable parameters, out int masterId)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerInsMember_SAg"))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                masterId = (int)db.GetParameterValue("custid");
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }

        /// <summary>
        /// Add New Agent
        /// </summary>
        /// <param name="parameters">Context of Add New Agent</param>
        /// <param name="agentId">Id output after inserted</param>
        /// <returns></returns>
        public Result Acc_CustomerInsMember_MAg(Hashtable parameters, out int agentId)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerInsMember_MAg", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                agentId = (int)db.GetParameterValue("custid");
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }

        /// <summary>
        /// Add New Member
        /// </summary>
        /// <param name="parameters">Context of Add New Member</param>
        /// <param name="memberId">Id output after inserted</param>
        /// <returns></returns>
        public Result Acc_CustomerInsMember_Ag(Hashtable parameters, out int memberId)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerInsMember_Ag", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                memberId = (int)db.GetParameterValue("custid");
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }

        #endregion Insert New Customer

        #region Update Customer

        /// <summary>
        /// Super Update Position Taking for Master
        /// </summary>
        /// <param name="parameters">Context of Master</param>
        /// <returns></returns>
        public Result Acc_CustomerUpdMaster_All(Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerUpdMaster_All", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }

        /// <summary>
        /// Master Update Position Taking for Agent
        /// </summary>
        /// <param name="parameters">Context of Agent</param>
        /// <returns></returns>
        public Result Acc_CustomerUpdAgent_All(Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerUpdAgent_All", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }

        /// <summary>
        /// Agent Update Position Taking for Member
        /// </summary>
        /// <param name="parameters">Context of Member</param>
        /// <returns></returns>
        public Result Acc_CustomerUpdMember_All(Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerUpdMember_All", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }

        /// <summary>
        /// Super Update Position Taking for Member
        /// </summary>
        /// <param name="parameters">Context of Member</param>
        /// <returns></returns>
        public Result Acc_CustomerUpdMember_Super(Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerUpdMember_Super", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }

        /// <summary>
        /// Master Update Position Taking for Member
        /// </summary>
        /// <param name="parameters">Context of Member</param>
        /// <returns></returns>
        public Result Acc_CustomerUpdMember_Master(Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerUpdMember_Master", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }

        public Result Acc_CustomerUpdMaster_Member(IRequestContext requestContext, Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerUpdMaster_Member", 120))
            {
                this.AddResultParams(db.Database, db.DbCommand);
                db.ExecuteNonQuery(parameters);
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }

        public Result Acc_CustomerUpdEditCustomer_Ag(IRequestContext requestContext, Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerUpdEditCustomer_Ag", 120))
            {
                this.AddResultParams(db.Database, db.DbCommand);
                db.ExecuteNonQuery(parameters);
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }

        #endregion Update Customer

        #region Super Update Min PT Master / Master Update Min PT Agent

        public Result Acc_CustOverrideUpd(Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustOverrideUpd", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();

                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }

        #endregion Super Update Min PT Master / Master Update Min PT Agent

        #region Company Insert Super

        /// <summary>
        /// This function will take parameter and insert to database
        /// </summary>
        /// <param name="requestContext">Context of insert new super</param>
        /// <param name="parameters">The hashtable contains parameters</param>
        /// <param name="superId">Super id output after Insert</param>
        /// <returns>The result contain message and error code</returns>
        public Result Acc_CompanyInsSuper(IRequestContext requestContext, Hashtable parameters, out int superId)
        {
            using (ObjectDb db = new ObjectDb("CustomerSetting_InsertSuper"))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                superId = (int)db.GetParameterValue("custid");
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }

        #endregion Company Insert Super

        #region Company Edit Super

        /// <summary>
        /// Get Customer for Company Edit Super
        /// </summary>
        /// <param name="requestContext">The context of Company Edit Super</param>
        /// <returns>Return a ICustomer contains current value of Super</returns>
        public ICustomer Acc_CustomerSelCEditCustomer(IRequestContext requestContext)
        {
            IDataReader reader;
            ICustomer customer = new Customer();
            using (ObjectDb db = new ObjectDb("Acc_CustomerSelCEditCustomer", 120))
            {
                db.AddInParameter("custid", DbType.Int32, requestContext.TargetCustId);
                reader = db.ExecuteReader();
                reader.Read();
                customer.ReadFromDataRecord(requestContext, reader);
            }

            return customer;
        }

        /// <summary>
        /// Company update super
        /// </summary>
        /// <param name="requestContext">Context of Company update super</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Result Acc_CompanyUpdateSuper(Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerUpdSuper_All", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }

        #endregion Company Edit Super

        #region Company Get Min PT Super
        public ICustomer Acc_CustomerSelComEditOverride_SAg(IRequestContext requestContext)
        {
            IDataReader reader;
            ICustomer customer = new Customer();
            using (ObjectDb db = new ObjectDb("Acc_CustomerSelComEditOverride_SAg", 120))
            {
                db.AddInParameter("custid", DbType.Int32, requestContext.TargetCustId);                
                reader = db.ExecuteReader();
                reader.Read();
                customer.ReadFromDataRecord(requestContext, reader);
            }

            return customer;
        }
        #endregion Company Get Min PT Super
    }
}