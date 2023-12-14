import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { Vault } from '../models/Vault'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async editAccount(editData){
    const res = await api.put('/account', editData)
    logger.log(res.data)
    AppState.account = new Account(res.data)
  }


  async getVaultsByAccount(accountId) {
    logger.log(`account id: ${accountId}`)
    const res = await api.get(`account/vaults`)
    logger.log('GOT ACCOUNT VAULTS', res.data)
    AppState.vaults = res.data.map((vault) => new Vault(vault))
}
}

export const accountService = new AccountService()
