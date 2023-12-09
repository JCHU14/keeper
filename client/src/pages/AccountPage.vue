<template>
  <div class="about text-center">
    <h1>Welcome {{ account.name }}</h1>
    <img class="rounded" :src="account.picture" alt="" />
    <p>{{ account.email }}</p>
    <button data-bs-toggle="modal" data-bs-target="#AccountModalForm" class="btn btn-info text-light">edit
      account</button>
  </div>
  <AccountModal />
</template>

<script>
import { computed } from 'vue';
import { AppState } from '../AppState';
import AccountModal from '../components/AccountModal.vue';
export default {
  setup() {
    return {
      account: computed(() => AppState.account)
    };
  },
  components: { AccountModal }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
