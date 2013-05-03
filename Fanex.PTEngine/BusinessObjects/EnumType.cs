namespace Fanex.PTEngine.BusinessObjects
{
    public enum MappingType
    {
        In,
        Out
    }

    public enum FormType : int
    {
        Add = 0,
        EditPT = 1,
        EditMinPT = 2,
        EditMinMaxPT = 3
    }

    public enum CustomerRole : int
    {
        Member = 1,
        Agent = 2,
        Master = 3,
        Super = 4
    }
}