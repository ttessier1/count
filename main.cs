using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

/*

	count.exe [-w|--words] [-d|--distinct] [-c|--characters] [-h|--histogram] [-l|--lines] [[-f|--file] <filespec>] [-r|--recursive] [[-l|--lan] <en|fr|it|es>] message
		-w|--words - count words
		-d|--distinct - count distinct words
		-c|--characters - count characters
		-h|--histogram - histogram of characters
		-l|--lines - count lines
		-f|--file <filespec> - the file path or glob to work on 
		-r|--recursive - operate recursively
		-l|--lang <en|fr|it|es>
		[message] - a message to operate on, if files are specified, this should not be
		
*/

// TODO: add author block
// TODO: complete process function
// TODO: calculate length of lines
// TODO: calculate length of all lines
// TODO: calculate number of words per line
// TODO: calculate number of words of all lines
// TODO: calculate distinct number of wordsper line
// TODO: calculate distinct number of words of all lines
// TODO: calculate length of longest line
// TODO: calculate length of shortest line
// TODO: calculate greatest number of words in a line
// TODO: calculate least number of words in a line


// TODO: translate messages


/// <summary>
/// namespace Test
/// </summary>
namespace Test
{
	/// <summary>
	/// public class Program - main application entrypoint class
	/// </summary>
	public class Program
	{

#region Language Details
				
		/// <summary>
		/// private e_Language - enumeration storage for language codes in no particular order
		/// </summary>
		private enum e_Language{
			none,
			en,
			fr,
			it,
			es,
			max
		};
		
		/// <summary>
		/// private struct LanguageName - storage struct for a language code string to e_Language enum mapping
		/// </summary>
		private struct LanguageName{
			public e_Language _lang;
			public string _code;
		};

		/// <summary>
		/// private enum e_Texts - enumeration of text codes to be associated with globalized text
		/// </summary>
		private enum e_Texts{
			TEXT_NONE,
			TEXT_INVALID_EXE,			
			TEXT_INVALID_ARGUMENTS,
			TEXT_USING_LANGUAGE,
			TEXT_INVALID_LANGUAGE,
			TEXT_USAGE_LINE_0001,
			TEXT_USAGE_LINE_0002,
			TEXT_USAGE_LINE_0003,
			TEXT_USAGE_LINE_0004,
			TEXT_USAGE_LINE_0005,
			TEXT_USAGE_LINE_0006,
			TEXT_USAGE_LINE_0007,
			TEXT_USAGE_LINE_0008,
			TEXT_USAGE_LINE_0009,
			TEXT_USAGE_LINE_0010,
			TEXT_PARAMETER_MULTIUSE,
			TEXT_NOT_ENOUGH_PARAMETERS,
			TEXT_LANGUAGE_OVERRIDE,
			TEXT_WORD_COUNT,
			TEXT_DISTINCT_WORD_COUNT,
			TEXT_CHARACTER_COUNT,
			TEXT_LINE_COUNT,
			TEXT_NULL_DATA,
			TEXT_EMPTY_DATA,
			TEXT_REPORT_EMPTY,
			TEXT_FILE_DOES_NOT_EXIST,
			TEXT_INVALID_ESCAPE,
			TEXT_EXCEPTION,
			TEXT_MAX	
		};

		/// <summary>
		/// private struct LanguageText - storage struct for a Text Code to text mapping
		/// </summary>
		private struct LanguageText
		{
			public e_Texts _code;
			public string _text;
		}

		/// <summary>
		/// private struct LanguageTexts - storage struct for a e_Language code to an array of LanguageText text code mappings
		/// </summary>
		private struct LanguageTexts{
			public e_Language _theLanguage;
			public LanguageText [] _theLanguageText;
		};

		/// <summary>
		/// private static LanguageName [] _LanguageNames - storage variable for all language name to code mappings
		/// </summary>
		private static LanguageName [] _LanguageNames = new LanguageName[]{
			new LanguageName(){_lang=e_Language.none, _code="__" },
			new LanguageName(){_lang=e_Language.en, _code="en" },
			new LanguageName(){_lang=e_Language.fr, _code="fr" },
			new LanguageName(){_lang=e_Language.it, _code="it" },
			new LanguageName(){_lang=e_Language.es, _code="es" },
		};

