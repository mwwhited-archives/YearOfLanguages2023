﻿CARDIAC
Background
The acronym CARDIAC stands for "CARDboard Illustrative Aid to Computation." It was developed by David Hagelbarger at Bell Labs as a tool for teaching how computers work in a time when access to real computers was extremely limited. The CARDIAC kit consists of a folded cardboard "computer" and an instruction manual. In July 1969, the Bell Laboratories Record contained an article describing the system and the materials being made available to teachers for working with it.

As illustrated in the following pictures, the CARDIAC computer consisted of a left-hand CPU section and a right-hand memory section. On the CPU side there are five sliders:

One slider of input "cards"
One slider for the accumulator sign
Three sliders for the digits of an instruction
The memory side has a single slider of output "cards." Portions of the sliders show through cutouts in the card frame. The cutouts for the input and output card sliders each show the current card to be read or written. The combination of the accumulator sign and the three instruction sliders show steps through cutouts that describe the operation of the selected instruction. Effectively, the sliders and cutouts are the instruction decoder of the CPU. Finally, each memory location has a hole in it. A small carboard ladybug serves as the program counter which is moved from location to location in response to the steps described on the CPU side.

The CARDIAC manual is 50+ pages divided into 16 sections describing the basics of computers from a late 1960s perspective. The first six sections cover things like flow charts, instructions, data, addresses, and the stored program concept. Sections 7–12 discuss the CARDIAC and some basic programming techniques including loops and multiplication. Sections 13 and 14 discuss the techniques for bootstrapping and subroutines, both of which we elaborate on below. Section 15 focuses on the development of a program to play NIM. Finally, Section 16 discusses assemblers and compilers. Although there is some duplication of information, the material on this page is not intended to replace the manual. Rather, the material here expands on that in the manual, particularly from the point of view of one who is already familiar with the most basic concepts of computers and programming.

Pictures
Click on these pictures for larger versions.

Book and "Computer"

Open CARDIAC

CARDIAC Architecture
Memory
The CARDIAC has a grand total of 100 memory locations identified by the two-digit decimal numbers 00 through 99. Each memory location holds a signed three-digit decimal numer. (With the exception of a single code example, the CARDIAC book is actually silent on whether memory contains signed or unsigned values.) Locations 00 and 99 are special. Location 00 always contains the value 001, which as we see below is the instruction to read a card into location 01. This special value is used the the bootstrapping process discussed later. Location 99 always contains a value between 800 and 899. The tens and ones digits of the number are the value of the program counter after a jump instruction is executed. This provides the mechanism for a return from subroutine.

CPU
The CARDIAC CPU is a single-accumulator single-address machine. Thus each instruction operates optionally on a single memory location and the accumulator. For example, the ADD instruction reads the data in one memory location, adds it to the current value of the accumulator and stores the result back into the accumulator. The ALU supports addition, subtraction, and decimal shifting. CARDIAC's CPU architecture is illustrated in the following figure:


The CARDIAC accumulator holds a signed 4-digit number, which seems odd given that everything else is oriented around 3-digit numbers. The manual includes the statement:

Since CARDIAC's memory can store only 3-digit numbers, you may be puzzled by the inclusion of an extra square in the accumulator. It is there to handle the overflow that will result when two 3-digit numbers whose sum exceeds 999 are added.
What's not clear is under what conditions that overflow/carry digit is kept or discarded. From the discussion of the SFT instruction in Section 12 of the manual, exactly four digits are kept for the intermediate value between the left and right shift operations. However, the manual doesn't state whether all four digits are kept between instructions nor what happens when storing the accumulator to memory if the accumulator contains a number whose magnitude is greater than 999. In the case of our simulator, we retain all four digits, effectively implementing a 4-digit ALU. However, when storing the accumulator to memory, we discard the fourth digit. I.e. the number stored in memory is a mod 1000, where a is the contents of the accumulator.

I/O
The CARDIAC has exactly one input device and one output device. These are a card reader and a card punch. Unlike real punch cards, the CARDIAC input and output cards can each hold exactly one signed three-digit number. When a card is read by way of the INP instruction, it is taken into the card reader and removed from the stack of cards to be read. Similarly, on each OUT instruction, a new card is "punched" with the specified value on it, and the card moved to the output card stack.

Instruction Set
The CARDIAC's instuction set has only 10 instructions, each identified by an operation code (opcode) of 0 through 9. The instructions are as follows:

Opcode	Mnemonic	Operation
0	INP	Read a card into memory
1	CLA	Clear accumulator and add from memory (load)
2	ADD	Add from memory to accumulator
3	TAC	Test accumulator and jump if negative
4	SFT	Shift accumulator
5	OUT	Write memory location to output card
6	STO	Store accumulator to memory
7	SUB	Subtract memory from accumulator
8	JMP	Jump and save PC
9	HRS	Halt and reset
Encoding
All instructions are non-negative numbers expressed as three-digit decimal numerals. The CARDIAC manual doesn't describe what happens if an attempt is made to execute a negative instruction. In our simulator, we treat negative instructions as no-ops (i.e. they are ignored and the program continues on to the next instruction). The operation code is the most significant of those three digits, i.e., o=⌊i /100⌋, where i is the contents of the instruction register (IR) loaded from the memory location specified by the PC. For most instructions, the lower-order digits are the address of the operand, i.e. a=i mod 100. This arrangement is illustrated in the following figure.


In the cases of the INP and STO instructions, a is the destination address for the data coming from either an input card or the accumulator, respectively. In the cases of the CLA, ADD, OUT, and SUB instructions, a is the source address of the second operand to the ALU or the source address of the operand being written to an output card. For the TAC, JMP, and HRS instructions, a is the address to be loaded into the PC (conditionally, in the case of the TAC instruction). The remaining instruction, SFT, doesn't treat the lower-order digits as an address. Instead, each of the lower-order digits is a number of digit positions to shift first left, then right. The left shift count is given by l=⌊a /10⌋, and the right shift count is given by r=a mod 10. The instruction format for the SFT instruction is shown in the following figure:


