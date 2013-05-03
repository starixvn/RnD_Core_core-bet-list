namespace Fanex.PTEngine.DataAccess
{
    using System.Collections;
    using System.Data;
    using Fanex.PTEngine.BusinessObjects;

    public interface ICustomerDataAccess
    {
        /// <summary>
        /// Get Customer For AddNew Master
        /// </summary>
        /// <param name="context">The context of Add new Master</param>
        /// <returns>Return a ICustomer contains value default of Master</returns>
        ICustomer Acc_CustomerSelSAgentInfo(IRequestContext requestContext);

        /// <summary>
        /// Get Customer For AddNew Agent
        /// </summary>
        /// <param name="context">The context of Add new Agent</param>
        /// <returns>Return a ICustomer contains value default of Agent</returns>
        ICustomer Acc_CustomerSelMAgentInfo(IRequestContext requestContext);

        /// <summary>
        /// Get Customer For AddNew Member
        /// </summary>
        /// <param name="context">The context of Add new Memeber</param>
        /// <returns>Return a ICustomer contains value default of Member</returns>
        ICustomer Acc_CustomerSelAgentInfo(IRequestContext requestContext);

        /// <summary>
        /// Get Customer for Super Edit Master
        /// </summary>
        /// <param name="requestContext">The context of Super Edit Master</param>
        /// <returns>Return a ICustomer contains current value of Master</returns>
        ICustomer Acc_CustomerSelSEditCustomer(IRequestContext requestContext);

        /// <summary>
        /// Get Customer for Super Edit Member
        /// </summary>
        /// <param name="requestContext">The context of Super Edit Member</param>
        /// <returns>Return a ICustomer contains current value of Member</returns>
        ICustomer Acc_CustomerSelEditCustomer_SAg(IRequestContext requestContext);

        /// <summary>
        /// Get Customer for Master Edit Agent
        /// </summary>
        /// <param name="requestContext">The context of Master Edit Agent</param>
        /// <returns>Return a ICustomer contains current value of Agent</returns>
        ICustomer Acc_CustomerSelMEditCustomer(IRequestContext requestContext);

        /// <summary>
        /// Get Customer for Master Edit Member
        /// </summary>
        /// <param name="requestContext">The context of Master Edit Member</param>
        /// <returns>Return a ICustomer contains current value of Member</returns>
        ICustomer Acc_CustomerSelEditCustomer_MAg(IRequestContext requestContext);

        ICustomer Acc_CustomerSelSAgentInfo_Multiple(int custId, int uplineId);

        ICustomer Acc_CustomerSelMAgentInfo_Multiple(int custId, int agentId, string custIds);

        /// <summary>
        /// Get Customer for Agent Edit Member
        /// </summary>
        /// <param name="requestContext">Context of Agent Edit Member</param>
        /// <returns>Customer Info</returns>
        ICustomer Acc_CustomerSelEditCustomer(IRequestContext requestContext);

        ICustomer Acc_CustomerSelComEditOverride_MAg(IRequestContext requestContext);

        ICustomer Acc_CustomerSelComEditOverride_Ag(IRequestContext requestContext);

        ICustomer Age_CustomerSelUserComm(int custId, int userId);

        /// <summary>
        /// Get Bet Setting of current user
        /// </summary>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        IBettingLimit<BettingLimitItem> Age_CustomerSelBetSetting(IRequestContext requestContext);

        /// <summary>
        /// Update Bet Setting of current user
        /// </summary>
        /// <param name="parameters">Context of user</param>
        /// <returns></returns>
        Result Age_CustomerUpdMemBetSetting(Hashtable parameters);

        string Acc_CustomerSelTransferOption(IRequestContext requestContext);

        /// <summary>
        /// Add new super
        /// </summary>
        /// <param name="requestContext">Context of Add new Super</param>
        /// <param name="parameters">Parameter contains data of new super</param>
        /// <param name="superId">Id return after insert successfully</param>
        /// <returns>Result contains error or success</returns>
        Result Acc_CompanyInsSuper(IRequestContext requestContext, Hashtable parameters, out int superId);

        /// <summary>
        /// Get Infor of Super need to be updated
        /// </summary>
        /// <param name="requestContext">Request of current context</param>
        /// <returns></returns>
        ICustomer Acc_CustomerSelCEditCustomer(IRequestContext requestContext);

        /// <summary>
        /// Add New Master
        /// </summary>
        /// <param name="parameters">Context of Add New Master</param>
        /// <param name="masterId">Id output after inserted</param>
        /// <returns></returns>
        Result Acc_CustomerInsMember_SAg(Hashtable parameters, out int masterId);

        /// <summary>
        /// Add New Agent
        /// </summary>
        /// <param name="parameters">Context of Add New Agent</param>
        /// <param name="agentId">Id output after inserted</param>
        /// <returns></returns>
        Result Acc_CustomerInsMember_MAg(Hashtable parameters, out int agentId);

        /// <summary>
        /// Add New Member
        /// </summary>
        /// <param name="parameters">Context of Add New Member</param>
        /// <param name="memberId">Id output after inserted</param>
        /// <returns></returns>
        Result Acc_CustomerInsMember_Ag(Hashtable parameters, out int memberId);

        /// <summary>
        /// Super Update Position Taking for Master
        /// </summary>
        /// <param name="parameters">Context of Master</param>
        /// <returns></returns>
        Result Acc_CustomerUpdMaster_All(Hashtable parameters);

        /// <summary>
        /// Master Update Position Taking for Agent
        /// </summary>
        /// <param name="parameters">Context of Agent</param>
        /// <returns></returns>
        Result Acc_CustomerUpdAgent_All(Hashtable parameters);

        /// <summary>
        /// Agent Update Position Taking for Member
        /// </summary>
        /// <param name="parameters">Context of Member</param>
        /// <returns></returns>
        Result Acc_CustomerUpdMember_All(Hashtable parameters);

        Result Acc_CompanyUpdateSuper(Hashtable parameters);

        /// <summary>
        /// Super Update Position Taking for Member
        /// </summary>
        /// <param name="parameters">Context of Member</param>
        /// <returns></returns>
        Result Acc_CustomerUpdMember_Super(Hashtable parameters);

        /// <summary>
        /// Master Update Position Taking for Member
        /// </summary>
        /// <param name="parameters">Context of Member</param>
        /// <returns></returns>
        Result Acc_CustomerUpdMember_Master(Hashtable parameters);

        Result Acc_CustomerUpdMaster_Member(IRequestContext requestContext, Hashtable parameters);

        Result Acc_CustomerUpdEditCustomer_Ag(IRequestContext requestContext, Hashtable parameters);

        Result Acc_CustOverrideUpd(Hashtable parameters);

        ICustomer Acc_CustomerSelComEditOverride_SAg(IRequestContext requestContext);
    }
}