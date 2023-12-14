import { AppState } from "../AppState"
import { Keep } from "../models/Keep"
import { VaultKeep } from "../models/VaultKeep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService{

    async getVaultKeeps(){
        const res = await api.get(`api/vaultKeeps`)
        logger.log(res.data)
        AppState.vaultKeeps = res.data.map((vaultKeep) => new VaultKeep(vaultKeep))
        logger.log (AppState.vaultKeeps)
    }

    async createVaultKeep(vaultKeepData) {
        const res = await api.post('api/vaultKeeps', vaultKeepData)
        logger.log('creating vaultKeep', res.data)
        const newVaultKeep = new VaultKeep(res.data)
        AppState.vaultKeeps.push(newVaultKeep)
        return newVaultKeep;
    }
    async destroyVaultKeep(vaultKeepId) {
        const res = await api.delete(`api/vaultKeeps/${vaultKeepId}`);
        const vaultKeepIndex = AppState.vaultKeeps.findIndex(vaultKeep => vaultKeep.id == vaultKeepId)
        logger.log("vaultKeep Deleted", res.data);
        AppState.vaultKeeps = new VaultKeep();
        AppState.vaultKeeps.splice(vaultKeepIndex, 1)
    }

    async editVaultKeep(vaultKeepId, vaultKeepData) {
        const res = await api.put(`api/vaultKeeps/${vaultKeepId}`, vaultKeepData)
        logger.log('you edited vault keep', res.data)
        const newVaultKeep = new VaultKeep(res.data)
        AppState.vaultKeeps = newVaultKeep
      }

      async getVaultKeepById(vaultKeepId) {
        AppState.vaultKeeps = null;
        const res = await api.get(`api/vaultKeeps/${vaultKeepId}`);
        logger.log("got vault keep by ID", res.data);
        AppState.vaultKeeps = new VaultKeep(res.data);
      }

      

}

export const vaultKeepsService = new VaultKeepsService();