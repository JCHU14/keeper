import { AppState } from "../AppState"
import { Keep } from "../models/Keep"
import { Profile } from "../models/Profile"
import { Vault } from "../models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfilesService {
    async getProfile(profileId) {
        AppState.profiles = null
        const res = await api.get(`api/profiles/${profileId}`)
        logger.log(res.data)
        AppState.profiles = new Profile(res.data)
    }

    async getKeepsByProfile(profileId) {
        logger.log(`profile id: ${profileId}`)
        const res = await api.get(`api/profiles/${profileId}/keeps`)
        logger.log('got keeps by id', res.data)
        AppState.keeps = res.data.map((keep) => new Keep(keep))
    }
    async getVaultByProfile(profileId) {
        logger.log(`profile id: ${profileId}`)
        const res = await api.get(`api/profiles/${profileId}/vaults`)
        logger.log('got vaults by id', res.data)
        AppState.vaults = res.data.map((vault) => new Vault(vault))
    }
}

export const profilesService = new ProfilesService