Instruction Execution
The instructions operate as described here. In this discussion, we use the following notation:

Notation	Meaning
ACC	Contents of the accumulator
PC	Contents of the program counter
a	Operand address as described in the previous subsection
MEM[x]	Contents of memory location x
INPUT	Contents of one card read from the input
OUTPUT	Contents of one card written to the output
INP
The INP instruction reads a single card from the input and stores the contents of that card into the memory location identified by the operand address. (MEM[a] ← INPUT)
CLA
This instruction causes the contents of the memory location specified by the operand address to be loaded into the accumulator. (ACC ← MEM[a])
ADD
The ADD instruction takes the contents of the accumulator, adds it to the contents of the memory location identified by the operand address and stores the sum into the accumulator. (ACC ← ACC + MEM[a])
TAC
The TAC instruction is the CARDIAC's only conditional branch instruction. It tests the accumulator, and if the accumulator is negative, then the PC is loaded with the operand address. Otherwise, the PC is not modified and the program continues with the instruction following the TAC. (If ACC < 0, PC ← a)
SFT
This instruction causes the accumulator to be shifted to the left by some number of digits and then back to the right some number of digits. The amounts by which it is shifted are shown above in the encoding for the SFT instruction. (ACC ← (ACC × 10^l) / 10^r)
OUT
The OUT instruction takes the contents of the memory location specified by the operand address and writes them out to an output card. (OUTPUT ← MEM[a])
STO
This is the inverse of the CLA isntruction. The accumulator is copied to the memory location given by the operand address. (MEM[a] ← ACC)
SUB
In the SUB instruction the contents of the memory location identified by the operand address is subtracted from the contents of the accumulator and the difference is stored in the accumulator. (ACC ← ACC − MEM[a])
JMP
The JMP instruction first copies the PC into the operand part of the instruction at address 99. So if the CARDIAC is executing a JMP instruction stored in memory location 42, then the value 843 will be stored in location 99. Then the operand address is copied into the PC, causing the next instruction to be executed to be the one at the operand address. (MEM[99] ← 800 + PC; PC ← a)
HRS
The HRS instruction halts the CARDIAC and puts the operand address into the PC. (PC ← a; HALT)
Assembly Language
All of the code fragments and complete program examples on this page are shown in an assembly language format with each line organized into six columns:

Address: The first column shows the memory address respresented by that line.
Contents: In the second column, we put the number that is stored in that memory location.
In most cases, this is a instruction, but for lines with a DATA pseudo-op, it is a data value.
Label: The third column contains an optional label on the memory location, allowing it to be identified by name, rather than by address.
Opcode: Instruction mnemonics are places in the fourth column. In addition to the ten instructions discussed above, we will use on pseudo-op (or assembler directive), DATA. For memory locations containing a DATA item, the operand is the literal data value stored in the memory location, rather than an operand for an instruction. This pseudo-op is particularly useful when labeled for creating variables.
Operand: The fifth column is the operand part of the instruction or the literal data for a DATA directive. Numerical operands are included directly in the address field of the instruction. When a label name appears as an oeprand, the memory address associated with that label is placed in the address field of the instruction.
Comment: Any desired descriptive text can be placed after the operand.
Indirection, Indexing, and Pointers
Notice that the only way of specifying the address of a memory location we want to use is in the instruction itself. Most comptuer architectures provide a mechanism whereby the address we want to use can be stored in a register or another memory location. Variables which contains memory addresses are usually referred to as pointers.

Indirect Loads
Even though the CARDIAC doesn't have hardware support for using pointers directly, we can still do simple indirect addressing. Suppose we have a variable stored in a memory location called ptr and it has the value 42 in it. Now if we want to load the accumulator with the contents of memory location 42, we can do something like:

05	100	loader	DATA	100
06	042	ptr	DATA	042

20	105		CLA	loader
21	206		ADD	ptr
22	623		STO	indload
23	100	indload	CLA	00
Notice that even though we have specified that we will load from location 00 in the instruction at location 23, we will have changed it to load from location 42 by the time we run execute that instruction. For that matter, it doesn't matter if we've loaded anything into location 23 before starting this sequence. It will get set before we use it.

Indirect Stores
Storing the accumulator to a memory location identified by a pointer is similar. We just have to be careful not to lose the value we want to store while we're fiddling about with the store instruction and in the following bit of code:

05	600	storer	DATA	600
06	042	ptr	DATA	042
07	000	acc	DATA	000

20	607		STO	acc
21	105		CLA	storer
22	206		ADD	ptr
23	625		STO	indstor
24	107		CLA	acc
25	600	indstor	STO	00
Array Indexing
Often we aren't so much interested in a pointer that identifies a single memory location as we are in an array of memory locations we can refer to by index. We will identify our array locations starting at index 0. So the first element of the array is at index 0, the second at index 1, and so on. If we have a variable called base that holds the first address of the array, then we can just add the base and the index together to get the address of a particular element. This is just a slight modification of the indirect accesses above. In particular, to load from an array element:

05	100	loader	DATA	100
06	042	base	DATA	042
07	000	index	DATA	000

20	105		CLA	loader
21	206		ADD	base
22	207		ADD	index
23	624		STO	arrload
24	100	arrload	CLA	00
and for storing to an array element:

05	600	storer	DATA	600
06	042	base	DATA	042
07	000	index	DATA	000
08	000	acc	DATA	000

20	608		STO	acc
21	105		CLA	storer
22	206		ADD	base
23	207		ADD	index
24	626		STO	arrstor
25	108		CLA	acc
26	600	arrstor	STO	00
If we're dealing with only one array, we could eliminate one add instruction from each sequence by pre-adding the base and loader and pre-adding the base and storer.

