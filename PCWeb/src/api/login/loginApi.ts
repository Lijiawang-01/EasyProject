import instance from '@/http/filter';
//获取token
export const getToken = (name: string, password: string) => {
  return instance.get('/api/Login/GetToken?name=' + name + '&password=' + password);
};