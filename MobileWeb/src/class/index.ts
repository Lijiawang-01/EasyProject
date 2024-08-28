export interface BannerAPIReq {
  title: string;
}

export interface BannerListType {
  detail_url: string;
  picture: string;
  url: string;
}

export interface BannerAPIRes {
  code: number;
  message: string;
  banner: BannerListType[];
}