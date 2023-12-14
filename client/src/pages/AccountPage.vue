<template>
  <div class="container-fluid">

    <div class="row">
      <div class="about text-center">
        <h1>Welcome {{ account.name }}</h1>
        <img class="rounded" :src="account.picture" alt="" />
        <p>{{ account.email }}</p>
      </div>
      <AccountModal />
    </div>

    <div class="row">
      <div class="col-12 text-center p-5">
        <p class="fs-1">MY Vaults</p>
        <button data-bs-toggle="modal" data-bs-target="#AccountModalForm" class="btn btn-info text-light">edit
          account</button>

      </div>


      <div class="col-3" v-for="vault in vaults" :key="vault.id">
        <VaultsProfile :vaultProp="vault" />
      </div>

    </div>



  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState';
import AccountModal from '../components/AccountModal.vue';
import { useRoute } from 'vue-router';
import { accountService } from '../services/AccountService';
import Pop from '../utils/Pop';
import VaultsProfile from '../components/VaultsProfile.vue';
export default {
  setup() {
    onMounted(() => {
      getVaultByAccount();
    })
    async function getVaultByAccount() {
      try {
        const accountId = route.params.accountId;
        await accountService.getVaultsByAccount(accountId);
      }
      catch (error) {
        Pop.error(error);
      }
    }
    const route = useRoute();
    return {
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults)
    };
  },
  components: { AccountModal, VaultsProfile }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
