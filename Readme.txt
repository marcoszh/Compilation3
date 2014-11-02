编译原理第二次实验
VS2010工程
首先读入文件，在Tools中选择词法分析获得Token序列
再选择Tools中的第二项生成词法分析表（界面会卡住，懒成狗大计算量操作没开线程），Grammar文件已上传
再选择Tools中的LL1文法分析，输出分法分析结果（依旧懒成狗，卡卡卡）
（严重参考了https://github.com/liushuaikobe 的代码，致谢）

Compilation course labwork 2
A VS2010 project
Firstly, open a C style code file to analysis, clike the first item in Tools menu to get the token list.
Secondly, click the second item in Tools menu to generate prediction table from grammar file which should be stored in the same dictionary with the executive file. The form won't response to any event during the process because I'm too cool to program in multi-thread, deal with it.
Finally, click the third item to start LL(1) analysis, still won't response, I'm lazzzzzy as duck.
Shout out to https://github.com/liushuaikobe for his great work which I refers to a lot, like a loooooooooooooooooooooooooooooooot.(I owe you a beer, bro.)