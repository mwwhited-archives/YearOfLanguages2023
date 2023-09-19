// Generated from c:\Repos\mwwhited-learning\YearOfLanguages2023\src\dotnet\Modeling\MatterV0.g4 by ANTLR 4.9.2
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class MatterV0Parser extends Parser {
	static { RuntimeMetaData.checkVersion("4.9.2", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, BUILTIN_ACCESS=11, RULE_NAME=12, WORD=13, CHAR=14, LETTER=15, 
		DIGIT=16, INTEGER=17, PREFIX=18, WS=19, EOL=20, SKIP_=21;
	public static final int
		RULE_types = 0, RULE_type = 1, RULE_type_definition = 2, RULE_type_member = 3, 
		RULE_type_reference = 4, RULE_type_set_rule = 5, RULE_rule_member = 6, 
		RULE_rule_parameter = 7, RULE_rule_parameter_item = 8;
	private static String[] makeRuleNames() {
		return new String[] {
			"types", "type", "type_definition", "type_member", "type_reference", 
			"type_set_rule", "rule_member", "rule_parameter", "rule_parameter_item"
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

	@Override
	public String getGrammarFileName() { return "MatterV0.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public MatterV0Parser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	public static class TypesContext extends ParserRuleContext {
		public TerminalNode EOF() { return getToken(MatterV0Parser.EOF, 0); }
		public List<TypeContext> type() {
			return getRuleContexts(TypeContext.class);
		}
		public TypeContext type(int i) {
			return getRuleContext(TypeContext.class,i);
		}
		public TypesContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_types; }
	}

	public final TypesContext types() throws RecognitionException {
		TypesContext _localctx = new TypesContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_types);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(19); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(18);
				type();
				}
				}
				setState(21); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( _la==WORD );
			setState(23);
			match(EOF);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class TypeContext extends ParserRuleContext {
		public Type_memberContext members;
		public Type_definitionContext type_definition() {
			return getRuleContext(Type_definitionContext.class,0);
		}
		public TerminalNode PREFIX() { return getToken(MatterV0Parser.PREFIX, 0); }
		public Type_memberContext type_member() {
			return getRuleContext(Type_memberContext.class,0);
		}
		public List<TerminalNode> EOL() { return getTokens(MatterV0Parser.EOL); }
		public TerminalNode EOL(int i) {
			return getToken(MatterV0Parser.EOL, i);
		}
		public TypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_type; }
	}

	public final TypeContext type() throws RecognitionException {
		TypeContext _localctx = new TypeContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_type);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(25);
			type_definition();
			setState(29);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==EOL) {
				{
				{
				setState(26);
				match(EOL);
				}
				}
				setState(31);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(32);
			match(PREFIX);
			setState(33);
			((TypeContext)_localctx).members = type_member();
			setState(37);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==EOL) {
				{
				{
				setState(34);
				match(EOL);
				}
				}
				setState(39);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Type_definitionContext extends ParserRuleContext {
		public Token name;
		public Token base;
		public List<TerminalNode> WORD() { return getTokens(MatterV0Parser.WORD); }
		public TerminalNode WORD(int i) {
			return getToken(MatterV0Parser.WORD, i);
		}
		public Type_definitionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_type_definition; }
	}

	public final Type_definitionContext type_definition() throws RecognitionException {
		Type_definitionContext _localctx = new Type_definitionContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_type_definition);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(40);
			((Type_definitionContext)_localctx).name = match(WORD);
			setState(43);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__0) {
				{
				setState(41);
				match(T__0);
				setState(42);
				((Type_definitionContext)_localctx).base = match(WORD);
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Type_memberContext extends ParserRuleContext {
		public Type_memberContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_type_member; }
	 
		public Type_memberContext() { }
		public void copyFrom(Type_memberContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class PropertyContext extends Type_memberContext {
		public Token name;
		public Type_referenceContext reference;
		public TerminalNode WORD() { return getToken(MatterV0Parser.WORD, 0); }
		public Type_referenceContext type_reference() {
			return getRuleContext(Type_referenceContext.class,0);
		}
		public PropertyContext(Type_memberContext ctx) { copyFrom(ctx); }
	}

	public final Type_memberContext type_member() throws RecognitionException {
		Type_memberContext _localctx = new Type_memberContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_type_member);
		try {
			_localctx = new PropertyContext(_localctx);
			enterOuterAlt(_localctx, 1);
			{
			setState(45);
			((PropertyContext)_localctx).name = match(WORD);
			setState(46);
			match(T__0);
			setState(47);
			((PropertyContext)_localctx).reference = type_reference();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Type_referenceContext extends ParserRuleContext {
		public Token name;
		public Type_set_ruleContext set;
		public TerminalNode WORD() { return getToken(MatterV0Parser.WORD, 0); }
		public Type_set_ruleContext type_set_rule() {
			return getRuleContext(Type_set_ruleContext.class,0);
		}
		public Type_referenceContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_type_reference; }
	}

	public final Type_referenceContext type_reference() throws RecognitionException {
		Type_referenceContext _localctx = new Type_referenceContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_type_reference);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(49);
			((Type_referenceContext)_localctx).name = match(WORD);
			setState(51);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__1) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__5))) != 0)) {
				{
				setState(50);
				((Type_referenceContext)_localctx).set = type_set_rule();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Type_set_ruleContext extends ParserRuleContext {
		public Token start;
		public Token end;
		public List<TerminalNode> INTEGER() { return getTokens(MatterV0Parser.INTEGER); }
		public TerminalNode INTEGER(int i) {
			return getToken(MatterV0Parser.INTEGER, i);
		}
		public Type_set_ruleContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_type_set_rule; }
	}

	public final Type_set_ruleContext type_set_rule() throws RecognitionException {
		Type_set_ruleContext _localctx = new Type_set_ruleContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_type_set_rule);
		int _la;
		try {
			setState(64);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__1:
			case T__2:
			case T__3:
				enterOuterAlt(_localctx, 1);
				{
				setState(53);
				_la = _input.LA(1);
				if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__1) | (1L << T__2) | (1L << T__3))) != 0)) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				}
				break;
			case T__4:
				enterOuterAlt(_localctx, 2);
				{
				setState(54);
				match(T__4);
				}
				break;
			case T__5:
				enterOuterAlt(_localctx, 3);
				{
				setState(55);
				match(T__5);
				setState(57);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==INTEGER) {
					{
					setState(56);
					((Type_set_ruleContext)_localctx).start = match(INTEGER);
					}
				}

				setState(59);
				match(T__6);
				setState(61);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==INTEGER) {
					{
					setState(60);
					((Type_set_ruleContext)_localctx).end = match(INTEGER);
					}
				}

				setState(63);
				match(T__7);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Rule_memberContext extends ParserRuleContext {
		public Token name;
		public TerminalNode RULE_NAME() { return getToken(MatterV0Parser.RULE_NAME, 0); }
		public Rule_parameterContext rule_parameter() {
			return getRuleContext(Rule_parameterContext.class,0);
		}
		public Rule_memberContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_rule_member; }
	}

	public final Rule_memberContext rule_member() throws RecognitionException {
		Rule_memberContext _localctx = new Rule_memberContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_rule_member);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(66);
			((Rule_memberContext)_localctx).name = match(RULE_NAME);
			setState(67);
			match(T__0);
			setState(69);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__5) | (1L << T__9) | (1L << BUILTIN_ACCESS))) != 0)) {
				{
				setState(68);
				rule_parameter();
				}
			}

			{
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Rule_parameterContext extends ParserRuleContext {
		public List<Rule_parameter_itemContext> rule_parameter_item() {
			return getRuleContexts(Rule_parameter_itemContext.class);
		}
		public Rule_parameter_itemContext rule_parameter_item(int i) {
			return getRuleContext(Rule_parameter_itemContext.class,i);
		}
		public Rule_parameterContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_rule_parameter; }
	}

	public final Rule_parameterContext rule_parameter() throws RecognitionException {
		Rule_parameterContext _localctx = new Rule_parameterContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_rule_parameter);
		int _la;
		try {
			setState(85);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__9:
			case BUILTIN_ACCESS:
				enterOuterAlt(_localctx, 1);
				{
				setState(73);
				rule_parameter_item();
				}
				break;
			case T__5:
				enterOuterAlt(_localctx, 2);
				{
				setState(74);
				match(T__5);
				setState(75);
				rule_parameter_item();
				setState(80);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==T__8) {
					{
					{
					setState(76);
					match(T__8);
					setState(77);
					rule_parameter_item();
					}
					}
					setState(82);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(83);
				match(T__7);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Rule_parameter_itemContext extends ParserRuleContext {
		public Token value;
		public TerminalNode BUILTIN_ACCESS() { return getToken(MatterV0Parser.BUILTIN_ACCESS, 0); }
		public Rule_parameter_itemContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_rule_parameter_item; }
	}

	public final Rule_parameter_itemContext rule_parameter_item() throws RecognitionException {
		Rule_parameter_itemContext _localctx = new Rule_parameter_itemContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_rule_parameter_item);
		int _la;
		try {
			setState(91);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case BUILTIN_ACCESS:
				enterOuterAlt(_localctx, 1);
				{
				setState(87);
				((Rule_parameter_itemContext)_localctx).value = match(BUILTIN_ACCESS);
				}
				break;
			case T__9:
				enterOuterAlt(_localctx, 2);
				{
				setState(88);
				match(T__9);
				setState(89);
				((Rule_parameter_itemContext)_localctx).value = _input.LT(1);
				_la = _input.LA(1);
				if ( _la <= 0 || (_la==T__9) ) {
					((Rule_parameter_itemContext)_localctx).value = (Token)_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(90);
				match(T__9);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3\27`\4\2\t\2\4\3\t"+
		"\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\3\2\6\2\26"+
		"\n\2\r\2\16\2\27\3\2\3\2\3\3\3\3\7\3\36\n\3\f\3\16\3!\13\3\3\3\3\3\3\3"+
		"\7\3&\n\3\f\3\16\3)\13\3\3\4\3\4\3\4\5\4.\n\4\3\5\3\5\3\5\3\5\3\6\3\6"+
		"\5\6\66\n\6\3\7\3\7\3\7\3\7\5\7<\n\7\3\7\3\7\5\7@\n\7\3\7\5\7C\n\7\3\b"+
		"\3\b\3\b\5\bH\n\b\3\b\3\b\3\t\3\t\3\t\3\t\3\t\7\tQ\n\t\f\t\16\tT\13\t"+
		"\3\t\3\t\5\tX\n\t\3\n\3\n\3\n\3\n\5\n^\n\n\3\n\2\2\13\2\4\6\b\n\f\16\20"+
		"\22\2\4\3\2\4\6\3\2\f\f\2c\2\25\3\2\2\2\4\33\3\2\2\2\6*\3\2\2\2\b/\3\2"+
		"\2\2\n\63\3\2\2\2\fB\3\2\2\2\16D\3\2\2\2\20W\3\2\2\2\22]\3\2\2\2\24\26"+
		"\5\4\3\2\25\24\3\2\2\2\26\27\3\2\2\2\27\25\3\2\2\2\27\30\3\2\2\2\30\31"+
		"\3\2\2\2\31\32\7\2\2\3\32\3\3\2\2\2\33\37\5\6\4\2\34\36\7\26\2\2\35\34"+
		"\3\2\2\2\36!\3\2\2\2\37\35\3\2\2\2\37 \3\2\2\2 \"\3\2\2\2!\37\3\2\2\2"+
		"\"#\7\24\2\2#\'\5\b\5\2$&\7\26\2\2%$\3\2\2\2&)\3\2\2\2\'%\3\2\2\2\'(\3"+
		"\2\2\2(\5\3\2\2\2)\'\3\2\2\2*-\7\17\2\2+,\7\3\2\2,.\7\17\2\2-+\3\2\2\2"+
		"-.\3\2\2\2.\7\3\2\2\2/\60\7\17\2\2\60\61\7\3\2\2\61\62\5\n\6\2\62\t\3"+
		"\2\2\2\63\65\7\17\2\2\64\66\5\f\7\2\65\64\3\2\2\2\65\66\3\2\2\2\66\13"+
		"\3\2\2\2\67C\t\2\2\28C\7\7\2\29;\7\b\2\2:<\7\23\2\2;:\3\2\2\2;<\3\2\2"+
		"\2<=\3\2\2\2=?\7\t\2\2>@\7\23\2\2?>\3\2\2\2?@\3\2\2\2@A\3\2\2\2AC\7\n"+
		"\2\2B\67\3\2\2\2B8\3\2\2\2B9\3\2\2\2C\r\3\2\2\2DE\7\16\2\2EG\7\3\2\2F"+
		"H\5\20\t\2GF\3\2\2\2GH\3\2\2\2HI\3\2\2\2IJ\3\2\2\2J\17\3\2\2\2KX\5\22"+
		"\n\2LM\7\b\2\2MR\5\22\n\2NO\7\13\2\2OQ\5\22\n\2PN\3\2\2\2QT\3\2\2\2RP"+
		"\3\2\2\2RS\3\2\2\2SU\3\2\2\2TR\3\2\2\2UV\7\n\2\2VX\3\2\2\2WK\3\2\2\2W"+
		"L\3\2\2\2X\21\3\2\2\2Y^\7\r\2\2Z[\7\f\2\2[\\\n\3\2\2\\^\7\f\2\2]Y\3\2"+
		"\2\2]Z\3\2\2\2^\23\3\2\2\2\16\27\37\'-\65;?BGRW]";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}