Stacks
Another use of indirect address is the stack data structure. If you're not familiar with a stack, think of it like a stack of plates in a cafateria. A plate is always placed on top of the stack. Likewise, the one removed is always the one on the top of the stack. We refer to the process of putting an element onto a stack as pushing and the process of taking an element off of a stack as popping. Note that we always pop that most recently pushed element. Because of this, the stack is often referred to as a last-in, first-out (LIFO) data structure. Pushing and popping are very similar to storing and loading indirectly, except that we must also adjust the value of the pointer that identifies the top of the stack. In the following code we'll use a memory location named tos (for top-of-stack) for the pointer. Also, we'll do as is often done in hardware stacks and let the stack grow downward. That is to say, as we push data onto the stack, the stack pointer moves toward lower memory addresses. With that in mind, here is a fragment of code for pushing the accumulator onto the stack:

05	600	storer	DATA	600
06	100	loader	DATA	100
07	089	tos	DATA	089
08	000	acc	DATA	000

20	608		STO	acc
21	107		CLA	tos
22	205		ADD	storer
23	628		STO	stapsh
24	107		CLA	tos
25	700		SUB	00
26	607		STO	tos
27	108		CLA	acc
28	600	stapsh	STO	00
And similarly to pop from the top of the stack:

20	107		CLA	tos
21	200		ADD	00
22	607		STO	tos
23	206		ADD	loader
24	625		STO	stapop
25	100	stapop	CLA	00
These code fragments (slightly modified) are used in the example below that uses the LIFO properties of the stack to reverse the order of a list of numbers on the input cards.

Subroutines
There are many reasons why we might wish to subdivide a program into a number of smaller parts. In the context of higher level languages and methodologies, these subdivisions are often referred to by names like procedures, functions, and methods. All of these are types of subroutines, the name we usually use when working at the hardware or machine language level. In these sections, we look at the techniques for creating and using subroutines on the CARDIAC. Each subsection progressively builds from the simplest subroutine technique to more complex and advanced techiques. Don't worry if not all of it makes sense on a first reading. You can get a good sense of the general idea of subroutines without necessarily understanding the details of how recursion is implemented on a machine as limited as the CARDIAC.

Single Simple Subroutines
In the CARDIAC, the JMP instruction is effectively a jump-to-subroutine instruction, storing the return address in location 99. Because the address stored in location 99 is prefixed by the opcode 8, the instruction in that location becomes a return-from-subroutine instruction. Thus any segment of code whose last instruction is at location 99 can be called as a subroutine, simply by jumping to its first instruction. For example, a simple routine to double the value of the accumulator could be coded as:

96	000	accval	DATA	000

97	696	double	STO	accval
98	296		ADD	accval
99	800		JMP	00
and the subroutine can be called with a jump to double:

	897		JMP	double
Multiple Subroutines
Clearly, if our subroutine executes a JMP instruction or if it calls another subroutine, then we will lose our return address, because it will be overwritten by the JMP instruction. Along similar lines, if we have more than one subroutine in our program, only one of them can be at the end of the memory space and flow directly into location 99.

As a result, in many cases, we'll need a more involved subroutine linkage mechanism. One way to accomplish this is to save the return address somewhere and restore it when needed. If we use this method, we'll have to devise a mechansism to transfer control to location 99 with the right return address. Although location 99 can itself be used as the return from subroutine instruction, it doesn't have to be. In many cases, it will be easier to copy it to the end of our actual subroutine. Using this approach, we can write a subroutine that outputs the value of the accumulator as follows:

80	686	aprint	STO	86
81	199		CLA	99
82	685		STO	aexit
83	586		OUT	86
84	186		CLA	86
85	800	aexit	JMP	00
Similarly, our doubling routine would look like:

90	696	double	STO	96
91	199		CLA	99
92	695		STO	dexit
93	196		CLA	96
94	296		ADD	96
95	800	dexit	JMP	00
See below for an example of a program that uses these subroutines to produce a list of powers of two.

Recursion
There's one more limitation on subroutines still in the techniques we have developed. What happens if a subroutine calls itself? You might reasonably as, is it even useful for a function call itself? The answer is, yes, and it called recursion.

The key to making it possible for a subroutine to call itself is to realize that no matter where we're called from, we always want to return to the place from which we were most recently called that we haven't already returned to. That should sound familiar. We should use the return addresses in the same LIFO order that a stack provides. In other words, when we call a recursive subroutine, we want to push the return address onto a stack and then pop it back off when we return from that subroutine. With a little reflection, we can see that this approach applies to all subroutine calls, not just to those that are recursive. This is why pushing return addresses on a stack is the basis for hardware subroutine call support in most architectures since about the 1970s on.

On the CARDIAC, we can implement this technique with a modification of the multiple subroutine technique above. When entering a subroutine, rather than copying location 99 to the return from subroutine instruction, we push the contents of location 99 onto the stack. Then when we're about to return from the subroutine, we pop the return address off the stack into the return from subroutine instruction. So our code would look something like:

	1xx		CLA	tos
	2yy		ADD	storer
	6zz		STO	stapsh
	1xx		CLA	tos
	700		SUB	00
	6xx		STO	tos
	199		CLA	99
zz	600	stapsh	STO	00
	 .
	 .		body of the subroutine
	 .
	1xx		CLA	tos
	200		ADD	00
	6xx		STO	tos
	2ww		ADD	loader
	6ss		STO	stapop
ss	100	stapop	CLA	00
	6rr		STO	rts
rr	800	rts	JMP	00
There's one more aspect of recursive subroutines that is also suitable for other subroutines as well. In particular, subroutines often need input data passed to them by whatever code has called them or temporary variables that are needed during the course of their operation. If a subroutine is not recursive, we can get away with just allocating some fixed memory locations for these. However, in the case of recursive subroutines, we need to make sure that we have fresh ones for each time the subroutine is called and not overwrite the ones that might still be needed by other instances we might return back to. The most natural way to handle this is to allocate them on the stack along with the return address.

Putting all these things together, we can summarize the steps for calling a subroutine in the most general cases:

