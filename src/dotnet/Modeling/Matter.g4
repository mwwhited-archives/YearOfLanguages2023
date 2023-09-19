
grammar Matter;

models : model+ EOF;

model 
    : identity ( WS* ':'  WS* base=definition)? EOL+
        property+ EOL+
    ;

definition
    : identity
    // | identity '[' ']'
    // | identity '?'
    ;

property : PREFIX identity WS* ':' WS* type=definition EOL+;


identity : WORD;
 

// type : 
//     type_definition EOL*
//     PREFIX members=type_member EOL*
//     ;

// type_definition : name=WORD (':' base=WORD)?;
// type_member 
//     : name=WORD ':' reference=type_reference #property
//     // | rule_member
//     ;
// type_reference : name=WORD set=type_set_rule?;
// type_set_rule 
//     : ('[]' | '[*]'| '[-]')
//     | '[+]'                
//     | '[' start=INTEGER? '-' end=INTEGER? ']' 
//     // | '[' start=INTEGER '..' end=INTEGER? ']'
//     // | '[' start=INTEGER? '..' end=INTEGER ']'
//     ;

// rule_member 
//     : name=RULE_NAME ':' rule_parameter? ()    
//     ; 
    
// rule_parameter
//     : rule_parameter_item
//     | '[' rule_parameter_item (',' rule_parameter_item)* ']'
//     ;

// rule_parameter_item 
//     : value=BUILTIN_ACCESS
//     | '\'' value=~('\'') '\'' 
//     ;

// BUILTIN_ACCESS
//     : 'everyone' 
//     | 'authenticated' 
//     | 'self'
//     | 'admin'
//     | 'system'
//     ;

// RULE_NAME
//     : '@' WORD;    

//     //  * `everyone` any authenticated or unauthenticated user
//     //   * `authenticated` any authenticated user
//     //   * `self` current user 
//     //   * `admin` user with system admin claim
//     //   * `system` a backend processor
//     //   * `'claim key$$value match'`

WORD : LETTER CHAR*;
CHAR : LETTER | DIGIT | [-._];
LETTER : [A-Za-z];   
DIGIT : [0-9];
INTEGER : DIGIT+;

PREFIX : WS+;
WS : [ \t];

EOL : [\r\n]; // -> channel (HIDDEN);

SKIP_
    : COMMENT -> skip
    ;

fragment COMMENT
 : '//' ~[\r\n\f]*
 ;
