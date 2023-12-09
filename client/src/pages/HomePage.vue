<template>
  <div class="container-fluid">
    <div class="row">

      <div class="col-md-4 col-6 pt-5" v-for="keep in keeps" :key="keep">
        <KeepComp :keepProp="keep" />
      </div>

    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import { keepsService } from '../services/KeepsService';
import Pop from '../utils/Pop';
import KeepComp from '../components/KeepComp.vue';
import { AppState } from '../AppState';
import { vaultService } from '../services/VaultService';



export default {
  setup() {
    onMounted(() => {
      getKeeps();
      getVaults();
    });
    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      }
      catch (error) {
        Pop.error;
      }
    }

    async function getVaults() {
      try {
        await vaultService.getVaults();
      }
      catch (error) {
        Pop.error;
      }
    }
    return {
      keeps: computed(() => AppState.keeps)
    };
  },
  components: { KeepComp }
}
</script>

<style scoped lang="scss"></style>