Before calling the subroutine, we push any inputs (also called arguments or parameters) onto the stack.
Transfer control to the first instruction of the subroutine, saving the PC (which holds the return address) in the process.
If the hardware has not already saved the PC onto the stack, the first thing we do in the subroutine is copy it to the stack.
Move the stack pointer to resever space on the stack for any temporary (local) variables the subroutine will need.
Before returning, the subroutine readjusts the stack pointer to remove the temporary variables it allocated.
If the hardware does not already expect the return address to be on the stack, we need to pop it off the stack and copy it back to where it does need to be.
Return control from the subroutine back to the code that called it.
Finally, the calling code adjusts the stack pointer to remove the arguments it pushed onto the stack before calling the subroutine.
Bootstrapping
Like many of the early system designs, the mechanism for loading an initial program into the CARDIAC and getting it running involves a small amount of hardware support and a lot of cleverness. The whole enterprise is often somewhat remenescent of the image of a person attempting to lift themselves off the ground by pulling on their own bootstraps. This is why we usually refrer to the process as bootstrapping or often just booting.

The CARDIAC's hardware support for bootstrapping is the fixed value of memory location 00. The fixed contents of this memory location are 001 which is the instruction to load a single card into location 01. Naturally, after executing this instruction, the PC moves to location 01 and executes the instruction on the card just read. But what do we put on that card to load? The answer is 002, which is the instruction to load a card into location 02. This causes us to load a second card into location 02 and execut it. At first glance, it would seem we haven't really improved things any, because we're right back where we're still just loading a single card and executing it. But here's where the first bit of cleverness comes in. The card we load into location 02 has the value 800 on it which causes us to jump back to location 00 which will load another card into location 01. We now have a loop that loads cards into location 01 and executes them. If the instructions we put into location 01 are reads of cards into other memory locations, we now have a little program that reads a whole set of cards into memory. Conveniently, a card containing just a memory address also contains the instruction to read a card into that memory address. So if the next card we read after the 800 is, say, 010, then location 01 will be changed to an instruction to read a card into location 10, after which we'll execut the 800 instruction to jump back to location 00 and do it all over again. This means that after the 002 and 800 cards, we can have pairs of cards where the first of the pair is the address where we want to put some data, and the second of the pair is the data to put there.

If this is all we did, we'd read all the remaining cards into memory and then the computer would halt when there were no more cards to read. But there's another trick we can play. If we make the last address-data card pair change location 02 from 800 to a jump to the first instruction of our program, the loader loop will stop and control will transfer to the program we just loaded. So after all of our address-data card pairs, we'll append the cards 002 and 8xx where xx is the address of the first instruction of our program. The net effect is that we can now load a program and start running it without any manual intervention.

The last piece of this puzzle is how do we include the data we want the program to operate on? It turns out, that's a simple as just appending the data after the 002 and 8xx cards. When control transfers to the program we loaded, any remaining cards will still be in the reader waiting to be read. When the program executes its first INP instruction, it will happily read the next card, not knowing that there were a bunch of other cards read ahead of it.

So putting all the pieces together, we bootstrap the CARDIAC by putting together a card deck that looks like:

002
800
 .
 .	address-data card pairs
 .
002
8xx	where xx is address of the first instruction
 .
 .	data cards
 .
Then we put that deck into the card reader, and start the computer at address 00. The CARDIAC will first load the two-card bootstrap loader, then load the program into memory, then transfer control to the newly loaded program. If the program itself also includes INP instructions, they read the remaining data cards.

Simulator
We have developed a CARDIAC simulator suitable for running the code discussed on this page. All of the examples in the next section have been tested using this simulator.

To avoid any unnecessary requirements on screen layout, the simulator is laid out a little differently than the physical CARDIAC. At the top of the screen is the CARDIAC logo from a photograph of the actual unit. This picture is also a link back to this page. The next section of the screen is the CARDIAC memory space as appears on the right hand side of the physical device. When the simulator starts up, the value 001 in location 00 and the value 8-- in location 99 are preloaded. As a simplification, we don't use a picture of a ladybug for the program counter, but instead highlight the memory location to which the PC points with a light green background. Each memory location is editable (including the ones that are intended to be fixed), and the tab key moves focus down each column in memory address order.

The bottom section of the simulator is the I/O and CPU. Input is divided into two text areas. The first is the card deck and is editable. The second area is the card reader, and as cards are consumed by the reader they are removed from the listing in the reader. Cards in the deck are loaded into the reader with the Load button. Output cards appear in the Output text area as they are generated with the OUT instruction.

The CPU section of the simulator has four parts showing the status of the CPU and buttons for control. On the top of the CPU section, the Program Counter is shown in an editable text box. Below that is the instruction decoder with non-editable text boxes showing the contents of the Instruction Register and a breakdown of the instruction decoding in the form of an opcode mnemonic and numeric operand. The Accumulator is shown below the instuction decoder. Below the register display are six buttons that control the operation of the simulator:

Reset
The Reset button clears the instruction register, resets the PC and accumulators to 0 and clears the output card deck.
Clear Mem
This button resets all memory locations to blank and re-initializes location 00 to 001 and location 99 to 8--.
Step
Clicking on the Step button causes the simulator to execute the single instruction highlighted in the memory space as pointed to by the program counter. Upon completion of the instruction, the screen is updated to show the state of the computer after the instruction.
Slow
The Slow button causes the simulator to begin executing code starting at the current PC. Instructions are executed at the rate of 10 per second with the screen being updated after each instruction. When the program is run in this way, the movement of the highlighted memory shows the flow of control in the program very clearly.
Run
In the current version of the simulator, the Run button causes the program to be executed beginning from the current PC at the full speed of the JavaScript interpreter. Because of the way JavaScript is typically implemented, the screen contents will not show the effects of code execution until the simulator executes the HRS instruction and the program halts.
Halt
Pressing the Halt button while the program is running in slow mode causes the simulator to stop after the current instruction. The state of the machine remains intact and can be continued with any of the Step, Slow, or Run buttons.
Examples
The remainder of this page are a number of examples of programs written for the CARDIAC. They have all been tested using the simulator described above. Because the memory space of the CARDIAC is so limited, none of the programs are particularly complex. You won't find a compiler, operating system, or web browser here. However, we do have a few of more complexity than you might expect. There's a pretty simple program for generating a list of the powers of 2. There's one that recursively solves the Towers of Hanoi problem. For each of them, we include the assembly language source code with assembled machine language code and a card deck suitable for bootstrapping on the CARDIAC.

