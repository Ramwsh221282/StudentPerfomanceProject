namespace StudentPerfomance.Application.Queries.EducationDirections.FilterConstraints;

public class FilterConstraint
{
	private readonly string _constraint;
	public FilterConstraint(string constraint) => _constraint = constraint;
	public const string General = "GENERAL";
	public const string NameOnly = "NAME";
	public const string CodeOnly = "CODE";
	public string Constraint => _constraint;
	public override string ToString() => _constraint;
}
