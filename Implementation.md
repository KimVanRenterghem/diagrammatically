#Test
<div id="diagram">

    </div>
    <script src="http://cdnjs.cloudflare.com/ajax/libs/raphael/2.2.0/raphael-min.js"></script>
    <script src="http://cdnjs.cloudflare.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="http://flowchart.js.org/flowchart-latest.js"></script>
		<script>
		  var diagram = flowchart.parse('st=>start: Start:>http://www.google.com[blank]\n' +
                                'e=>end:>http://www.google.com\n' +
                                'op1=>operation: My Operation\n' +
                                'op2=>operation: Stuff|current\n' +
                                'sub1=>subroutine: My Subroutine\n' +
                                'cond=>condition: Yes \n' + // use cond(align-next=no) to disable vertical align of symbols below
                                'or No?\n:>http://www.google.com\n' +
                                'c2=>condition: Good idea|rejected\n' +
                                'io=>inputoutput: catch something...|request\n' +
                                '\n' +
                                'st->op1(right)->cond\n' +
                                'cond(yes, right)->c2\n' + // conditions can also be redirected like cond(yes, bottom) or cond(yes, right)
                                'cond(no)->sub1(left)->op1\n' + // the other symbols too...
                                'c2(true)->io->e\n' +
                                'c2(false)->op2->e'  //allow for true and false in conditionals
                                );
  diagram.drawSVG('diagram');
		</script>
