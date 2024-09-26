namespace StudentPerfomance.Application.Queries.EducationPlans.FilterConstraints;

public class FilterConstraint
{
	private readonly string _constraint;
	public FilterConstraint(string constraint) => _constraint = constraint;
	public const string General = "GENERAL";
	public const string YearOnly = "YEAR";
	public const string DirectionOnly = "DIRECTION";
	public string Constraint => _constraint;
	public override string ToString() => _constraint;
}
