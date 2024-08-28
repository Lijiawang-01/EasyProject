import request from "@/http/index"
import { BannerAPIReq, BannerAPIRes } from "@/class/index"

export const helpCenterRes = () => request.get('/helpcenter')

export const bannerAPIRes = (params: BannerAPIReq): Promise<BannerAPIRes> => request.get('/banner', { params })