Note that most of these examples aren't the most compact way of solving the problem. Rather, they illustrate techniques as described through this page. The primary exception is the Towers of Hanoi solution which requried some effort to squeeze it into the limited memory space of the CARDIAC.

When we take these programs and turn them into decks of cards to be bootstrapped on the CARDIAC, we get the card decks listed below the program listings. If you cut and paste the list into the input deck of the simulator, hit load, and hit slow, you can see the program get loaded into memory and run.

Count from 1 to 10
This is sort of our CARDIAC version of "Hello World." Our objective is simply to print out a set of output cards with the values 1 to 10. We keep two variables to control the process. One, called n keeps track of how many cards we still have left to print. At any point in time it represents that we need to print n+1 more cards. We also have a variable called cntr wich is the number to print out. Each time through the loop, we check to see if n is negative and if so, we're done. If not, we decrement it, print cntr and then increment cntr.

Program Listing
04	009	n	DATA	009
05	000	cntr	DATA	000

10	100		CLA	00	Initialize the counter
11	605		STO	cntr	
12	104	loop	CLA	n	If n < 0, exit
13	322		TAC	exit
14	505		OUT	cntr	Output a card
15	105		CLA	cntr	Increment the card
16	200		ADD	00
17	605		STO	cntr
18	104		CLA	n	Decrement n
19	700		SUB	00
20	604		STO	n
21	812		JMP	loop
22	900	exit	HRS	00
Card Deck
002
800
010
100
011
605
012
104
013
322
014
505
015
105
016
200
017
605
018
104
019
700
020
604
021
812
022
900
004
009
002
810
List Reversal
Our next example uses the stack techniques described above to take in a list of cards and output the same list in reverse order. The first card in the input deck (after the bootstrapping and the program code) is the count of how many cards we're operating on. The remainder of the input deck are the cards to reverse. In the example card deck, we are reversing the first seven Fibonacci numbers.

Program Listing
04	600	storer	DATA	600
05	100	loader	DATA	100
06	089	tos	DATA	089	Stack pointer
07	000	acc	DATA	000	Temp for saving accumulator
08	000	n1	DATA	000	Write counter
09	000	n2	DATA	000	Read counter

10	008		IN	n1	Get the number of cards to reverse
11	108		CLA	n1	Initialize a counter
12	609		STO	n2
13	109	rdlp	CLA	n2	Check to see if there are any more cards to read
14	700		SUB	00
15	327		TAC	wrlp
16	609		STO	n2
17	007		IN	acc	Read a card
18	106		CLA	tos	Push it onto the stack
19	204		ADD	storer
20	625		STO	stapsh
21	106		CLA	tos
22	700		SUB	00
23	606		STO	tos
24	107		CLA	acc
25	600	stapsh	STO	00
26	813		JMP	rdlp
27	108	wrlp	CLA	n1	Check to see if there are any more cards to write
28	700		SUB	00
29	339		TAC	done
30	608		STO	n1	
31	106		CLA	tos	Pop a card off the stack
32	200		ADD	00
33	606		STO	tos
34	205		ADD	loader
35	636		STO	stapop
36	100	stapop	CLA	00
37	890		JMP	aprint	Output a card
38	827		JMP	wrlp
39	900	done	HRS	00

90	696	aprint	STO	96	Write a card containing the contents of the accumulator
91	199		CLA	99
92	695		STO	aexit
93	596		OUT	96
94	196		CLA	96
95	800	aexit	JMP	00
Card Deck
002
800
004
600
005
100
006
089
007
000
008
000
009
000
010
008
011
108
012
609
013
109
014
700
015
327
016
609
017
007
018
106
019
204
020
625
021
106
022
700
023
606
024
107
025
600
026
813
027
108
028
700
029
339
030
608
031
106
032
200
033
606
034
205
035
636
036
100
037
890
038
827
039
900
090
696
091
199
092
695
093
596
094
196
095
800
002
810
007
001
001
002
003
005
008
013
Powers of 2
This is a slightly more interesting version of the list from 1 to 10. In this case, we are printing the powers of 2 from 0 to 9. The main difference is that instead of incrementing the number to output, we call a subroutine that doubles it. The program illustrates the use of multiple subroutines as discussed above.

Program Listing
04	000	n	DATA	000
05	009	cntr	DATA	009

10	100		CLA	00	Initialize the power variable with 2^0
11	880		JMP	aprint
12	604	loop	STO	n
13	105		CLA	cntr	Decrement the counter
14	700		SUB	00
15	321		TAC	exit	Are we done yet?
16	605		STO	cntr
17	104		CLA	n
18	890		JMP	double	Double the power variable
19	880		JMP	aprint	Print it
20	812		JMP	loop
21	900	exit	HRS	00

80	686	aprint	STO	86	Print a card with the contents of the accumulator
81	199		CLA	99
82	685		STO	aexit
83	586		OUT	86
84	186		CLA	86
85	800	aexit	JMP	00

90	696	double	STO	96	Double the contents of the accumulator
91	199		CLA	99
92	695		STO	dexit
93	196		CLA	96
94	296		ADD	96
95	800	dexit	JMP	00
Card Deck
002
800
005
009
010
100
011
880
012
604
013
105
014
700
015
321
016
605
017
104
018
890
019
880
020
812
021
900
080
686
081
199
082
685
083
586
084
186
090
696
091
199
092
695
093
196
094
296
002
810
Towers of Hanoi

By far the most complex example we include is a solution to the Towers of Hanoi problem. The puzzle consists of three posts on which disks can be placed. We begin with a tower of disks on one post with each disk smaller than the one below it. The other two posts are empty. The objective is to move all of the disks from one post to another subject to the following rules:

