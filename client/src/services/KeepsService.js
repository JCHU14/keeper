import { AppState } from "../AppState"
import { Keep } from "../models/Keep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService{

    async getKeeps(){
        const res = await api.get(`api/keeps`)
        logger.log(res.data)
        AppState.keeps = res.data.map((keep) => new Keep(keep))
        logger.log (AppState.keeps)
    }

    async createKeep(keepData) {
        const res = await api.post('api/keeps', keepData)
        logger.log('creating keep', res.data)
        const newKeep = new Keep(res.data)
        AppState.keeps.push(newKeep)
        return newKeep;
    }
    async destroyKeep(keepId) {
        const res = await api.delete(`api/keeps/${keepId}`);
        const keepIndex = AppState.keeps.findIndex(keep => keep.id == keepId)
        logger.log("keep Deleted", res.data);
        AppState.activeKeep = new Keep(res.data);
        AppState.keeps.splice(keepIndex, 1)
      
    }

    async editKeep(keepId, keepData) {
        const res = await api.put(`api/keeps/${keepId}`, keepData)
        logger.log('you edited keep', res.data)
        const newKeep = new Keep(res.data)
        AppState.activeKeep = newKeep
      }

      async getKeepById(keepId) {
        AppState.activeKeep = null;
        const res = await api.get(`api/keeps/${keepId}`);
        logger.log("got keep by ID", res.data);
        AppState.activeKeep = new Keep(res.data);
      }

      

}

export const keepsService = new KeepsService();