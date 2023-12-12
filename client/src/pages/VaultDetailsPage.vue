<template>
    <div>
        <img :src="vaults?.img" alt="">
    </div>
</template>


<script>
import { useRoute } from 'vue-router';
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { vaultService } from '../services/VaultService.js';
import Pop from '../utils/Pop';
export default {
    setup() {
        const route = useRoute();
        onMounted(() => {
            getVaultById();
        });


        async function getVaultById() {
            try {
                const vaultId = route.params.vaultId;
                await vaultService.getVaultById(vaultId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        return {
            vaults: computed(() => AppState.activeVault)
        }
    }
};
</script>


<style lang="scss" scoped></style>