		/// <summary>
		/// private static LanguageTexts[] _languageTexts - storage variable for all language and text mappings
		/// </summary>
		private static LanguageTexts[] _languageTexts = new LanguageTexts[]
		{
			new LanguageTexts(){
				_theLanguage = e_Language.none,
				_theLanguageText=new LanguageText[] {
					new LanguageText(){_code=e_Texts.TEXT_NONE,_text=String.Empty},
				}
			},
			new LanguageTexts(){
				_theLanguage = e_Language.en,
				_theLanguageText=new LanguageText[] {
					new LanguageText(){_code=e_Texts.TEXT_NONE,_text=String.Empty},
					new LanguageText(){_code=e_Texts.TEXT_INVALID_EXE,_text="Invalid Exe"},
					new LanguageText(){_code=e_Texts.TEXT_INVALID_ARGUMENTS,_text="Invalid Arguments"},
					new LanguageText(){_code=e_Texts.TEXT_USING_LANGUAGE,_text="Using Language "},
					new LanguageText(){_code=e_Texts.TEXT_INVALID_LANGUAGE,_text="Invalid Language "},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0001,_text="[-w|--words] [-d|--distinct] [-c|--characters] [-h|--histogram] [-l|--lines] [[-f|--file] <filespec>] [-r|--recursive] [[-l|--lan] <en|fr|it|es>] message"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0002,_text="\t-w|--words - count words"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0003,_text="\t-d|--distinct - count distinct words"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0004,_text="\t-c|--characters - count characters"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0005,_text="\t-h|--histogram - histogram of characters"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0006,_text="\t-l|--lines - count lines"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0007,_text="\t-f|--file <filespec> - the file path or glob to work on"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0008,_text="\t-r|--recursive - operate recursively"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0009,_text="\t-l|--lang <en|fr|it|es> - set the language for the program"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0010,_text="\t[message] - a message to operate on, if files are specified, this should not be"},
					new LanguageText(){_code=e_Texts.TEXT_PARAMETER_MULTIUSE,_text="Parameter used more than once"},
					new LanguageText(){_code=e_Texts.TEXT_NOT_ENOUGH_PARAMETERS,_text="Not enough parameters"},
					new LanguageText(){_code=e_Texts.TEXT_LANGUAGE_OVERRIDE,_text="Language Override"},
					new LanguageText(){_code=e_Texts.TEXT_WORD_COUNT,_text="Word Count:"},
					new LanguageText(){_code=e_Texts.TEXT_DISTINCT_WORD_COUNT,_text="Distinct Count:"},
					new LanguageText(){_code=e_Texts.TEXT_CHARACTER_COUNT,_text="Character Count:"},
					new LanguageText(){_code=e_Texts.TEXT_LINE_COUNT,_text="Line Count:"},
					new LanguageText(){_code=e_Texts.TEXT_NULL_DATA,_text="Null Data Sent to Process"},
					new LanguageText(){_code=e_Texts.TEXT_EMPTY_DATA,_text="Empty Sent to Process"},
					new LanguageText(){_code=e_Texts.TEXT_REPORT_EMPTY,_text="Report Empty"},
					new LanguageText(){_code=e_Texts.TEXT_FILE_DOES_NOT_EXIST,_text="File does not exist or is inaccessible"},
					new LanguageText(){_code=e_Texts.TEXT_INVALID_ESCAPE,_text="Invalid Escape"},
					new LanguageText(){_code=e_Texts.TEXT_EXCEPTION,_text="Exception"},
					
					

				}	
			},
			new LanguageTexts(){
				_theLanguage = e_Language.fr,
				_theLanguageText=new LanguageText[] {
					new LanguageText(){_code=e_Texts.TEXT_NONE,_text=String.Empty},
					new LanguageText(){_code=e_Texts.TEXT_INVALID_EXE,_text="Invalid Exe"},
					new LanguageText(){_code=e_Texts.TEXT_INVALID_ARGUMENTS,_text="Invalid Arguments"},
					new LanguageText(){_code=e_Texts.TEXT_USING_LANGUAGE,_text="Using Language"},
					new LanguageText(){_code=e_Texts.TEXT_INVALID_LANGUAGE,_text="Invalid Language "},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0001,_text="[-w|--words] [-d|--distinct] [-c|--characters] [-h|--histogram] [-l|--lines] [[-f|--file] <filespec>] [-r|--recursive] [[-l|--lan] <en|fr|it|es>] message"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0002,_text="\t-w|--words - count words"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0003,_text="\t-d|--distinct - count distinct words"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0004,_text="\t-c|--characters - count characters"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0005,_text="\t-h|--histogram - histogram of characters"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0006,_text="\t-l|--lines - count lines"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0007,_text="\t-f|--file <filespec> - the file path or glob to work on"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0008,_text="\t-r|--recursive - operate recursively"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0009,_text="\t-l|--lang <en|fr|it|es> - set the language for the program"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0010,_text="\t[message] - a message to operate on, if files are specified, this should not be"},
					new LanguageText(){_code=e_Texts.TEXT_PARAMETER_MULTIUSE,_text="Parameter used more than once"},
					new LanguageText(){_code=e_Texts.TEXT_NOT_ENOUGH_PARAMETERS,_text="Not enough parameters"},
					new LanguageText(){_code=e_Texts.TEXT_LANGUAGE_OVERRIDE,_text="Language Override"},
					new LanguageText(){_code=e_Texts.TEXT_WORD_COUNT,_text="Word Count:"},
					new LanguageText(){_code=e_Texts.TEXT_CHARACTER_COUNT,_text="Character Count:"},
					new LanguageText(){_code=e_Texts.TEXT_LINE_COUNT,_text="Line Count:"},
					new LanguageText(){_code=e_Texts.TEXT_NULL_DATA,_text="Null Data Sent to Process"},
					new LanguageText(){_code=e_Texts.TEXT_EMPTY_DATA,_text="Empty Sent to Process"},
					new LanguageText(){_code=e_Texts.TEXT_REPORT_EMPTY,_text="Report Empty"},
					new LanguageText(){_code=e_Texts.TEXT_FILE_DOES_NOT_EXIST,_text="File does not exist or is inaccessible"},
					new LanguageText(){_code=e_Texts.TEXT_INVALID_ESCAPE,_text="Invalid Escape"},
					new LanguageText(){_code=e_Texts.TEXT_EXCEPTION,_text="Exception"},
				}	
			},
			new LanguageTexts(){
				_theLanguage = e_Language.it,
				_theLanguageText=new LanguageText[] {
					new LanguageText(){_code=e_Texts.TEXT_NONE,_text=String.Empty},
					new LanguageText(){_code=e_Texts.TEXT_INVALID_EXE,_text="Invalid Exe"},
					new LanguageText(){_code=e_Texts.TEXT_INVALID_ARGUMENTS,_text="Invalid Arguments"},
					new LanguageText(){_code=e_Texts.TEXT_USING_LANGUAGE,_text="Using Language"},
					new LanguageText(){_code=e_Texts.TEXT_INVALID_LANGUAGE,_text="Invalid Language "},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0001,_text="[-w|--words] [-d|--distinct] [-c|--characters] [-h|--histogram] [-l|--lines] [[-f|--file] <filespec>] [-r|--recursive] [[-l|--lan] <en|fr|it|es>] message"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0002,_text="\t-w|--words - count words"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0003,_text="\t-d|--distinct - count distinct words"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0004,_text="\t-c|--characters - count characters"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0005,_text="\t-h|--histogram - histogram of characters"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0006,_text="\t-l|--lines - count lines"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0007,_text="\t-f|--file <filespec> - the file path or glob to work on"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0008,_text="\t-r|--recursive - operate recursively"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0009,_text="\t-l|--lang <en|fr|it|es> - set the language for the program"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0010,_text="\t[message] - a message to operate on, if files are specified, this should not be"},
					new LanguageText(){_code=e_Texts.TEXT_PARAMETER_MULTIUSE,_text="Parameter used more than once"},
					new LanguageText(){_code=e_Texts.TEXT_NOT_ENOUGH_PARAMETERS,_text="Not enough parameters"},
					new LanguageText(){_code=e_Texts.TEXT_LANGUAGE_OVERRIDE,_text="Language Override"},
					new LanguageText(){_code=e_Texts.TEXT_WORD_COUNT,_text="Word Count:"},
					new LanguageText(){_code=e_Texts.TEXT_CHARACTER_COUNT,_text="Character Count:"},
					new LanguageText(){_code=e_Texts.TEXT_LINE_COUNT,_text="Line Count:"},
					new LanguageText(){_code=e_Texts.TEXT_NULL_DATA,_text="Null Data Sent to Process"},
					new LanguageText(){_code=e_Texts.TEXT_EMPTY_DATA,_text="Empty Sent to Process"},
					new LanguageText(){_code=e_Texts.TEXT_REPORT_EMPTY,_text="Report Empty"},
					new LanguageText(){_code=e_Texts.TEXT_FILE_DOES_NOT_EXIST,_text="File does not exist or is inaccessible"},
					new LanguageText(){_code=e_Texts.TEXT_INVALID_ESCAPE,_text="Invalid Escape"},
					new LanguageText(){_code=e_Texts.TEXT_EXCEPTION,_text="Exception"},

				}	
			},
			new LanguageTexts(){
				_theLanguage = e_Language.es,
				_theLanguageText=new LanguageText[] {
					new LanguageText(){_code=e_Texts.TEXT_NONE,_text=String.Empty},
					new LanguageText(){_code=e_Texts.TEXT_INVALID_EXE,_text="Invalid Exe"},
					new LanguageText(){_code=e_Texts.TEXT_INVALID_ARGUMENTS,_text="Invalid Arguments"},
					new LanguageText(){_code=e_Texts.TEXT_USING_LANGUAGE,_text="Using Language"},
					new LanguageText(){_code=e_Texts.TEXT_INVALID_LANGUAGE,_text="Invalid Language "},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0001,_text="[-w|--words] [-d|--distinct] [-c|--characters] [-h|--histogram] [-l|--lines] [[-f|--file] <filespec>] [-r|--recursive] [[-l|--lan] <en|fr|it|es>] message"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0002,_text="\t-w|--words - count words"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0003,_text="\t-d|--distinct - count distinct words"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0004,_text="\t-c|--characters - count characters"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0005,_text="\t-h|--histogram - histogram of characters"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0006,_text="\t-l|--lines - count lines"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0007,_text="\t-f|--file <filespec> - the file path or glob to work on"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0008,_text="\t-r|--recursive - operate recursively"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0009,_text="\t-l|--lang <en|fr|it|es> - set the language for the program"},
					new LanguageText(){_code=e_Texts.TEXT_USAGE_LINE_0010,_text="\t[message] - a message to operate on, if files are specified, this should not be"},
					new LanguageText(){_code=e_Texts.TEXT_PARAMETER_MULTIUSE,_text="Parameter used more than once"},
					new LanguageText(){_code=e_Texts.TEXT_NOT_ENOUGH_PARAMETERS,_text="Not enough parameters"},
					new LanguageText(){_code=e_Texts.TEXT_LANGUAGE_OVERRIDE,_text="Language Override"},
					new LanguageText(){_code=e_Texts.TEXT_WORD_COUNT,_text="Word Count:"},
					new LanguageText(){_code=e_Texts.TEXT_CHARACTER_COUNT,_text="Character Count:"},
					new LanguageText(){_code=e_Texts.TEXT_LINE_COUNT,_text="Line Count:"},
					new LanguageText(){_code=e_Texts.TEXT_NULL_DATA,_text="Null Data Sent to Process"},
					new LanguageText(){_code=e_Texts.TEXT_EMPTY_DATA,_text="Empty Sent to Process"},
					new LanguageText(){_code=e_Texts.TEXT_REPORT_EMPTY,_text="Report Empty"},
					new LanguageText(){_code=e_Texts.TEXT_FILE_DOES_NOT_EXIST,_text="File does not exist or is inaccessible"},
					new LanguageText(){_code=e_Texts.TEXT_INVALID_ESCAPE,_text="Invalid Escape"},
					new LanguageText(){_code=e_Texts.TEXT_EXCEPTION,_text="Exception"},

				}	
			},
		};

		/// <summary>
		/// private static string GetLanguageText(e_Language theLanguage, e_Texts theText)
		/// <example>
		/// For example:
		/// <code>
		/// GetLanguageText(e_Language.en, e_Texts.TEXT_INVALID_EXE);
		/// </code>
		/// results in "Invalid Exe" returned
		/// </example>
		/// </summary>
		private static string GetLanguageText(e_Language theLanguage, e_Texts theText)
		{
			string theString = string.Empty;
			// Basic Sanity Check
			if( theLanguage >= e_Language.none && theLanguage <= e_Language.max )
			{ 
				if ( (uint)theLanguage < _languageTexts.Length)
				{
					if((uint)theText>= 0 && (uint)theText < _languageTexts[(uint)theLanguage]._theLanguageText.Length )
					{
						theString = _languageTexts[(uint)theLanguage]._theLanguageText[(uint)theText]._text;
					}
				} 
			}
			return theString;
		}

		/// <summary>
		/// private static e_Language GetLanguage()
		/// <example>
		/// For example:
		/// <code>
		/// GetLanguageText();
		/// </code>
		/// results in the e_Language corresponding language for the current culture- may be overridden by command line
		/// </example>
		/// </summary>
		private static e_Language GetLanguage()
		{
			e_Language theLanguage = e_Language.en;
			string theLanguageName = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
			foreach ( LanguageName language in _LanguageNames)
			{
				if(string.Equals(language._code,theLanguageName))
				{
					if(language._lang >= e_Language.none && language._lang <= e_Language.max)
					{
						return language._lang;
					}
					else
					{
						Console.Error.WriteLine(string.Format(System.Globalization.CultureInfo.CurrentCulture,"Invalid Language {0}",theLanguageName));
						Environment.Exit(255);
					}
				}
			}
			return theLanguage;
		}