Only one disk at a time may be moved.
No disk may be placed on top of a smaller disk.
According to legend, there is a set of 64 disks which a group of monks are responsible for moving from one post to another. When the puzzle with 64 disks is finally solved, the world will end.

Although the puzzle sounds like it would be difficult to solve, it's very easy if we think recursively. Moving n disks from Post a to Post b using Post c as a spare can be done as follows:

Move n−1 disks from Post a to Post c.
Move one disk from Post a to Post b.
Move n−1 disks from Post c to Post b.
The CARDIAC doesn't have enough memory to solve a 64-disk puzzle, but we can solve smaller instances of the problem. In particular, the program we show here can solve up to six disks. The actual number of disks to solve is given by the first data card, and the initial assignment of source destination and spare posts is given on the second data card. The post assignments as well as the output encoding are shown in the following table.

Output	Disk Move
000	1 → 3
001	2 → 3
002	3 → 2
003	3 → 1
004	2 → 1
005	1 → 2
For example, the post assignments indicated by a card with the value 3 are that Post 3 is a, Post 2 is c and Post 1 is b. Similarly, an output card with 3 indicates that we are to move a disk from Post 3 to Post 1.

Before trying to understand the details of this program, note that there are several tricks used to reduce the memory usage. The amount of memory available for the stack allows for a puzzle of up to six disks to be solved with this program. Be aware, however, that slow running this program on six disks takes the better part of a half hour to run.

Program Listing
03	031	tos	DATA	031
04	100	loader	DATA	100
05	600	storer	DATA	600
06	107	r2ld	DATA	r2
07	001	r2	DATA	001
08	000		DATA	000
09	005	five	DATA	005
10	004		DATA	004
11	003	three	DATA	003
12	002		DATA	002

34	033		INP	32	Get the number of disks from the cards
35	032		INP	31	Get the column ordering from the cards
36	838		JMP	tower	Call the tower solver
37	900		HRS

38	199	tower	CLA	99	Push the return address on the stack
39	890		JMP	push
40	111		CLA	three	Fetch n from the stack
41	870		JMP	stkref
42	700		SUB	00	Check for n=0
43	366		TAC	towdone
44	890		JMP	push	Push n-1 for a recursive call
45	111		CLA	three	Get the first recursive order
46	870		JMP	stkref
47	669		STO	t1
48	109		CLA	five
49	769		SUB	t1
50	890		JMP	push
51	838		JMP	tower	Make first recursive call
52	880		JMP	pop
53	111		CLA	three	Get move to output
54	870		JMP	stkref
55	669		STO	t1
56	569		OUT	t1
57	111		CLA	three	Get second recursive order
58	870		JMP	stkref
59	206		ADD	r2ld
60	661		STO	t2
61	100	t2	CLA	00
62	890		JMP	push
63	838		JMP	tower	Make second recursive call
64	880		JMP	pop
65	880		JMP	pop
66	880	towdone	JMP	pop
67	668		STO	towret
68	800	towret	JMP	00

70	679	stkref	STO	refsav	Replace the accumulator with the contents
71	199		CLA	99	of the stack indexed by the accumulator
72	678		STO	refret
73	179		CLA	refsav
74	203		ADD	tos
75	204		ADD	loader
76	677		STO	ref
77	100	ref	CLA	00
78	800	refret	JMP	00

80	199	pop	CLA	99	Pop the stack into the accumulator
81	688		STO	popret
82	103		CLA	tos
83	200		ADD	00
84	603		STO	tos
85	204		ADD	loader
86	687		STO	popa
87	100	popa	CLA	00
88	800	popret	JMP	00

90	689	push	STO	pshsav	Push the accumulator on to the stack
91	103		CLA	tos
92	205		ADD	storer
93	698		STO	psha
94	103		CLA	tos
95	700		SUB	00
96	603		STO	tos
97	189		CLA	pshsav
98	600	psha	STO	00
Card Deck
002
800
003
031
004
100
005
600
006
107
007
001
008
000
009
005
010
004
011
003
012
002
034
033
035
032
036
838
037
900
038
199
039
890
040
111
041
870
042
700
043
366
044
890
045
111
046
870
047
669
048
109
049
769
050
890
051
838
052
880
053
111
054
870
055
669
056
569
057
111
058
870
059
206
060
661
061
100
062
890
063
838
064
880
065
880
066
880
067
668
068
800
070
679
071
199
072
678
073
179
074
203
075
204
076
677
077
100
078
800
080
199
081
688
082
103
083
200
084
603
085
204
086
687
087
100
088
800
090
689
091
103
092
205
093
698
094
103
095
700
096
603
097
189
098
600
002
834
003
000
Pythagorian Triples
The next example comes courtesy of Mark and Will Tapley. It finds sets of three integers which satisfy the Pythagorian property of x2+y2=z2.

Discussion
There is much motivation and explanation for this program at:

https://www.khanacademy.org/math/recreational-math/vi-hart/vi-cool-stuff/v/what-was-up-with-pythagoras

Subroutine to calculate square of a number:
In finding pythagorean triplets, the operation of squaring a number occurs very often, so the program uses a subroutine to perform this function.

Addresses 076–099 are loaded with the subroutine to utilize the return function hard-wired at address 099.
Addresses 072–075 are used for data storage for the subroutine.
Address 072 is loaded with the value 32, one larger than the largest allowable input. The calling program can test an input by subtracting this value from the prospective input and branching if the result is negative. (Negative value means legal input.)
Address 073 accepts the input to the subroutine. On return, the absolute value of the input will be in this location.
Address 074 is used as a counter during routine execution.
Address 075 will contain the calculated square, an integer between 0 and 961 inclusive.
Subroutine INPUT:
Store the number to be squared in address 073
Jump to address 077 (label SQmem in assembly listing)
-OR-

Load the number to be squared into the accumulator
Jump to address 076 (label SQacc in assembly listing)
Subroutine OUTPUT:
On return, the square of the input number is in address 075.

