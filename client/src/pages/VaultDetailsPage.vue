<template>
    <div class="container">
        <div class="row">
            <div class="col-12 text-center">
                <img class="image img-fluid p-2" :src="vaults?.img" alt="">
                <p class="text display-5 fs-1 text-white">{{ vaults?.name }}</p>
                <p class="text display-5 fs-5 text-white">{{ vaults?.description }}</p>
            </div>
            <div class="col-4" v-for="vaultKeep in vaultKeeps" :key="vaultKeep">
                <VaultKeepComp :vaultKeepProp="vaultKeep" />
            </div>
        </div>
    </div>
</template>


<script>
import { useRoute } from 'vue-router';
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { vaultService } from '../services/VaultService.js';
import { vaultKeepsService } from '../services/VaultKeepsService.js'
import Pop from '../utils/Pop';
import VaultKeepComp from '../components/VaultKeepComp.vue';
import { keepsService } from '../services/KeepsService';
import { router } from '../router';
export default {
    setup() {
        const route = useRoute();
        onMounted(() => {
            getVaultById();
            getKeepsByVault();
        });
        async function getVaultById() {
            try {
                const vaultId = route.params.vaultId;
                await vaultService.getVaultById(vaultId);
            }
            catch (error) {
                Pop.error(error);
                if (error.response.data.includes('good try....')) {
                    router.push({ name: 'Home' })
                }
            }
        }
        async function getKeepsByVault() {
            try {
                const vaultId = route.params.vaultId;
                await vaultService.getKeepsByVaultId(vaultId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        return {
            vaults: computed(() => AppState.activeVault),
            Keeps: computed(() => AppState.keeps),
            vaultKeeps: computed(() => AppState.vaultKeeps)


        };
    },
    components: { VaultKeepComp }
};
</script>


<style lang="scss" scoped>
.image {
    position: relative;
    width: 75vh;
    height: 35vh;
    object-fit: cover;
}

.profile {
    object-fit: cover;
    object-position: center;
    width: 100%;
    height: 45px;
}

.text {
    position: relative;
    bottom: 150px;

    text-shadow: 2px 2px 5px black;
}

.text-2 {
    position: relative;
    top: 15px;
    left: 130px;

    text-shadow: 2px 2px 5px black;
}
</style>