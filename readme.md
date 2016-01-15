##介绍	
纯C#写的通用socket服务端/客户端程序,支持协议扩展。	  
  
服务端  
![服务端](https://github.com/tinygg/Socket.Net/blob/master/image/server.png?raw=true)  
  
客户端  
![客户端](https://github.com/tinygg/Socket.Net/blob/master/image/client.png?raw=true)  
  
消息窗口  
![消息窗](https://github.com/tinygg/Socket.Net/blob/master/image/msg.png?raw=true)  

##功能
多人聊天、客户端消息弹出、即时查询	

##原则	
简化使用，尽量不引用DOTNET外部代码,尽量做出大家都想用的模块.	

##更新	
2015.11.5  简单的socket连接，能发送文本消息，统计在线用户

2015.11.6  弹出界面，回复消息	

2015.11.7  添加协议概念，添加根据协议构造消息、解析协议消息方法。	
![协议](https://github.com/tinygg/Socket.Net/blob/master/image/protocol.png?raw=true)
byte[10] 命令区  
byte[150] 发送者信息区(包含认证信息)  
byte[150] 接收者信息区  
byte[4] 消息长度区  
byte[any < 65535-10-150-150-4] 消息区(消息长度区指定的长度)	  

2015.11.8  添加对socket查询的支持(点到为止，不添加对数据库的支持,需要的自己添加)

##TODO  
文件传输、可执行代码流、远程桌面、远程控制  

##小工具  
初期为了方便调试，自己做了个小工具附上。
[下载](https://github.com/tinygg/Tools.Net/releases/download/V0.1/Convert.zip)
