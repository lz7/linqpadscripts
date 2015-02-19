<Query Kind="Program" />

void Main()
{
	var singers = ParseSingers();
	var states = GetAbbreviationsToStates();
	var byState = singers.GroupBy(s => s.State).OrderByDescending(g => g.Count());
	foreach (var stateGrouping in byState){
		string singerNames = String.Join(", ", stateGrouping.Select(s => s.Name));
		Console.Write("{0}--{1}; ", singerNames, states[stateGrouping.Key]);
	}
}

Dictionary<string, string> GetAbbreviationsToStates(){
	var dict = File.ReadAllLines(@"C:\Users\lz7\SkyDrive\states.txt").ToDictionary(l=>l.Split('\t')[1], l=>l.Split('\t')[0]);
	dict["OR/AL"] = "Oregon / Alabama";
	return dict;		
}

IList<Singer> ParseSingers(){
	String[] lines = File.ReadAllLines(@"C:\Users\lz7\SkyDrive\scratch.txt");
	IList<Singer> result = new List<Singer>();
	int cityCount = 0;
	Singer currSinger = new Singer();
	foreach (string l in lines)
	{
		switch (cityCount++ % 3){
			case 0:
				currSinger.Name = l.Trim();
				break;
			case 2:
				currSinger.State = l.Trim().Replace(".", "");
				result.Add(currSinger);
				currSinger = new Singer();
				break;
			default:
				break;
		}		
	}
	return result;
}

// Define other methods and classes here
public struct Singer {
	public string Name;
	public string State;
}