import { AppState } from "../AppState"
import { Keep } from "../models/Keep"
import { Vault } from "../models/Vault"
import { VaultKeep } from "../models/VaultKeep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultService{

    async getVaults(){
        const res = await api.get(`api/vaults`)
        logger.log(res.data)
        AppState.vaults = res.data.map((vault) => new Vault(vault))
        logger.log (AppState.vaults)
    }

    async createVault(vaultData) {
        const res = await api.post('api/vaults', vaultData)
        logger.log('creating vault', res.data)
        const newVault = new Vault(res.data)
        AppState.vaults.push(newVault)
        return newVault;
    }
    async destroyVault(vaultId) {
        const vaultIndex = AppState.vaults.findIndex(vault => vault.id == vaultId)
        const res = await api.delete(`api/vaults/${vaultId}`);
        logger.log("vault Deleted", res.data);
        AppState.activeVault = new Vault(res.data);
        AppState.vaults.splice(vaultIndex, 1)
      
    }

    async editVault(vaultId, vaultData) {
        const res = await api.put(`api/vaults/${vaultId}`, vaultData)
        logger.log('you edited vault', res.data)
        const newVault = new Vault(res.data)
        AppState.activeVault = newVault
      }

      async getVaultById(vaultId) {
        AppState.activeVault = null;
        const res = await api.get(`api/vaults/${vaultId}`);
        logger.log("got vault by ID", res.data);
        AppState.activeVault = new Vault(res.data);
      }

      async getKeepsByVaultId(vaultId) {
        logger.log(`vault id: ${vaultId}`)
        const res = await api.get(`api/vaults/${vaultId}/keeps`)
        logger.log('got keeps by id', res.data)
        AppState.vaultKeeps = res.data.map((vaultKeep) => new Keep(vaultKeep))
    }

}

export const vaultService = new VaultService();