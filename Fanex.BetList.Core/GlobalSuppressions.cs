// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Major Code Smell", "S6566:Use \"DateTimeOffset\" instead of \"DateTime\"", Justification = "<Pending>")]
[assembly: SuppressMessage("Major Code Smell", "S6580:Use a format provider when parsing date and time", Justification = "<Pending>")]
[assembly: SuppressMessage("Critical Code Smell", "S3874:\"out\" and \"ref\" parameters should not be used", Justification = "<Pending>")]
[assembly: SuppressMessage("Blocker Code Smell", "S3427:Method overloads with default parameter values should not overlap", Justification = "<Pending>", Scope = "member", Target = "~M:Fanex.BetList.Core.Entities.ITicketHelper.GetBetTypeNameById(System.Object,System.Object,System.Object)~System.String")]