#endregion

#region Private Member Variables
		/// <summary>
		/// private static readonly string _appName - stores the expected app name
		/// </summary>
		private static readonly string _appName = "count.exe";
		
		/// <summary>
		/// private static bool _wordsSet - flag for word parameter set on commandline
		/// </summary>
		private static bool _wordSet = false;
		
		/// <summary>
		/// private static bool _distinctSet - flag for distinct parameter set on commandline
		/// </summary>
		private static bool _distinctSet=false;
		
		/// <summary>
		/// private static bool _characterSet - flag for character parameter set on commandline
		/// </summary>
		private static bool _characterSet=false;
		
		/// <summary>
		/// private static bool _histogramSet - flag for histogram parameter set on commandline
		/// </summary>
		private static bool _histogramSet=false;
		
		/// <summary>
		/// private static bool _linesSet - flag for line parameter set on commandline
		/// </summary>
		private static bool _lineSet = false;
		
		/// <summary>
		/// private static bool _fileSet - flag for file parameter set on commandline.
		/// </summary>
		private static bool _fileSet = false;
		
		/// <summary>
		/// private static string _fileValue - storage for file spec specified on commandline
		/// </summary>
		private static string _fileValue = string.Empty;
		
		/// <summary>
		/// private static bool _recursiveSet - flag for recursive parameter set on commandline.
		/// </summary>
		private static bool _recursiveSet = false;
		
		/// <summary>
		/// private static bool _langSet - flag for language parameter set on commandline.
		/// </summary>
		private static bool _langSet = false;
		
		/// <summary>
		/// private static bool _lang - storage for language parameter set on commandline.
		/// </summary>
		private static string _langValue = string.Empty;

		/// <summary>
		/// private static bool _messageSet - flag for message parameter set on commandline.
		/// </summary>
		private static bool _messageSet = false;

		/// <summary>
		/// private static string _commandLineValue
		/// </summary>
		private static string _message = string.Empty ;
		
		/// <summary>
		/// private static e_Language _currentLanguage - stores language 
		/// </summary>
		private static e_Language _currentLanguage = e_Language.none;
		
		/// <summary>
		/// public report struct - store report details
		/// </summary>
		public struct report
		{
			public ulong _wordCount;
			public ulong _distinctWords;
			public ulong _characters;
			public ulong _lines;
			public ulong _longestLine;
			public ulong _shortestLine;
			public ulong _greatestWordsLine;
			public ulong _leastWordsLine;
			public SortedDictionary<char,ulong> _histogram; 
			public SortedDictionary<string,ulong> _distinctWordsList;
		};
		
		private static report _reportDetails = new report(){
			_wordCount=0,_distinctWords=0,_characters=0,_lines=0,
			_longestLine=UInt64.MinValue,_shortestLine=UInt64.MaxValue,_greatestWordsLine=UInt64.MinValue,_leastWordsLine=UInt64.MaxValue,
			_histogram=new SortedDictionary<char,ulong>(),_distinctWordsList=new SortedDictionary<string,ulong>()
		};

