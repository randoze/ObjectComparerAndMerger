
using DtoTools;



ClassComparer.CompareClasses<Person, PersonDto>();

string mergedCode = ObjectMerger.MergeClasses<Person, PersonDto>("PersonMerged");
Console.WriteLine(mergedCode);
Console.ReadLine();