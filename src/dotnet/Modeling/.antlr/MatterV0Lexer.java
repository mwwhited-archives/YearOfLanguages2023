// Generated from c:\Repos\mwwhited-learning\YearOfLanguages2023\src\dotnet\Modeling\MatterV0.g4 by ANTLR 4.9.2
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class MatterV0Lexer extends Lexer {
	static { RuntimeMetaData.checkVersion("4.9.2", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, BUILTIN_ACCESS=11, RULE_NAME=12, WORD=13, CHAR=14, LETTER=15, 
		DIGIT=16, INTEGER=17, PREFIX=18, WS=19, EOL=20, SKIP_=21;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	private static String[] makeRuleNames() {
		return new String[] {
			"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
			"T__9", "BUILTIN_ACCESS", "RULE_NAME", "WORD", "CHAR", "LETTER", "DIGIT", 
			"INTEGER", "PREFIX", "WS", "EOL", "SKIP_", "COMMENT"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "':'", "'[]'", "'[*]'", "'[-]'", "'[+]'", "'['", "'-'", "']'", 
			"','", "'''"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, null, null, null, null, null, null, null, null, null, null, "BUILTIN_ACCESS", 
			"RULE_NAME", "WORD", "CHAR", "LETTER", "DIGIT", "INTEGER", "PREFIX", 
			"WS", "EOL", "SKIP_"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}


	public MatterV0Lexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "MatterV0.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2\27\u00a0\b\1\4\2"+
		"\t\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4"+
		"\13\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22"+
		"\t\22\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\3\2\3\2\3\3\3"+
		"\3\3\3\3\4\3\4\3\4\3\4\3\5\3\5\3\5\3\5\3\6\3\6\3\6\3\6\3\7\3\7\3\b\3\b"+
		"\3\t\3\t\3\n\3\n\3\13\3\13\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f"+
		"\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\3"+
		"\f\3\f\3\f\3\f\3\f\3\f\3\f\3\f\5\fo\n\f\3\r\3\r\3\r\3\16\3\16\6\16v\n"+
		"\16\r\16\16\16w\3\17\3\17\3\17\3\17\5\17~\n\17\3\20\3\20\3\21\3\21\3\22"+
		"\6\22\u0085\n\22\r\22\16\22\u0086\3\23\6\23\u008a\n\23\r\23\16\23\u008b"+
		"\3\24\3\24\3\25\3\25\3\25\3\25\3\26\3\26\3\26\3\26\3\27\3\27\3\27\3\27"+
		"\7\27\u009c\n\27\f\27\16\27\u009f\13\27\2\2\30\3\3\5\4\7\5\t\6\13\7\r"+
		"\b\17\t\21\n\23\13\25\f\27\r\31\16\33\17\35\20\37\21!\22#\23%\24\'\25"+
		")\26+\27-\2\3\2\b\4\2/\60aa\4\2C\\c|\3\2\62;\4\2\13\13\"\"\4\2\f\f\17"+
		"\17\4\2\f\f\16\17\2\u00a9\2\3\3\2\2\2\2\5\3\2\2\2\2\7\3\2\2\2\2\t\3\2"+
		"\2\2\2\13\3\2\2\2\2\r\3\2\2\2\2\17\3\2\2\2\2\21\3\2\2\2\2\23\3\2\2\2\2"+
		"\25\3\2\2\2\2\27\3\2\2\2\2\31\3\2\2\2\2\33\3\2\2\2\2\35\3\2\2\2\2\37\3"+
		"\2\2\2\2!\3\2\2\2\2#\3\2\2\2\2%\3\2\2\2\2\'\3\2\2\2\2)\3\2\2\2\2+\3\2"+
		"\2\2\3/\3\2\2\2\5\61\3\2\2\2\7\64\3\2\2\2\t8\3\2\2\2\13<\3\2\2\2\r@\3"+
		"\2\2\2\17B\3\2\2\2\21D\3\2\2\2\23F\3\2\2\2\25H\3\2\2\2\27n\3\2\2\2\31"+
		"p\3\2\2\2\33s\3\2\2\2\35}\3\2\2\2\37\177\3\2\2\2!\u0081\3\2\2\2#\u0084"+
		"\3\2\2\2%\u0089\3\2\2\2\'\u008d\3\2\2\2)\u008f\3\2\2\2+\u0093\3\2\2\2"+
		"-\u0097\3\2\2\2/\60\7<\2\2\60\4\3\2\2\2\61\62\7]\2\2\62\63\7_\2\2\63\6"+
		"\3\2\2\2\64\65\7]\2\2\65\66\7,\2\2\66\67\7_\2\2\67\b\3\2\2\289\7]\2\2"+
		"9:\7/\2\2:;\7_\2\2;\n\3\2\2\2<=\7]\2\2=>\7-\2\2>?\7_\2\2?\f\3\2\2\2@A"+
		"\7]\2\2A\16\3\2\2\2BC\7/\2\2C\20\3\2\2\2DE\7_\2\2E\22\3\2\2\2FG\7.\2\2"+
		"G\24\3\2\2\2HI\7)\2\2I\26\3\2\2\2JK\7g\2\2KL\7x\2\2LM\7g\2\2MN\7t\2\2"+
		"NO\7{\2\2OP\7q\2\2PQ\7p\2\2Qo\7g\2\2RS\7c\2\2ST\7w\2\2TU\7v\2\2UV\7j\2"+
		"\2VW\7g\2\2WX\7p\2\2XY\7v\2\2YZ\7k\2\2Z[\7e\2\2[\\\7c\2\2\\]\7v\2\2]^"+
		"\7g\2\2^o\7f\2\2_`\7u\2\2`a\7g\2\2ab\7n\2\2bo\7h\2\2cd\7c\2\2de\7f\2\2"+
		"ef\7o\2\2fg\7k\2\2go\7p\2\2hi\7u\2\2ij\7{\2\2jk\7u\2\2kl\7v\2\2lm\7g\2"+
		"\2mo\7o\2\2nJ\3\2\2\2nR\3\2\2\2n_\3\2\2\2nc\3\2\2\2nh\3\2\2\2o\30\3\2"+
		"\2\2pq\7B\2\2qr\5\33\16\2r\32\3\2\2\2su\5\37\20\2tv\5\35\17\2ut\3\2\2"+
		"\2vw\3\2\2\2wu\3\2\2\2wx\3\2\2\2x\34\3\2\2\2y~\5\37\20\2z~\5!\21\2{~\5"+
		"\'\24\2|~\t\2\2\2}y\3\2\2\2}z\3\2\2\2}{\3\2\2\2}|\3\2\2\2~\36\3\2\2\2"+
		"\177\u0080\t\3\2\2\u0080 \3\2\2\2\u0081\u0082\t\4\2\2\u0082\"\3\2\2\2"+
		"\u0083\u0085\5!\21\2\u0084\u0083\3\2\2\2\u0085\u0086\3\2\2\2\u0086\u0084"+
		"\3\2\2\2\u0086\u0087\3\2\2\2\u0087$\3\2\2\2\u0088\u008a\5\'\24\2\u0089"+
		"\u0088\3\2\2\2\u008a\u008b\3\2\2\2\u008b\u0089\3\2\2\2\u008b\u008c\3\2"+
		"\2\2\u008c&\3\2\2\2\u008d\u008e\t\5\2\2\u008e(\3\2\2\2\u008f\u0090\t\6"+
		"\2\2\u0090\u0091\3\2\2\2\u0091\u0092\b\25\2\2\u0092*\3\2\2\2\u0093\u0094"+
		"\5-\27\2\u0094\u0095\3\2\2\2\u0095\u0096\b\26\3\2\u0096,\3\2\2\2\u0097"+
		"\u0098\7\61\2\2\u0098\u0099\7\61\2\2\u0099\u009d\3\2\2\2\u009a\u009c\n"+
		"\7\2\2\u009b\u009a\3\2\2\2\u009c\u009f\3\2\2\2\u009d\u009b\3\2\2\2\u009d"+
		"\u009e\3\2\2\2\u009e.\3\2\2\2\u009f\u009d\3\2\2\2\t\2nw}\u0086\u008b\u009d"+
		"\4\2\3\2\b\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}