The subroutine has a single loop (addresses 090–098). In each loop, it subtracts one from a counter which is initially set to one greater than the input number N, then adds a copy of N into the output address. When the counter reaches 1, the output address contains the sum of N copies of N=N2 and the loop exits, returning program control to the location from which it was called (per the return capability special function of location 99).

Limitations:
The square of the input number must have 3 or fewer digits to comply with cell storage limitations. Therefore the input number is checked to be 31 or less (since 322=1024). Violating this condition will cause the subroutine to terminate execution (HRS) with the program counter pointing at location 086. The input number is converted from negative to positive if it was negative, so if the calling program needs a copy of the input, it should store it in some location other than Address 073 (SQIN). After the subroutine executes, that location will contain the absolute value of the input.

Main Program:
The main program searches over all allowable lengths of the shortest side S of the right triangles corresponding to pythagorean triplets. For each shortest side, it then searches over all possible lengths of the intermediate side L. For each combination of short and intermediate sides, it checks whether there is a hypotenuse H that satisfies the condition S2+L2=H2. The short side (S) search starts at 0, to avoid missing any triplets with very small values. (This results in identifying the degenerate triplet (0,1,1) which does satisfy 02+12=12 but does not really correspond to a right triangle.) The long side (L) search for each value of S starts at S+1, because L cannot equal S for an integer triplet (see URL above) and if L<S, the corresponding triplet should already have been found with a smaller S. (So, this program will identify (3,4,5) but will not identify (4,3,5).) The hypotenuse (H) search starts at 1.4 times S, since the minimum possible length of the hypotenuse is greater than the square root of 2 (1.404...) times S. (Note: 1.4 times S is calculated by shifting S right and then adding four copies of the result, which is truncated to an integer, to S. For S<10, the result is just S, so the search takes needlessly long until S≥10.)

With the starting values for S, L, and H, the program calculates S2 + L2−H2. If the result is <0, H is too long. In this case, the program increments L and tries again. If the result is =0, a triplet has been found and is printed out. The program then increments L and tries again. If the result is >0, H is too short. In this case, H is incremented and the program tries again. When H is long enough that no more triplets can be found for this value of S, the value of S is incremented, new L and H starting values are calculated, and the loop repeats.

