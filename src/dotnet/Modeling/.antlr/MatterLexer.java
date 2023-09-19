// Generated from c:\Repos\mwwhited-learning\YearOfLanguages2023\src\dotnet\Modeling\Matter.g4 by ANTLR 4.9.2
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class MatterLexer extends Lexer {
	static { RuntimeMetaData.checkVersion("4.9.2", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, WORD=2, CHAR=3, LETTER=4, DIGIT=5, INTEGER=6, PREFIX=7, WS=8, 
		EOL=9, SKIP_=10;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	private static String[] makeRuleNames() {
		return new String[] {
			"T__0", "WORD", "CHAR", "LETTER", "DIGIT", "INTEGER", "PREFIX", "WS", 
			"EOL", "SKIP_", "COMMENT"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "':'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, null, "WORD", "CHAR", "LETTER", "DIGIT", "INTEGER", "PREFIX", "WS", 
			"EOL", "SKIP_"
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


	public MatterLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "Matter.g4"; }

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
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2\fF\b\1\4\2\t\2\4"+
		"\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t"+
		"\13\4\f\t\f\3\2\3\2\3\3\3\3\7\3\36\n\3\f\3\16\3!\13\3\3\4\3\4\3\4\5\4"+
		"&\n\4\3\5\3\5\3\6\3\6\3\7\6\7-\n\7\r\7\16\7.\3\b\6\b\62\n\b\r\b\16\b\63"+
		"\3\t\3\t\3\n\3\n\3\13\3\13\3\13\3\13\3\f\3\f\3\f\3\f\7\fB\n\f\f\f\16\f"+
		"E\13\f\2\2\r\3\3\5\4\7\5\t\6\13\7\r\b\17\t\21\n\23\13\25\f\27\2\3\2\b"+
		"\4\2/\60aa\4\2C\\c|\3\2\62;\4\2\13\13\"\"\4\2\f\f\17\17\4\2\f\f\16\17"+
		"\2J\2\3\3\2\2\2\2\5\3\2\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13\3\2\2\2\2\r\3"+
		"\2\2\2\2\17\3\2\2\2\2\21\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2\2\3\31\3\2\2"+
		"\2\5\33\3\2\2\2\7%\3\2\2\2\t\'\3\2\2\2\13)\3\2\2\2\r,\3\2\2\2\17\61\3"+
		"\2\2\2\21\65\3\2\2\2\23\67\3\2\2\2\259\3\2\2\2\27=\3\2\2\2\31\32\7<\2"+
		"\2\32\4\3\2\2\2\33\37\5\t\5\2\34\36\5\7\4\2\35\34\3\2\2\2\36!\3\2\2\2"+
		"\37\35\3\2\2\2\37 \3\2\2\2 \6\3\2\2\2!\37\3\2\2\2\"&\5\t\5\2#&\5\13\6"+
		"\2$&\t\2\2\2%\"\3\2\2\2%#\3\2\2\2%$\3\2\2\2&\b\3\2\2\2\'(\t\3\2\2(\n\3"+
		"\2\2\2)*\t\4\2\2*\f\3\2\2\2+-\5\13\6\2,+\3\2\2\2-.\3\2\2\2.,\3\2\2\2."+
		"/\3\2\2\2/\16\3\2\2\2\60\62\5\21\t\2\61\60\3\2\2\2\62\63\3\2\2\2\63\61"+
		"\3\2\2\2\63\64\3\2\2\2\64\20\3\2\2\2\65\66\t\5\2\2\66\22\3\2\2\2\678\t"+
		"\6\2\28\24\3\2\2\29:\5\27\f\2:;\3\2\2\2;<\b\13\2\2<\26\3\2\2\2=>\7\61"+
		"\2\2>?\7\61\2\2?C\3\2\2\2@B\n\7\2\2A@\3\2\2\2BE\3\2\2\2CA\3\2\2\2CD\3"+
		"\2\2\2D\30\3\2\2\2EC\3\2\2\2\b\2\37%.\63C\3\b\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}