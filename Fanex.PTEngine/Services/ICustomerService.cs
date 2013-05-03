namespace Fanex.PTEngine.Services
{
    using Fanex.PTEngine.BusinessObjects;
    using System.Collections;

    /// <summary>
    /// Do action on Customer Setting
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Init default value for add new super
        /// </summary>
        /// <returns>Return a ICustomer contains value default of super</returns>
        ICustomer GetCustomerForAddNewSuper();

        /// <summary>
        /// Add new supper
        /// </summary>
        /// <param name="super">Customer info</param>
        /// <param name="context">Context of Add New Super</param>
        /// <param name="superId">Id of super just inserted</param>
        /// <returns>Return a result contain error code and error message</returns>
        Result AddNewSuper(ICustomer super, IRequestContext context, out int superId);

        /// <summary>
        /// Company Edit Super
        /// </summary>
        /// <param name="super">Information of Super</param>
        /// <param name="context">Current context of Company edit Super</param>
        /// <returns></returns>
        Result CompanyUpdateSuper(ICustomer super, IRequestContext context);

        /// <summary>
        /// Get Customer For Company Edit Super
        /// </summary>
        /// <param name="requestContext">The context of Company Edit Super</param>
        /// <returns>Return a ICustomer contains current value of Super</returns>
        ICustomer GetCustomerForCompanyEditSuper(IRequestContext context);

        /// <summary>
        /// Get Customer For AddNewMaster
        /// </summary>
        /// <param name="context">The context of Add new master</param>
        /// <returns>Return a ICustomer contains value default of Master</returns>
        ICustomer GetCustomerForAddNewMaster(IRequestContext context);

        /// <summary>
        /// Get Customer For SuperEditMaster
        /// </summary>
        /// <param name="requestContext">The context of Super Edit Master</param>
        /// <returns>Return a ICustomer contains current value of Master</returns>
        ICustomer GetCustomerForSuperEditMaster(IRequestContext context);

        /// <summary>
        /// Get Customer For SuperEditMember
        /// </summary>
        /// <param name="requestContext">The context of Super Edit Member</param>
        /// <returns>Return a ICustomer contains current value of Member</returns>
        ICustomer GetCustomerForSuperEditMember(IRequestContext context);

        /// <summary>
        /// Get Customer For SuperEditMinPTMaster
        /// </summary>
        /// <param name="requestContext">The context of Super Edit Min PT Master</param>
        /// <returns>Return a ICustomer contains current value of Master</returns>
        ICustomer GetCustomerForSuperEditMinPTMaster(IRequestContext context);

        /// <summary>
        /// Add New Master
        /// </summary>
        /// <param name="master">Information of Master</param>
        /// <param name="context">Context of Add New Master</param>
        /// <param name="masterId">Id output after inserted</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        Result AddNewMaster(ICustomer master, IRequestContext context, out int masterId);

        /// <summary>
        /// Super Update Position Taking for Master
        /// </summary>
        /// <param name="master">Information of Master</param>
        /// <param name="context">Context of Master</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        Result SuperUpdateMaster(ICustomer master, IRequestContext context, bool isUpdateAllMembers, decimal newPositionTakingValue);

        /// <summary>
        /// Super Update Multiple Position Taking for Masters
        /// </summary>
        /// <param name="master">Information of Master</param>
        /// <param name="context">Context of Master</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        Result SuperUpdateMultipleMasters(ICustomer master, IRequestContext context, bool isUpdateAllMembers, decimal newPositionTakingValue, Hashtable updateParameters);

        /// <summary>
        /// Super Update Position Taking for Member
        /// </summary>
        /// <param name="master">Information of Member</param>
        /// <param name="context">Context of Member</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        Result SuperUpdateMember(ICustomer member, IRequestContext context);

        /// <summary>
        /// Super Update Multiple Position Taking for Members
        /// </summary>
        /// <param name="master">Information of Members</param>
        /// <param name="context">Context of Members</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        Result SuperUpdateMultipleMembers(ICustomer member, IRequestContext context, Hashtable updateParameters);

        /// <summary>
        /// Super Update Min Position Taking for Master
        /// </summary>
        /// <param name="master">Information of Master</param>
        /// <param name="context">Context of Master</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        Result SuperUpdateMinPTMaster(ICustomer master, IRequestContext context);

        /// <summary>
        /// Master Update Position Taking for Member
        /// </summary>
        /// <param name="master">Information of Member</param>
        /// <param name="context">Context of Member</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        Result MasterUpdateMember(ICustomer member, IRequestContext context);

        /// <summary>
        /// Master Update Multiple Position Taking for Members
        /// </summary>
        /// <param name="master">Information of Members</param>
        /// <param name="context">Context of Members</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        Result MasterUpdateMultipleMembers(ICustomer member, IRequestContext context, Hashtable updateParameters);

        /// <summary>
        /// Get Customer For MasterEditMinPTAgent
        /// </summary>
        /// <param name="requestContext">The context of Matser Edit Min PT Agent</param>
        /// <returns>Return a ICustomer contains current value of Agent</returns>
        ICustomer GetCustomerForMasterEditMinPTAgent(IRequestContext context);

        /// <summary>
        /// Get Customer For AddNewAgent
        /// </summary>
        /// <param name="context">The context of Add new agent</param>
        /// <returns>Return a ICustomer contains value default of Agent</returns>
        ICustomer GetCustomerForAddNewAgent(IRequestContext context);

        /// <summary>
        /// Get Customer For MasterEditAgent
        /// </summary>
        /// <param name="requestContext">The context of Master Edit Agent</param>
        /// <returns>Return a ICustomer contains current value of Agent</returns>
        ICustomer GetCustomerForMasterEditAgent(IRequestContext context);

        /// <summary>
        /// Master Update Min Position Taking for Agent
        /// </summary>
        /// <param name="master">Information of Agent</param>
        /// <param name="context">Context of Agent</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        Result MasterUpdateMinPTAgent(ICustomer agent, IRequestContext context);

        /// <summary>
        /// Get Customer For MasterEditMember
        /// </summary>
        /// <param name="requestContext">The context of Master Edit Member</param>
        /// <returns>Return a ICustomer contains current value of Member</returns>
        ICustomer GetCustomerForMasterEditMember(IRequestContext context);

        /// <summary>
        /// Add New Agent
        /// </summary>
        /// <param name="master">Information of Agent</param>
        /// <param name="context">Context of Add New Agent</param>
        /// <param name="masterId">Id output after inserted</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        Result AddNewAgent(ICustomer agent, IRequestContext context, out int agentId);

        /// <summary>
        /// Master Update Position Taking for Agent
        /// </summary>
        /// <param name="master">Information of Agent</param>
        /// <param name="context">Context of Agent</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        Result MasterUpdateAgent(ICustomer agent, IRequestContext context, bool isUpdateAllMembers, decimal newPositionTakingValue);

        /// <summary>
        /// Master Update Multiple Position Taking for Agents
        /// </summary>
        /// <param name="master">Information of Agent</param>
        /// <param name="context">Context of Agent</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        Result MasterUpdateMultipleAgents(ICustomer agent, IRequestContext context, bool isUpdateAllMembers, decimal newPositionTakingValue, Hashtable updateParameters);

        /// <summary>
        /// Get Customer For AddNewMember
        /// </summary>
        /// <param name="context">The context of Add new member</param>
        /// <returns>Return a ICustomer contains value default of Member</returns>
        ICustomer GetCustomerForAddNewMember(IRequestContext context);

        /// <summary>
        /// Get Customer For Update Member
        /// </summary>
        /// <param name="context">Context of Update Member</param>
        /// <returns>Return Customer info associate with context</returns>
        ICustomer GetCustomerForAgentUpdateMember(IRequestContext context);

        /// <summary>
        /// Add New Memeber
        /// </summary>
        /// <param name="master">Information of Member</param>
        /// <param name="context">Context of Add New Member</param>
        /// <param name="masterId">Id output after inserted</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        Result AddNewMember(ICustomer member, IRequestContext context, out int memberId);

        /// <summary>
        /// Agent Update Member
        /// </summary>
        /// <param name="member">Customer contains member information</param>
        /// <param name="context">context of agent update member</param>
        /// <returns>Return the result of update status</returns>
        Result AgentUpdateMember(ICustomer member, IRequestContext context);

        /// <summary>
        /// Agent Update Multiple Members
        /// </summary>
        /// <param name="member">Customer contains member information</param>
        /// <param name="context">context of agent update multiple members</param>
        /// <returns>Return the result of update status</returns>
        Result AgentUpdateMultipleMembers(ICustomer member, IRequestContext context, Hashtable updateParameters);

        /// <summary>
        /// Get Transfer Condition
        /// </summary>
        /// <param name="context">Context of Transerfer</param>
        /// <returns>Return a tring</returns>
        string GetTransferCondition(IRequestContext context);

        /// <summary>
        /// Company Update Min/Max Position Taking for Master
        /// </summary>
        /// <param name="master">Information of Master</param>
        /// <param name="context">Context of Master</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        Result CompanyUpdateMinMaxPTMaster(ICustomer master, IRequestContext context);

        /// <summary>
        /// Company Update Min/Max Position Taking for Agent
        /// </summary>
        /// <param name="master">Information of Agent</param>
        /// <param name="context">Context of Agent</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        Result CompanyUpdateMinMaxPTAgent(ICustomer agent, IRequestContext context);

        /// <summary>
        /// Get Customer For CompanyEditMinPTSuper
        /// </summary>
        /// <param name="requestContext">The context of Company Edit Min PT Super</param>
        /// <returns>Return a ICustomer contains current value of Super</returns>
        ICustomer GetCustomerForCompanyEditMinPTSuper(IRequestContext context);

        /// <summary>
        /// Company Update Min Position Taking for Super
        /// </summary>
        /// <param name="master">Information of Super</param>
        /// <param name="context">Context of Super</param>
        /// <returns>Return a Result with Error Code and Error Message</returns>
        Result CompanyUpdateMinPTSuper(ICustomer super, IRequestContext context);
    }
}
