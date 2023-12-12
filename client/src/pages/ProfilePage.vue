<template>
    <div class="container">
        <div class="row">
            <img class="p-5 coverImg" :src="profile?.coverImg" alt="">
            <div v-if="profile" class="col-12 text-center">
                <img class="rounded-circle text-center profile" :src="profile.picture" alt="">
                <p class="fs-4">{{ profile?.name }}</p>
                <p>{{ vaults.length }} Vaults | {{ keeps.length }} Keeps</p>
            </div>
            <p class="fs-2 text-center underline p-2">Vaults</p>
            <div class="col-12">
                <section class="row text-center justify-content-center">
                    <div class="col-md-3 col-8 text-center" v-for="vault in vaults" :key="vault">
                        <VaultsProfile :vaultProp="vault" />
                    </div>

                </section>
            </div>
            <p class="fs-2 text-center underline p-2">Keeps</p>
            <div class="row text-center justify-content-center">
                <div class="col-md-3 col-8 text-center" v-for="keep in keeps" :key="keep">
                    <KeepsProfile :keepProp="keep" />
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, onMounted } from 'vue';
import { logger } from '../utils/Logger.js';
import { useRoute } from "vue-router"
import Pop from '../utils/Pop';
import { profilesService } from '../services/ProfilesService';
import VaultsProfile from '../components/VaultsProfile.vue';
import KeepsProfile from '../components/KeepsProfile.vue';




export default {
    setup() {
        onMounted(() => {
            getProfile();
            getKeepsByProfile();
            getVaultByProfile();
        });
        async function getKeepsByProfile() {
            try {
                const profileId = route.params.profileId;
                await profilesService.getKeepsByProfile(profileId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        async function getVaultByProfile() {
            try {
                const profileId = route.params.profileId;
                await profilesService.getVaultByProfile(profileId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        const route = useRoute();
        async function getProfile() {
            const profileId = route.params.profileId;
            logger.log(profileId);
            await profilesService.getProfile(profileId);
        }
        return {
            profile: computed(() => AppState.profiles),
            vaults: computed(() => AppState.vaults),
            keeps: computed(() => AppState.keeps)
        };
    },
    components: { VaultsProfile, KeepsProfile }
};
</script>


<style lang="scss" scoped>
.underline {
    text-decoration: underline;
}

.coverImg {
    width: 100%;
    height: 60vh;
    object-fit: cover;
    object-position: center;
}

.profile {
    width: 25%;
    height: 45vh;
    object-fit: cover;
    object-position: center;
}
</style>