Addresses 010–067 are loaded with the main program.
Addresses 004–009 are used for data storage.
Address 004 contains S, the smallest member of the triplet (length of the short leg of the triangle) and is initially set to 0.
Address 005 contains S2, calculated each time S is changed.
Address 006 contains L, the intermediate member of the triplet (length of the long "leg" of the triangle) and is re-initialized for each smallest member loop to one greater than the smallest member (which is always the minimum possible value for L; see above)
Address 007 contains L2, calculated each time L is changed.
Address 008 contains H, the largest member of the triplet (length of the hypotenuse of the triangle) and is initialized for each smallest member to a value <1.4×(the smallest value) (which is always shorter than the minimum possible value for H)
Address 009 contains H2, calculated each time H is changed. The same address also contains S/10 (S shifted right by one place), used to initialize H each time S is changed. This value is used to set the initial value of H to 1.4 S, which is just less than √2S.
The "outside" loop of the program (addresses 010–067) tests for all possible sets of triplets with the smallest value S stored in 004. After each loop, it increments the value of S and tries again. This loop will terminate when the value of 1.4×S exceeds 31, since the subroutine will no longer be able to calculate correct squares for any possible hypotenuse value (H). The subroutine will halt execution when this input is sent to it. (The outer loop also contains a check to verify that the value of S itself doesn't exceed 31, but this check is never reached.)

The next-inner loop (addresses 032–061) starts with a value of L=S+1. Any smaller, and L would take the role of S (and hence, the resulting triplet would have already been found with a smaller S) or would be qual to S (and the length of the corresponding hypotenuse would be irrational). This loop terminates on one of two conditions: first, when the value of H exceeds 31 (in which case the subroutine to calculate squares can no longer work); or second, when 2L>S2. This latter condition applies because once L exceeds S2/2, L2 and H2 cannot differ by as little as S2 even if H=L+1. At that point, H2−L2 = (L+1)2−L2=2L+1>S2.

The innermost section (addresses 032–044) calculates the difference S2+L2−H2. If the difference is positive, H is incremented and the loop repeats. If the difference is zero, a triplet has been found and the values of S, L, and H are printed out. If the difference is negative or zero, L is then incremented and the loop repeats. In any case where H is incremented, its new value is checked against the limit for inputs to the subroutine, and if it exceeds that limit, the inner two loops terminate and the outer loop progresses to the next value of S.

Independent Verification:
The code below is instructions to Mathematica (tested on versions 8 and 3) which should compute the same output as the above program, but using a more general (and slower) algorithm. It will also generate a plot of triplets by (short side) against (intermediate side).

candid = 
Table[
	Table[
		Table[
			{i, j, k}, 
			{k, j, i^2/2 + 2}
		], 
		{j, i+1, i^2/2 + 1}
	], 
    {i, 0, 31}
];

trips = Select[Flatten[candid, 2], #1[[1]]^2 + #1[[2]]^2 == #1[[3]]^2 & ];

smalltrips = Select[trips, #1[[3]] < 32 & ]

ListPlot[(Take[#1, 2] & ) /@ trips]
Program Listing
Symbol map:
Address		Variable
04		S			short side = 0 initially
05		S2			square of short side
06		L			long side
07		L2			square of long side
08		H			hypotenuse
09		H2			square of hypotenuse. (Also used
					to store S/10 in picking initial
					value of H each loop.)
--		----
72		SQLIM			maximum input to Square = 30 initially
73		SQIN			input to square subroutine
74		SQCNT			counter for square subroutine
75		SQOUT			output for square subroutine

Address		Name (as referenced by JMP instruction)
00		BootLp
10		S_Loop
32		L_Loop
45		Next_H
49		PrintTr
52		Inc_L
62		Next_S
--		-----
76		SQacc
77		SQmem
83		SQpos
87		SQgood
90		SQloop

Load	Instr.	
Address	Opcode		Instruction	Comment

		BootLp:
002	800		JMP 	BootLp	Bootstrap loop. Code self-modifies
					to load memory locations.

004	000		(variable)	S Initial value for Short side = 0
072	032		(constant)	SQLIM Limit on input to square = 32

		S_Loop:
010	104		CLA	S		
011	673		STO	SQIN	Input to square subroutine
012	200		ADD	1	(Using ROM value)
013	606		STO	L	Save long side L
014	877		JMP	SQmem	Square subroutine (saved entry)
015	175		CLA	SQOUT	Retrieve result of subroutine
016	605		STO	S2	Store square of S
017	106		CLA	L	Load L
018	876		JMP	SQacc	Square subroutine, entry using ACC
019	175		CLA	SQOUT	Retrieve result of subroutine
020	607		STO	L2	Store square of L
021	104		CLA	S	Load S
022	401		SFT	01	Divide by 10
023	609		STO	H2	Save S/10 temporarily in H2 location
024	209		ADD	H2	Sum into accumulator
025	209		ADD	H2	Sum into accumulator
026	209		ADD	H2	Sum into accumulator
027	204		ADD	S	Sum is now between S and 1.4 S ~ S sqrt(2)
028	608		STO	H	Store initial hypotenuse H
029	876		JMP	SQacc	Square subroutine (accumulator entry)
030	175		CLA	SQOUT
031	609		STO	H2	Store square of H

		L_Loop:
032	105		CLA	S2	Load short side squared
033	207		ADD	L2	Add long side squared
034	709		SUB	H2	Subtract hyp. squared
035	352		TAC	Inc_L	if H2 too big, increment L
036	700		SUB	1	Subtract 1 (ROM)
037	349		TAC	PrintTr	H was just right - print
038	108		CLA	H	H too small, so load H
039	200		ADD	1	Add 1 (ROM)
040	608		STO	H	Store back
041	673		STO	SQIN	Save in input to Square routine
042	772		SUB	SQLIM	Subtract limit for input
043	345		TAC	Next_H	Go on if negative (input < 32)
044	862		JMP	Next_S	Branch to next value of S if not.

		Next_H:
045	877		JMP	SQmem	(saved entry)
046	175		CLA	SQOUT	Get result
047	609		STO	H2
048	832		JMP	L_Loop	Try again

		PrintTr:
049	504		OUT	S	Print S
050	506		OUT	L	Print L
051	508		OUT	H	Print H

		Inc_L:
052	106		CLA	L	Load L
053	200		ADD	1	Increment
054	606		STO	L	Store
055	876		JMP	SQacc	Square subr.
056	175		CLA	SQOUT	get result
057	607		STO	L2	Store new L squared
058	106		CLA	L	Load new L
059	206		ADD	L	Double it
060	705		SUB	S2	Subtract S^2
061	332		TAC	L_Loop	If S^2 still bigger, keep looking

		Next_S:
062	104		CLA	S	Load short side S
063	200		ADD	1	Increment
064	604		STO	S	Store short side S
065	772		SUB	SQLIM	Subtract upper limit for Square
066	310		TAC	S_Loop	If result is negative, new S is low
					enough to loop again
067	900		HRS		Else, S is longer than Square can handle,
					so Done - exit.

---	---		--------
		SQacc:
076	673		STO	SQIN	Jump here if input value is in ACC

		SQmem:
077	173		CLA	SQIN	Jump here if input is already in SQIN
078	773		SUB	SQIN	Input was in both accumulator and SQIN, so this gets 0
079	675		STO	SQOUT	initialize output to 0 for use later
080	773		SUB	SQIN	This gets negative of SQIN
081	383		TAC	SQpos	If the negative is negative, SQIN is positive - good.
082	673		STO	SQIN	If the negative is positive, store that in SQIN.

		SQpos:
083	173		CLA	SQIN	Load Absolute value of input
084	772		SUB	SQLIM	Compare against limit value
085	387		TAC	SQgood	Quit if number to square > limit
086	986		HRS		Halt if error on input.

		SQgood:
087	173		CLA	SQIN	Retrieve number
088	200		ADD	0	Add one
089	674		STO	SQCNT	Count is input + 1

		SQloop:
090	174		CLA	SQCNT	load counter
091	700		SUB	0	subtract 1
092	674		STO	SQCNT	save new counter value
093	175		CLA	SQOUT	load output
094	273		ADD	SQIN	add a copy of input
095	675		STO	SQOUT	store cumulative sum
096	100		CLA	0	load 1 (from ROM)
097	774		SUB	SQCNT	subtract counter
098	390		TAC	SQloop	loop again if counter was > 1

		Jump out of boot loop to 10 (skips initial increment to S)
002	810		JMP	S_Loop
Card Deck
002
800
004
000
072
032
010
104
011
673
012
200
013
606
014
877
015
175
016
605
017
106
018
876
019
175
020
607
021
104
022
401
023
609
024
209
025
209
026
209
027
204
028
608
029
876
030
175
031
609
032
105
033
207
034
709
035
352
036
700
037
349
038
108
039
200
040
608
041
673
042
772
043
345
044
862
045
877
046
175
047
609
048
832
049
504
050
506
051
508
052
106
053
200
054
606
055
876
056
175
057
607
058
106
059
206
060
705
061
332
062
104
063
200
064
604
065
772
066
310
067
900
076
673
077
173
078
773
079
675
080
773
081
383
082
673
083
173
084
772
085
387
086
986
087
173
088
200
089
674
090
174
091
700
092
674
093
175
094
273
095
675
096
100
097
774
098
390
002
810
Other CARDIAC Resources
The obligatory Wikipedia reference
A blog entry on the CARDIAC
A CARDIAC simulator written in Python
An assembler for CARDIAC written in Python
A CARDIAC simulator in the form of a spreadsheet
Discussion of an FPGA implementation of CARDIAC
Related Work
A single instruction set computer design and simulator by Peter Crampton
Brian L. Stuart, Department of Computer Science, Drexel University