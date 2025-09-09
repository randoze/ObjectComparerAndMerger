
using DtoTools;

ClassComparer.CompareClasses<AssetTaggerContracts2.DTOs.DTOLocationView, AssetTaggerContracts.DTOs.DTOLocationView>();

string mergedCode = ObjectMerger.MergeClasses<AssetTaggerContracts2.DTOs.DTOLocationView, AssetTaggerContracts.DTOs.DTOLocationView>("DTOLocationView");
Console.WriteLine(mergedCode);
Console.ReadLine();