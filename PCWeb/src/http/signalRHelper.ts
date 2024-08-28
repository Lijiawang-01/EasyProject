import * as signalR from "@microsoft/signalr"; //及时聊天插件
import store from "@/store/index"; //token过期后刷新token的方式
//建立signalR链接，然后存放在pinia中
class getChats extends signalR.DefaultHttpClient {
  constructor() {
    // constructor在这里没有用到所以就随便给super了个值防止报错
    super(console);
  }
  public async send(
    request: signalR.HttpRequest
  ): Promise<signalR.HttpResponse> {
    // const authHeader = store().$state.Token; //获取token的方法
    // request.headers = { ...authHeader }; // 给signalR链接添加请求头
    try {
      const response = await super.send(request);
      return response;
    } catch (error) {
      if (error instanceof signalR.HttpError) {
        const err = error as signalR.HttpError;
        if (err.statusCode == 401) {
          // // 如果返回的是401就表示token过期那么就需要去做token的刷新
          // const authHeader = await getStorage();
          // request.headers = { ...authHeader };
        }
      } else {
        throw error;
      }
    }
    return super.send(request);
  }
}
// 保存的后端及时通讯接口地址
const D = {
  dev: "http://127.0.0.1:5106",
  dev2: "http://192.168.1.30:5001",
  pro: import.meta.env.VITE_API_URL,
};
// 最后只需要去导出这方法然后传入你的signalR链接
export const getChat = new signalR.HubConnectionBuilder()
  .withUrl('/ChatHubTest', {
    httpClient: new getChats(),
  })
  .configureLogging(signalR.LogLevel.Information)
  .build() as signalR.HubConnection;