#endregion

#region Main Entrypoint

		/// <summary>
		/// public static int Main(string[] arguments)
		/// <example>
		/// <code>
		/// Main("","");
		/// </code>
		/// results in a test of the regex against a string
		/// </example>
		/// </summary>
		public static int Main(string [] arguments)
		{
			string[] lines=null;
			e_Language languageOverride;
			_currentLanguage = GetLanguage();
			if(_currentLanguage <= e_Language.none|| _currentLanguage >= e_Language.max)
			{
				Console.Error.WriteLine(string.Format(System.Globalization.CultureInfo.CurrentCulture,":( Invalid Language :("));
				Environment.Exit(1);
			}
			Console.WriteLine("{0} {1}",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_USING_LANGUAGE)),_currentLanguage);
			if ( !string.Equals(_appName,System.AppDomain.CurrentDomain.FriendlyName) )
			{
				Console.Error.WriteLine(string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_INVALID_EXE)));
				Environment.Exit(255);
			}
			if(arguments.Length<1)
			{
				Console.Error.WriteLine("No Arguments");
				Usage();
			}
			
			for(uint index=0;index<arguments.Length;index++)
			{
				string argument = arguments[index];
				if(string.Equals("-w",argument)||
					string.Equals("--words",argument))
				{
					if(!_wordSet)
					{
						_wordSet=true;
					}
					else
					{
						Console.Error.WriteLine("{0} {1} [{2}]",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_PARAMETER_MULTIUSE)),index,argument);
						Environment.Exit(255);
					}
				}
				else if(string.Equals("-d",argument)||
					string.Equals("--distinct",argument))
				{
					if(!_distinctSet)
					{
						_distinctSet=true;
					}
					else
					{
						Console.Error.WriteLine("{0} {1} [{2}]",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_PARAMETER_MULTIUSE)),index,argument);
						Environment.Exit(255);
					}
				}
				else if(string.Equals("-c",argument)||
					string.Equals("--characters",argument))
				{
					if(!_characterSet)
					{
						_characterSet=true;
					}
					else
					{
						Console.Error.WriteLine("{0} {1} [{2}]",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_PARAMETER_MULTIUSE)),index,argument);
						Environment.Exit(255);
					}
				}
				else if(string.Equals("-h",argument)||
					string.Equals("--histogram",argument))
				{
					if(!_histogramSet)
					{
						_histogramSet=true;
					}
					else
					{
						Console.Error.WriteLine("{0} {1} [{2}]",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_PARAMETER_MULTIUSE)),index,argument);
						Environment.Exit(255);
					}
				}
				else if(string.Equals("-l",argument)||
					string.Equals("--lines",argument))
				{
					if(!_lineSet)
					{
						_lineSet=true;
					}
					else
					{
						Console.Error.WriteLine("{0} {1} [{2}]",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_PARAMETER_MULTIUSE)),index,argument);
						Environment.Exit(255);
					}
				}
				else if(string.Equals("-f",argument)||
					string.Equals("--file",argument))
				{
					if(index<arguments.Length-1)
					{
						if(!_fileSet)
						{
							_fileSet=true;
							_fileValue = arguments[index+1].Trim().ToLowerInvariant();
							index++;
						}
						else
						{
							Console.Error.WriteLine("{0} {1} [{2}]",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_PARAMETER_MULTIUSE)),index,argument);
							Environment.Exit(255);
						}
					}
					else
					{
						Console.Error.WriteLine("{0} {1} [{2}]",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_NOT_ENOUGH_PARAMETERS)),index,argument);
						Environment.Exit(255);
					}
				}
				else if(string.Equals("-r",argument)||
					string.Equals("--recursive",argument))
				{
					if(!_recursiveSet)
					{
						_recursiveSet=true;
					}
					else
					{
						Console.Error.WriteLine("{0} {1} [{2}]",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_PARAMETER_MULTIUSE)),index,argument);
						Environment.Exit(255);
					}
				}
				else if(string.Equals("-l",argument)||
					string.Equals("--lang",argument))
				{
					if(index<arguments.Length-1)
					{
						if(!_langSet)
						{
							_langSet=true;
							_langValue = arguments[index+1].Trim().ToLowerInvariant();
							index++;
						}
						else
						{
							Console.Error.WriteLine("{0} {1} [{2}]",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_PARAMETER_MULTIUSE)),index,argument);
							Environment.Exit(255);
						}
					}
					else
					{
						Console.Error.WriteLine("{0} {1} [{2}]",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_NOT_ENOUGH_PARAMETERS)),index,argument);
						Environment.Exit(255);
					}
				}
				else
				{
					if(index==arguments.Length-1)
					{
						if(!_messageSet)
						{
							_messageSet=true;
							_message=argument;
							index++;
						}
						else
						{
							Console.Error.WriteLine("{0} {1} [{2}]",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_PARAMETER_MULTIUSE)),index,argument);
							Environment.Exit(255);
						}
					}
					else
					{
						Console.Error.WriteLine("{0} {1} [{2}]",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_INVALID_ARGUMENTS)),index,argument);
						Usage();
						Environment.Exit(255);
					}
				}
			}
			if(_langSet)
			{
				languageOverride=LanguageCodeToLanguage(_langValue);
				if(languageOverride>e_Language.none && languageOverride<e_Language.max)
				{
					_currentLanguage=languageOverride;
					Console.WriteLine("{0} {1}",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_LANGUAGE_OVERRIDE)),_langValue);
				}
				else
				{
					Console.Error.WriteLine("{0} {1}",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_INVALID_LANGUAGE)),_langValue);
					Environment.Exit(255);
				}
			}
			if(!_fileSet&&!_messageSet)
			{
				string s = string.Empty;
				List<string> lineList = new List<string>();
				while ((s = Console.ReadLine()) != null)
				{
					lineList.Add(s);
				}
				lines = new string[lineList.Count];
				lineList.CopyTo(lines);
				Process(lines);
				// Process string list
			}
			else if (_fileSet)
			{
				// Process the file
				if(_fileValue.Contains("*"))
				{
					Console.WriteLine("Invalid File Glob Not Supported Yet");
					Environment.Exit(0);
				}
				else
				{					
					if(File.Exists(_fileValue))
					{
						lines=File.ReadAllLines(_fileValue);
						Process(lines);
					}
					else
					{
						Console.Error.WriteLine("{1} {0}",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_FILE_DOES_NOT_EXIST)),_fileValue);
						Environment.Exit(255);
					}
				}
			}
			else if(_messageSet)
			{
				// Process the message
				_message = Unescape(_message);
				lines= _message.Replace("\r","").Split(new char[]{'\n'});
				Process(lines);
				
			}
			Console.WriteLine("Success Point");
			return 0;
		}
