import instance from '@/http/index';
//获取token
export const getToken = (name: string, password: string) => {
  return instance.get('/api/Login/GetToken?name=' + name + '&password=' + password);
};