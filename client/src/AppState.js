import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},
  
  /** @type {import('./models/Keep.js').Keep[]} */
  keeps: [],
  
  
  /** @type {import('./models/Keep.js').Keep | null} */
  activeKeep: {},
  
  /** @type {import('./models/Vault.js').Vault[]} */
  vaults: [],
  
  /** @type {import('./models/Vault.js').Vault | null} */
  activeVault: null,
  
  /** @type {import('./models/Profile.js').Profile[]} */
  profiles: [],
  
  /** @type {import('./models/VaultKeep.js').VaultKeep[]} */
  vaultKeeps: [],

})