#endregion

#region Private Methods

		/// <summary>
		/// private static e_Language LanguageCodeToLanguage(string lang)
		/// <example>
		/// <code>
		/// LanguageCodeToLanguage("en");
		/// </code>
		/// results in the e_Language enum value returned or e_Language.none
		/// </example>
		/// </summary>
		private static e_Language LanguageCodeToLanguage(string lang)
		{
			e_Language returnLanguage = e_Language.none;
			for(uint index=(uint)e_Language.none;index<(uint)e_Language.max;index++)
			{
				if(string.Equals(_LanguageNames[index]._code,lang.ToLower().Trim()))
				{
					returnLanguage = _LanguageNames[index]._lang;
				}
			}
			return returnLanguage;
		}

		/// <summary>
		/// private static void Usage()
		/// <example>
		/// <code>
		/// Usage();
		/// </code>
		/// results in the usage message output to standard output
		/// </example>
		/// </summary>
		private static void Usage()
		{
			Console.Error.WriteLine("{1} {0}",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_USAGE_LINE_0001)),System.AppDomain.CurrentDomain.FriendlyName);
			Console.Error.WriteLine(string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_USAGE_LINE_0002)));
			Console.Error.WriteLine(string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_USAGE_LINE_0003)));
			Console.Error.WriteLine(string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_USAGE_LINE_0004)));
			Console.Error.WriteLine(string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_USAGE_LINE_0005)));
			Console.Error.WriteLine(string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_USAGE_LINE_0006)));
			Console.Error.WriteLine(string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_USAGE_LINE_0007)));
			Console.Error.WriteLine(string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_USAGE_LINE_0008)));
			Console.Error.WriteLine(string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_USAGE_LINE_0009)));
			Console.Error.WriteLine(string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_USAGE_LINE_0010)));
			
			Environment.Exit(255);
		}
		
		/// <summary>
		/// private static string Unescape ( string theString)
		/// <example>
		/// <code>
		/// Unescape("\0\a\b\t\v\h\r\n\u0001\U00000001");
		/// </code>
		/// results in the unescaped string returned to the user, be cautioned about proper usage of unicode mixed characters
		/// </example>
		/// </summary>
		private static string Unescape ( string theString)
		{
			StringBuilder sb = new StringBuilder();
			if(theString==null||theString.Length<=0)
			{
				Console.Error.WriteLine(string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_NULL_DATA)));
				Environment.Exit(255);
			}
			for(uint index=0;index<(uint)theString.Length;index++)
			{
				Char theChar = theString[(int)index];
				if(theChar=='\\')
				{
					if(index<theString.Length-1)
					{
						switch(theString[(int)index+1])
						{
							case '0':
								sb.Append('\0');
								index++;
							break;
							case 'a':
								sb.Append('\a');
								index++;
							break;
							case 'b':
								sb.Append('\b');
								index++;
							break;
							case '\t':
								sb.Append('\t');
							break;
							case '\n':
								sb.Append('\n');
							break;
							case 'v':
								sb.Append('\v');
								index++;
							break;
							case 'f':
								sb.Append('\f');
								index++;
							break;
							case 'r':
								sb.Append('\r');
								index++;
							break;
							case '"':
								sb.Append('\"');
								index++;
							break;
							case  '\'':
								sb.Append('\'');
								index++;
							break;
							case  '\\':
								sb.Append('\\');
								index++;
							break;
							case 'u': // 4 hex character \u0000
								if(index<theString.Length-5)
								{
									// Skip the \u
									index++;
									index++;
									sb.Append(IsHexUnicode4(theString.Substring((int)index,4)));
									index+=3;
									if(index<theString.Length)
									{
										Console.WriteLine("Index:{0} {1}",index,theString[(int)index]);
									}
								}
								else
								{
									Console.Error.WriteLine("{0} [{1}] [{2}]",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_INVALID_ESCAPE)),index,theString);
									Environment.Exit(255);
								}
							break;
							case 'U': // 8 hex character \U00000000
								// Skip the \U
								if(index<theString.Length-9)
								{
									index++;
									index++;
									sb.Append(IsHexUnicode8(theString.Substring((int)index,8)));
									index+=7;
									if(index<theString.Length)
									{
										Console.WriteLine("Index:{0} {1}",index,theString[(int)index]);
									}
								}
								else
								{
									Console.Error.WriteLine("{0} [{1}] [{2}]",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_INVALID_ESCAPE)),index,theString);
									Environment.Exit(255);
								}
							break;
							
							
						}
					}
					else
					{
						Console.Error.WriteLine("{0} [{1}] [{2}]",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_INVALID_ESCAPE)),index,theString);
						Environment.Exit(255);
					}
				}
				else if( theChar=='"')
				{
					if(index<theString.Length-1)
					{
						if(theString[(int)index+1]=='"')
						{
							sb.Append('"');
							index++;
						}
						else
						{
							sb.Append('"');
						}
					}
					else
					{
						sb.Append('"');
					}
				}
				else
				{
					sb.Append(theChar);
				}
			}
			return sb.ToString();
		}

		/// <summary>
		/// private static void Process(string[]lines)
		/// <example>
		/// <code>
		/// Process({"Test this out","This is a test");
		/// </code>
		/// results in Report statistics of data printed based on command line flags sent to program unless error
		/// </example>
		/// </summary>
		private static void Process(string[]lines)
		{
//#if DEBUG
			Console.WriteLine("Start of Process");
//#endif
			string regexWordMatch = "(\\p{L}+ ?)";
			Regex wordMatch= new Regex(regexWordMatch,RegexOptions.Compiled,TimeSpan.FromSeconds(1));
			if(lines==null)
			{
				Console.Error.WriteLine(string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_NULL_DATA)));
				Environment.Exit(255);
			}
			if(lines.LongLength>0)
			{
				foreach(string line in lines)
				{
					if(line.Length>0)
					{
						if(_wordSet||_distinctSet)
						{
							foreach ( Match m in wordMatch.Matches(line))
							{
								string matchValue= m.Value.Trim();
								if(m.Success && matchValue.Length>0)
								{
									if(_wordSet)
									{
										_reportDetails._wordCount+=1;
									}
									if(_distinctSet)
									{
										if(!_reportDetails._distinctWordsList.ContainsKey(matchValue))
										{
											_reportDetails._distinctWordsList.Add(matchValue,1);
											_reportDetails._distinctWords++;
										}
										else
										{
											_reportDetails._distinctWordsList[matchValue]++;
										}
									}
									
									#if DEBUG
									Console.WriteLine("[{0}] [{1}] [{2}]",m.Value,m.Value.Length,m.Success);
									#endif
								}
							}
						}
						if(_characterSet)
						{
							_reportDetails._characters+=(uint)line.Length;
						}
						if(_lineSet)
						{
							_reportDetails._lines++;
						}
						if(_histogramSet)
						{
							foreach (char theChar in line)
							{
								if(!_reportDetails._histogram.ContainsKey(theChar))
								{
									_reportDetails._histogram.Add(theChar,1);
								}
								else
								{
									_reportDetails._histogram[theChar]++;
								}
							}
						}
					}
				}
				PrintReport();
			}
			else
			{
				Console.Error.WriteLine(string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_EMPTY_DATA)));
				Environment.Exit(255);
			}
		}
		
		/// <summary>
		/// private static string IsHexUnicode4(string theCharacter)
		/// <example>
		/// <code>
		/// IsHexUnicode4("0000"); // 8 character hex string
		/// </code>
		/// results in the unicode character string equivelant returned unless error or exception
		/// </example>
		/// </summary>
		private static string IsHexUnicode4(string theCharacter)
		{
			string returnString=string.Empty;
			string theChar=string.Empty;
			byte [] theByteArray={0x0,0x0};
			if(theCharacter==null||theCharacter.Length<=0)
			{
				Console.WriteLine(string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_NULL_DATA)));
				Environment.Exit(255);
			}
			theCharacter=theCharacter.ToUpper();
			if(theCharacter.Length==4)
			{
				uint count=0;
				byte currentByte=0;
				byte charByte=0;
				for(uint index=0;index<4;index++)
				{
					if(theCharacter[(int)index]>='0' && theCharacter[(int)index]<='9')
					{
						charByte=(byte)theCharacter[(int)index];
						currentByte+=(byte)(charByte-(byte)0x30);
					}
					else if(theCharacter[(int)index]>='A'&&theCharacter[(int)index]<='F')
					{
						charByte=(byte)theCharacter[(int)index];
						currentByte+=(byte)(charByte-(byte)0x41+(byte)0x0A);
					}
					else
					{
						//Invalid Character
						Console.WriteLine("{0} [{1}] [{2}] [{3}]",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_INVALID_ESCAPE)),theCharacter,index,theCharacter[(int)index]);
						Environment.Exit(255);
					}
					
					 
					if(index%2==0 && index>0)
					{
						theByteArray[count]=currentByte;
						count++;
						currentByte=0;
						continue;
					}
					currentByte<<=4;
				}
				 
			}
			else
			{
				Console.WriteLine("{0} [{1}] [{2}]",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_INVALID_ESCAPE)),theCharacter,theCharacter.Length);
				Environment.Exit(255);
			}
			try
			{
				returnString=Encoding.Unicode.GetString(theByteArray);
			}
			catch(ArgumentException aex)
			{
				Console.WriteLine("{0} [{1}] [{2}] [{3}] [{4}]",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_EXCEPTION)),theCharacter,theCharacter.Length,aex.Message,aex.StackTrace);
				Environment.Exit(255);
			}			
			return  returnString;
		}
		
		/// <summary>
		/// private static string IsHexUnicode8(string theCharacter)
		/// <example>
		/// <code>
		/// IsHexUnicode8("00000000"); // 8 character hex string
		/// </code>
		/// results in the unicode character string equivelant returned unless error or exception
		/// </example>
		/// </summary>
		private static string IsHexUnicode8(string theCharacter)
		{
			string returnString=string.Empty;
			string theChar=string.Empty;
			byte [] theByteArray={0x0,0x0,0x0,0x0};
			if(theCharacter==null||theCharacter.Length<=0)
			{
				Console.WriteLine(string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_NULL_DATA)));
				Environment.Exit(255);
			}
			theCharacter=theCharacter.ToUpper();
			if(theCharacter.Length==8)
			{
				uint count=0;
				byte charByte=0;
				byte currentByte=0;
				for(uint index=0;index<4;index++)
				{
					if(theCharacter[(int)index]>='0' && theCharacter[(int)index]<='9')
					{
						charByte=(byte)theCharacter[(int)index];
						currentByte+=(byte)(charByte-(byte)0x30);
					}
					else if(theCharacter[(int)index]>='A'&&theCharacter[(int)index]<='F')
					{
						charByte=(byte)theCharacter[(int)index];
						currentByte+=(byte)(charByte-(byte)0x41+(byte)0x0A);
					}
					else
					{
						//Invalid Character
						Console.WriteLine("{0} [{1}] [{2}] [{3}]",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_INVALID_ESCAPE)),theCharacter,index,theCharacter[(int)index]);
						Environment.Exit(255);
					}
					
					 
					if(index%2==0 && index>0)
					{
						theByteArray[count]=currentByte;
						count++;
						currentByte=0;
						continue;
					}
					currentByte<<=4;
				}
				 
			}
			else
			{
				Console.WriteLine("{0} [{1}] [{2}]",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_INVALID_ESCAPE)),theCharacter,theCharacter.Length);
				Environment.Exit(255);
			}
			try
			{
				returnString=Encoding.UTF32.GetString(theByteArray);
			}
			catch(ArgumentException aex)
			{
				Console.WriteLine("{0} [{1}] [{2}] [{3}] [{4}]",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_EXCEPTION)),theCharacter,theCharacter.Length,aex.Message,aex.StackTrace);
				Environment.Exit(255);
			}
			return returnString;
		}

		/// <summary>
		/// private static void PrintReport()
		/// <example>
		/// <code>
		/// PrintReport();
		/// </code>
		/// results in Report statistics printed to standard output if not error
		/// </example>
		/// </summary>
		private static void PrintReport()
		{
			//if(_reportDetails!=null)
			//{
				if(_wordSet)
				{
					Console.WriteLine("{0} {1}",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_WORD_COUNT)),_reportDetails._wordCount);
				}
				if(_distinctSet)
				{
					Console.WriteLine("{0} {1}",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_DISTINCT_WORD_COUNT)),_reportDetails._distinctWords);
			
					foreach(KeyValuePair<string,ulong> pair in _reportDetails._distinctWordsList)
					{
						Console.WriteLine("[{0}] [{1}]",pair.Key,pair.Value);
					}
				}
				if(_characterSet)
				{
					Console.WriteLine("{0} {1}",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_CHARACTER_COUNT)),_reportDetails._characters);
				}
				if(_lineSet)
				{
					Console.WriteLine("{0} {1}",string.Format(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_LINE_COUNT)),_reportDetails._lines);
				}
				if(_histogramSet)
				{
					foreach ( KeyValuePair<char, ulong> di in _reportDetails._histogram)
					{
						if(Char.IsControl(di.Key))
						{
							Console.WriteLine("[{0}] [{1}]",((int)di.Key).ToString("X8"),di.Value);
						}
						else if(di.Key<=255)
						{
							Console.WriteLine("[{0}] [{1}]",di.Key,di.Value);
						}
						else
						{
							Console.WriteLine("[{0}] [{1}]",((int)di.Key).ToString("X8"),di.Value);
						}
					}
				}
				
			
			/*public ulong _longestLine;
			public ulong _shortestLine;
			public ulong _greatestWordsLine;
			public ulong _leastWordsLine;
			public Dictionary<char,ulong> _histogram; 
			public Dictionary<string,ulong> _distinctWordsList;*/
			//}
			//else
			//{
			//	Console.Error.WriteLine(System.Globalization.CultureInfo.CurrentCulture,GetLanguageText(_currentLanguage,e_Texts.TEXT_REPORT_EMPTY));
			//}
		}
#endregion

	}

};