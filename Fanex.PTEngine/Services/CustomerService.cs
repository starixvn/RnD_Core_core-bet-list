// -----------------------------------------------------------------------
// <copyright file="CustomerSettingDataAccess.cs" company="Nexcel Solutions">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
namespace Fanex.PTEngine.Services
{
    using System;
    using System.Collections;
    using Fanex.Logging;
    using Fanex.PTEngine.BusinessObjects;
    using Fanex.PTEngine.DataAccess;

    /// <summary>
    /// Do action on Customer Setting
    /// </summary>
    public class CustomerService : ICustomerService
    {
        #region ICustomerService Members

        /// <summary>
        /// Init default value for add new super
        /// </summary>
        /// <returns>Return a ICustomer contains value default of super</returns>
        public ICustomer GetCustomerForAddNewSuper()
        {
            ICustomer customer = new Customer();
            customer.InitDefaultValue();
            try
            {
                //// Init for pt setting
                foreach (SportType sportType in customer.PtSetting.SportTypes)
                {
                    foreach (BetTypeGroupSet betTypeGroupSet in sportType.BetTypeGroupSets)
                    {
                        foreach (BetTypeGroup betTypeGroup in betTypeGroupSet.BetTypeGroups)
                        {
                            foreach (BetType betType in betTypeGroup.BetTypes)
                            {
                                betType.Pt.A4 = (decimal?)0.9;
                                betType.Pt.M4 = (decimal?)0.9;
                                betType.Pt.S4 = (decimal?)0.9;
                                betType.Pt.L4 = (decimal?)0.9;
                                betType.Pt.N4 = (decimal?)0;
                            }
                        }
                    }
                }
                //// Init for betting limit
                foreach (BettingLimitItem bettingLimitItem in customer.BettingLimit.BettingLimitItems)
                {
                    bettingLimitItem.MinBet = 0;
                    bettingLimitItem.MaxBet = 0;
                    bettingLimitItem.MaxPerMatch = 0;
                    if (bettingLimitItem.Id == Constant.NUMBER_GAME_SPORT_TYPE_ID)
                    {
                        bettingLimitItem.MaxPerBall = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                throw new System.MissingFieldException("Init Default Value is failed", ex);
            }

            return customer;
        }

        /// <summary>
        /// Add new supper
        /// </summary>
        /// <param name="super">Customer info</param>
        /// <param name="context">Context of Add New Super</param>
        /// <param name="superId">Id of super just inserted</param>
        /// <returns>Return a result contain error code and error message</returns>
        public Result AddNewSuper(ICustomer super, IRequestContext context, out int superId)
        {
            Result result = new Result();
            int tempSuperId = -1;
            try
            {
                super.Password = Fanex.Common.Cryptography.CfsEnCode(super.Password);
                Hashtable parameters = super.MapToSpParameters(context);
                parameters.Add("refcode1", string.Empty);
                ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
                result = customerDataAccess.Acc_CompanyInsSuper(context, parameters, out tempSuperId);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                throw new Exception("Add New Super is failed", ex);
            }

            superId = tempSuperId;
            if (result.ErrorCode == string.Empty && result.ErrorMessage == string.Empty)
            {
                result.ErrorCode = Constant.ERROR_CODE;
                result.ErrorMessage = Constant.ERROR_MESSAGE;
            }

            return result;
        }

        /// <summary>
        /// Company Update Position Taking for Super
        /// </summary>
        /// <param name="master">Information of Super</param>
        /// <param name="context">Context of Company Update Super</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        public Result CompanyUpdateSuper(ICustomer super, IRequestContext context)
        {
            Result result = new Result();
            try
            {
                Hashtable parameters = super.MapToSpParameters(context);
                ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
                result = customerDataAccess.Acc_CompanyUpdateSuper(parameters);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                throw new Exception("Company Update Position Taking for Super is failed", ex);
            }

            if (result.ErrorCode == string.Empty && result.ErrorMessage == string.Empty)
            {
                result.ErrorCode = Constant.ERROR_CODE;
                result.ErrorMessage = Constant.ERROR_MESSAGE;
            }

            return result;
        }

        /// <summary>
        /// Get Customer For Company Edit Super
        /// </summary>
        /// <param name="requestContext">The context of Company Edit Super</param>
        /// <returns>Return a ICustomer contains current value of Super</returns>
        public ICustomer GetCustomerForCompanyEditSuper(IRequestContext context)
        {
            ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
            return customerDataAccess.Acc_CustomerSelCEditCustomer(context);
        }

        /// <summary>
        /// Get Customer For AddNewMaster
        /// </summary>
        /// <param name="context">The context of Add new master</param>
        /// <returns>Return a ICustomer contains value default of Master</returns>
        public ICustomer GetCustomerForAddNewMaster(IRequestContext context)
        {
            ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
            return customerDataAccess.Acc_CustomerSelSAgentInfo(context);
        }

        /// <summary>
        /// Get Customer For SuperEditMaster
        /// </summary>
        /// <param name="requestContext">The context of Super Edit Master</param>
        /// <returns>Return a ICustomer contains current value of Master</returns>
        public ICustomer GetCustomerForSuperEditMaster(IRequestContext context)
        {
            ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
            return customerDataAccess.Acc_CustomerSelSEditCustomer(context);
        }

        /// <summary>
        /// Get Customer For SuperEditMember
        /// </summary>
        /// <param name="requestContext">The context of Super Edit Member</param>
        /// <returns>Return a ICustomer contains current value of Member</returns>
        public ICustomer GetCustomerForSuperEditMember(IRequestContext context)
        {
            ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
            return customerDataAccess.Acc_CustomerSelEditCustomer_SAg(context);
        }

        /// <summary>
        /// Get Customer For SuperEditMinPTMaster
        /// </summary>
        /// <param name="requestContext">The context of Super Edit Min PT Master</param>
        /// <returns>Return a ICustomer contains current value of Master</returns>
        public ICustomer GetCustomerForSuperEditMinPTMaster(IRequestContext context)
        {
            ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
            return customerDataAccess.Acc_CustomerSelComEditOverride_MAg(context);
        }

        /// <summary>
        /// Add New Master
        /// </summary>
        /// <param name="master">Information of Master</param>
        /// <param name="context">Context of Add New Master</param>
        /// <param name="masterId">Id output after inserted</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        public Result AddNewMaster(ICustomer master, IRequestContext context, out int masterId)
        {
            Result result = new Result();
            try
            {
                master.Password = Fanex.Common.Cryptography.CfsEnCode(master.Password);
                Hashtable parameters = master.MapToSpParameters(context);
                ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
                result = customerDataAccess.Acc_CustomerInsMember_SAg(parameters, out masterId);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                throw new Exception("Add New Master is failed", ex);
            }

            if (result.ErrorCode == string.Empty && result.ErrorMessage == string.Empty)
            {
                result.ErrorCode = Constant.ERROR_CODE;
                result.ErrorMessage = Constant.ERROR_MESSAGE;
            }

            return result;
        }

        /// <summary>
        /// Super Update Position Taking for Master
        /// </summary>
        /// <param name="master">Information of Master</param>
        /// <param name="context">Context of Master</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        public Result SuperUpdateMaster(ICustomer master, IRequestContext context, bool isUpdateAllMembers, decimal newPositionTakingValue)
        {
            Result result = new Result();
            try
            {
                Hashtable parameters = master.MapToSpParameters(context);
                parameters.Add("updateallmembers", isUpdateAllMembers);
                parameters.Add("newspositiontaking", newPositionTakingValue);
                ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
                result = customerDataAccess.Acc_CustomerUpdMaster_All(parameters);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                throw new Exception("Super Update Position Taking for Master is failed", ex);
            }

            if (result.ErrorCode == string.Empty && result.ErrorMessage == string.Empty)
            {
                result.ErrorCode = Constant.ERROR_CODE;
                result.ErrorMessage = Constant.ERROR_MESSAGE;
            }

            return result;
        }

        /// <summary>
        /// Super Update Multiple Position Taking for Masters
        /// </summary>
        /// <param name="master">Information of Master</param>
        /// <param name="context">Context of Master</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        public Result SuperUpdateMultipleMasters(ICustomer master, IRequestContext context, bool isUpdateAllMembers, decimal newPositionTakingValue, Hashtable updateParameters)
        {
            Result result = new Result();
            try
            {
                Hashtable parameters = master.MapToSpParameters(context);
                master.MapToSpUpdateMultipleParameters(ref parameters, updateParameters, true);
                parameters.Add("updateallmembers", isUpdateAllMembers);
                parameters.Add("newspositiontaking", newPositionTakingValue);

                parameters.Add("updatemultiple", 1);
                parameters.Add("updatesuspended", 0);
                parameters.Add("updateclosed", Convert.ToInt16(updateParameters[0]));
                parameters.Add("updatecommission", Convert.ToInt16(updateParameters[1]));

                ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
                result = customerDataAccess.Acc_CustomerUpdMaster_All(parameters);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                throw new Exception("Super Update Position Taking for Master is failed", ex);
            }

            if (result.ErrorCode == string.Empty && result.ErrorMessage == string.Empty)
            {
                result.ErrorCode = Constant.ERROR_CODE;
                result.ErrorMessage = Constant.ERROR_MESSAGE;
            }

            return result;
        }

        /// <summary>
        /// Super Update Position Taking for Member
        /// </summary>
        /// <param name="master">Information of Member</param>
        /// <param name="context">Context of Member</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        public Result SuperUpdateMember(ICustomer member, IRequestContext context)
        {
            Result result = new Result();
            try
            {
                Hashtable parameters = member.MapToSpParameters(context);
                parameters.Add("updateclosed", 1);
                parameters.Add("updatesuspended", 1);
                ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
                result = customerDataAccess.Acc_CustomerUpdMember_Super(parameters);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                throw new Exception("Super Update Position Taking for Member is failed", ex);
            }

            if (result.ErrorCode == string.Empty && result.ErrorMessage == string.Empty)
            {
                result.ErrorCode = Constant.ERROR_CODE;
                result.ErrorMessage = Constant.ERROR_MESSAGE;
            }

            return result;
        }

        /// <summary>
        /// Super Update Multiple Position Taking for Members
        /// </summary>
        /// <param name="master">Information of Members</param>
        /// <param name="context">Context of Members</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        public Result SuperUpdateMultipleMembers(ICustomer member, IRequestContext context, Hashtable updateParameters)
        {
            Result result = new Result();
            try
            {
                Hashtable parameters = member.MapToSpParameters(context);
                member.MapToSpUpdateMultipleParameters(ref parameters, updateParameters, false);
                parameters.Add("updatemultiple", 1);
                parameters.Add("updatesuspended", 0);
                parameters.Add("updateclosed", Convert.ToInt16(updateParameters[0]));

                ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
                result = customerDataAccess.Acc_CustomerUpdMember_Super(parameters);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                throw new Exception("Super Update Multiple Position Taking for Members is failed", ex);
            }

            if (result.ErrorCode == string.Empty && result.ErrorMessage == string.Empty)
            {
                result.ErrorCode = Constant.ERROR_CODE;
                result.ErrorMessage = Constant.ERROR_MESSAGE;
            }

            return result;
        }

        /// <summary>
        /// Super Update Min Position Taking for Master
        /// </summary>
        /// <param name="master">Information of Master</param>
        /// <param name="context">Context of Master</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        public Result SuperUpdateMinPTMaster(ICustomer master, IRequestContext context)
        {
            Result result = new Result();
            try
            {
                Hashtable parameters = master.MapToSpParameters(context);
                parameters.Add("updateminpos", 1);
                parameters.Add("updatemaxpos", 0);
                ICustomerDataAccess customerDataAccess = new CustomerDataAccess();

                result = customerDataAccess.Acc_CustOverrideUpd(parameters);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                throw new Exception("Super Update Min Position Taking for Master is failed", ex);
            }

            if (result.ErrorCode == string.Empty && result.ErrorMessage == string.Empty)
            {
                result.ErrorCode = Constant.ERROR_CODE;
                result.ErrorMessage = Constant.ERROR_MESSAGE;
            }

            return result;
        }

        /// <summary>
        /// Master Update Position Taking for Member
        /// </summary>
        /// <param name="master">Information of Member</param>
        /// <param name="context">Context of Member</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        public Result MasterUpdateMember(ICustomer member, IRequestContext context)
        {
            Result result = new Result();
            try
            {
                Hashtable parameters = member.MapToSpParameters(context);
                parameters.Add("updateclosed", 1);
                parameters.Add("updatesuspended", 1);

                ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
                result = customerDataAccess.Acc_CustomerUpdMember_Master(parameters);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                throw new Exception("Master Update Position Taking for Member is failed", ex);
            }

            if (result.ErrorCode == string.Empty && result.ErrorMessage == string.Empty)
            {
                result.ErrorCode = Constant.ERROR_CODE;
                result.ErrorMessage = Constant.ERROR_MESSAGE;
            }

            return result;
        }

        /// <summary>
        /// Master Update Multiple Position Taking for Members
        /// </summary>
        /// <param name="master">Information of Members</param>
        /// <param name="context">Context of Members</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        public Result MasterUpdateMultipleMembers(ICustomer member, IRequestContext context, Hashtable updateParameters)
        {
            Result result = new Result();
            try
            {
                Hashtable parameters = member.MapToSpParameters(context);
                member.MapToSpUpdateMultipleParameters(ref parameters, updateParameters, false);
                parameters.Add("updatemultiple", 1);
                parameters.Add("updatesuspended", 0);
                parameters.Add("updateclosed", Convert.ToInt16(updateParameters[0]));

                ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
                result = customerDataAccess.Acc_CustomerUpdMember_Master(parameters);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                throw new Exception("Master Update Multiple Position Taking for Members is failed", ex);
            }

            if (result.ErrorCode == string.Empty && result.ErrorMessage == string.Empty)
            {
                result.ErrorCode = Constant.ERROR_CODE;
                result.ErrorMessage = Constant.ERROR_MESSAGE;
            }

            return result;
        }

        /// <summary>
        /// Get Customer For MasterEditMinPTAgent
        /// </summary>
        /// <param name="requestContext">The context of Master Edit Min PT Agent</param>
        /// <returns>Return a ICustomer contains current value of Agent</returns>
        public ICustomer GetCustomerForMasterEditMinPTAgent(IRequestContext context)
        {
            ICustomerDataAccess customerDataAccess = new CustomerDataAccess();

            return customerDataAccess.Acc_CustomerSelComEditOverride_Ag(context);
        }

        /// <summary>
        /// Get Customer For AddNewAgent
        /// </summary>
        /// <param name="context">The context of Add new agent</param>
        /// <returns>Return a ICustomer contains value default of Agent</returns>
        public ICustomer GetCustomerForAddNewAgent(IRequestContext context)
        {
            ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
            return customerDataAccess.Acc_CustomerSelMAgentInfo(context);
        }

        /// <summary>
        /// Get Customer For MasterEditAgent
        /// </summary>
        /// <param name="requestContext">The context of Master Edit Agent</param>
        /// <returns>Return a ICustomer contains current value of Agent</returns>
        public ICustomer GetCustomerForMasterEditAgent(IRequestContext context)
        {
            ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
            return customerDataAccess.Acc_CustomerSelMEditCustomer(context);
        }
        
        /// <summary>
        /// Get Customer For MasterEditMember
        /// </summary>
        /// <param name="requestContext">The context of Master Edit Member</param>
        /// <returns>Return a ICustomer contains current value of Member</returns>
        public ICustomer GetCustomerForMasterEditMember(IRequestContext context)
        {
            ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
            return customerDataAccess.Acc_CustomerSelEditCustomer_MAg(context);
        }

        /// <summary>
        /// Add New Agent
        /// </summary>
        /// <param name="master">Information of Agent</param>
        /// <param name="context">Context of Add New Agent</param>
        /// <param name="masterId">Id output after inserted</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        public Result AddNewAgent(ICustomer agent, IRequestContext context, out int agentId)
        {
            Result result = new Result();
            try
            {
                agent.Password = Fanex.Common.Cryptography.CfsEnCode(agent.Password);
                Hashtable parameters = agent.MapToSpParameters(context);
                ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
                result = customerDataAccess.Acc_CustomerInsMember_MAg(parameters, out agentId);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                throw new Exception("Add New Agent is failed", ex);
            }

            if (result.ErrorCode == string.Empty && result.ErrorMessage == string.Empty)
            {
                result.ErrorCode = Constant.ERROR_CODE;
                result.ErrorMessage = Constant.ERROR_MESSAGE;
            }

            return result;
        }

        /// <summary>
        /// Master Update Position Taking for Agent
        /// </summary>
        /// <param name="master">Information of Agent</param>
        /// <param name="context">Context of Agent</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        public Result MasterUpdateAgent(ICustomer agent, IRequestContext context, bool isUpdateAllMembers, decimal newPositionTakingValue)
        {
            Result result = new Result();
            try
            {
                Hashtable parameters = agent.MapToSpParameters(context);
                parameters.Add("updateallmembers", isUpdateAllMembers);
                parameters.Add("newmpositiontaking", newPositionTakingValue);
                ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
                result = customerDataAccess.Acc_CustomerUpdAgent_All(parameters);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                throw new Exception("Master Update Position Taking for Agent is failed", ex);
            }

            if (result.ErrorCode == string.Empty && result.ErrorMessage == string.Empty)
            {
                result.ErrorCode = Constant.ERROR_CODE;
                result.ErrorMessage = Constant.ERROR_MESSAGE;
            }

            return result;
        }

        /// <summary>
        /// Master Update Multiple Position Taking for Agents
        /// </summary>
        /// <param name="master">Information of Agent</param>
        /// <param name="context">Context of Agent</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        public Result MasterUpdateMultipleAgents(ICustomer agent, IRequestContext context, bool isUpdateAllMembers, decimal newPositionTakingValue, Hashtable updateParameters)
        {
            Result result = new Result();
            try
            {
                Hashtable parameters = agent.MapToSpParameters(context);
                agent.MapToSpUpdateMultipleParameters(ref parameters, updateParameters, true);

                parameters.Add("updateallmembers", isUpdateAllMembers);
                parameters.Add("newmpositiontaking", newPositionTakingValue);

                parameters.Add("updatemultiple", 1);
                parameters.Add("updatesuspended", 0);
                parameters.Add("updateclosed", Convert.ToInt16(updateParameters[0]));
                parameters.Add("updatecommission", Convert.ToInt16(updateParameters[1]));

                ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
                result = customerDataAccess.Acc_CustomerUpdAgent_All(parameters);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                throw new Exception("Master Update Position Taking for Agent is failed", ex);
            }

            if (result.ErrorCode == string.Empty && result.ErrorMessage == string.Empty)
            {
                result.ErrorCode = Constant.ERROR_CODE;
                result.ErrorMessage = Constant.ERROR_MESSAGE;
            }

            return result;
        }

        /// <summary>
        /// Master Update Min Position Taking for Agent
        /// </summary>
        /// <param name="master">Information of Agent</param>
        /// <param name="context">Context of Agent</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        public Result MasterUpdateMinPTAgent(ICustomer agent, IRequestContext context)
        {
            Result result = new Result();
            try
            {
                Hashtable parameters = agent.MapToSpParameters(context);
                parameters.Add("updateminpos", 1);
                parameters.Add("updatemaxpos", 0);
                ICustomerDataAccess customerDataAccess = new CustomerDataAccess();

                result = customerDataAccess.Acc_CustOverrideUpd(parameters);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                throw new Exception("Master Update Min Position Taking for Agent is failed", ex);
            }

            if (result.ErrorCode == string.Empty && result.ErrorMessage == string.Empty)
            {
                result.ErrorCode = Constant.ERROR_CODE;
                result.ErrorMessage = Constant.ERROR_MESSAGE;
            }

            return result;
        }

        /// <summary>
        /// Get Customer For AddNewMember
        /// </summary>
        /// <param name="context">The context of Add new member</param>
        /// <returns>Return a ICustomer contains value default of Member</returns>
        public ICustomer GetCustomerForAddNewMember(IRequestContext context)
        {
            ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
            return customerDataAccess.Acc_CustomerSelAgentInfo(context);
        }

        /// <summary>
        /// Get Customer For Update Member
        /// </summary>
        /// <param name="context">Context of Update Member</param>
        /// <returns>Return Customer info associate with context</returns>
        public ICustomer GetCustomerForAgentUpdateMember(IRequestContext context)
        {
            ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
            return customerDataAccess.Acc_CustomerSelEditCustomer(context);
        }

        /// <summary>
        /// Add New Member
        /// </summary>
        /// <param name="master">Information of Member</param>
        /// <param name="context">Context of Add New Member</param>
        /// <param name="masterId">Id output after inserted</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        public Result AddNewMember(ICustomer member, IRequestContext context, out int memberId)
        {
            Result result = new Result();
            try
            {
                member.Password = Fanex.Common.Cryptography.CfsEnCode(member.Password);
                Hashtable parameters = member.MapToSpParameters(context);
                ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
                result = customerDataAccess.Acc_CustomerInsMember_Ag(parameters, out memberId);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                throw new Exception("Add New Member is failed", ex);
            }

            if (result.ErrorCode == string.Empty && result.ErrorMessage == string.Empty)
            {
                result.ErrorCode = Constant.ERROR_CODE;
                result.ErrorMessage = Constant.ERROR_MESSAGE;
            }

            return result;
        }

        /// <summary>
        /// Agent Update Member
        /// </summary>
        /// <param name="member">Customer contains member information</param>
        /// <param name="context">context of agent update member</param>
        /// <returns>Return the result of update status</returns>
        public Result AgentUpdateMember(ICustomer member, IRequestContext context)
        {
            Result result = new Result();
            try
            {
                Hashtable parameters = member.MapToSpParameters(context);
                ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
                result = customerDataAccess.Acc_CustomerUpdMember_All(parameters);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                throw new Exception("Agent Update Member is failed", ex);
            }

            if (result.ErrorCode == string.Empty && result.ErrorMessage == string.Empty)
            {
                result.ErrorCode = Constant.ERROR_CODE;
                result.ErrorMessage = Constant.ERROR_MESSAGE;
            }

            return result;
        }

        /// <summary>
        /// Agent Update Multiple Members
        /// </summary>
        /// <param name="member">Customer contains member information</param>
        /// <param name="context">context of agent update multiple members</param>
        /// <returns>Return the result of update status</returns>
        public Result AgentUpdateMultipleMembers(ICustomer member, IRequestContext context, Hashtable updateParameters)
        {
            Result result = new Result();
            try
            {
                Hashtable parameters = member.MapToSpParameters(context);
                member.MapToSpUpdateMultipleParameters(ref parameters, updateParameters, true);
                parameters.Add("updatemultiple", 1);
                parameters.Add("updatesuspended", 0);
                parameters.Add("updateclosed", Convert.ToInt16(updateParameters[0]));
                parameters.Add("updatecommission", Convert.ToInt16(updateParameters[1]));

                ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
                result = customerDataAccess.Acc_CustomerUpdMember_All(parameters);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                throw new Exception("Agent Update Member is failed", ex);
            }

            if (result.ErrorCode == string.Empty && result.ErrorMessage == string.Empty)
            {
                result.ErrorCode = Constant.ERROR_CODE;
                result.ErrorMessage = Constant.ERROR_MESSAGE;
            }

            return result;
        }

        /// <summary>
        /// Get Transfer Condition
        /// </summary>
        /// <param name="context">Context of Transerfer</param>
        /// <returns>Return a tring</returns>
        public string GetTransferCondition(IRequestContext context)
        {
            ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
            return customerDataAccess.Acc_CustomerSelTransferOption(context);
        }


        /// <summary>
        /// Company Update Min/Max Position Taking for Master
        /// </summary>
        /// <param name="master">Information of Master</param>
        /// <param name="context">Context of Master</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        public Result CompanyUpdateMinMaxPTMaster(ICustomer master, IRequestContext context)
        {
            Result result = new Result();
            try
            {
                Hashtable parameters = master.MapToSpParameters(context);
                parameters.Add("updateminpos", 1);
                parameters.Add("updatemaxpos", 1);
                ICustomerDataAccess customerDataAccess = new CustomerDataAccess();

                result = customerDataAccess.Acc_CustOverrideUpd(parameters);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                throw new Exception("Company Update Min Position Taking for Master is failed", ex);
            }

            if (result.ErrorCode == string.Empty && result.ErrorMessage == string.Empty)
            {
                result.ErrorCode = Constant.ERROR_CODE;
                result.ErrorMessage = Constant.ERROR_MESSAGE;
            }

            return result;
        }

        /// <summary>
        /// Company Update Min/Max Position Taking for Agent
        /// </summary>
        /// <param name="master">Information of Agent</param>
        /// <param name="context">Context of Agent</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        public Result CompanyUpdateMinMaxPTAgent(ICustomer agent, IRequestContext context)
        {
            Result result = new Result();
            try
            {
                Hashtable parameters = agent.MapToSpParameters(context);
                parameters.Add("updateminpos", 1);
                parameters.Add("updatemaxpos", 1);
                ICustomerDataAccess customerDataAccess = new CustomerDataAccess();

                result = customerDataAccess.Acc_CustOverrideUpd(parameters);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                throw new Exception("Company Update Min Position Taking for Agent is failed", ex);
            }

            if (result.ErrorCode == string.Empty && result.ErrorMessage == string.Empty)
            {
                result.ErrorCode = Constant.ERROR_CODE;
                result.ErrorMessage = Constant.ERROR_MESSAGE;
            }

            return result;
        }

        /// <summary>
        /// Get Customer For CompanyEditMinPTSuper
        /// </summary>
        /// <param name="requestContext">The context of Company Edit Min PT Super</param>
        /// <returns>Return a ICustomer contains current value of Super</returns>
        public ICustomer GetCustomerForCompanyEditMinPTSuper(IRequestContext context)
        {
            ICustomerDataAccess customerDataAccess = new CustomerDataAccess();
            return customerDataAccess.Acc_CustomerSelComEditOverride_SAg(context);
        }

        /// <summary>
        /// Company Update Min Position Taking for Super
        /// </summary>
        /// <param name="master">Information of Super</param>
        /// <param name="context">Context of Super</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        public Result CompanyUpdateMinPTSuper(ICustomer super, IRequestContext context)
        {
            Result result = new Result();
            try
            {
                Hashtable parameters = super.MapToSpParameters(context);
                parameters.Add("updateminpos", 1);
                parameters.Add("updatemaxpos", 0);
                ICustomerDataAccess customerDataAccess = new CustomerDataAccess();

                result = customerDataAccess.Acc_CustOverrideUpd(parameters);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                throw new Exception("Company Update Min Position Taking for Super is failed", ex);
            }

            if (result.ErrorCode == string.Empty && result.ErrorMessage == string.Empty)
            {
                result.ErrorCode = Constant.ERROR_CODE;
                result.ErrorMessage = Constant.ERROR_MESSAGE;
            }

            return result;
        }
        #endregion ICustomerService Members   
    }
}
