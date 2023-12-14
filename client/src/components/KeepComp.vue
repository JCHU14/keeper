<template>
    <div class="container-fluid">
        <div class="row">

            <div>
                <div @click="getVaultByAccount()">
                    <img type="button" role="button" data-bs-toggle="modal" data-bs-target="#keepModal"
                        @click="getKeepById(keepProp.id);" class="img-fluid image" :src="keepProp.img" alt="">
                    <div class="d-flex justify-content-between p-2 align-items-center">
                        <p class="text text-light fs-2">{{ keepProp.name }}</p>
                        <router-link title="To Profile Page"
                            :to="{ name: 'Profile', params: { profileId: keepProp.creator.id } }">
                            <img class="profile text rounded-circle" :src="keepProp.creator.picture" alt="">
                        </router-link>
                    </div>
                </div>
            </div>

        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Keep } from '../models/Keep';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService';
import { accountService } from '../services/AccountService';
import { useRoute } from 'vue-router';
export default {
    props: { keepProp: { type: Keep, required: true } },

    setup() {
        const route = useRoute();
        return {
            async getKeepById(keepId) {
                try {
                    logger.log(keepId)
                    await keepsService.getKeepById(keepId)
                    Modal.getOrCreateInstance('#keepModal').hide()
                } catch (error) {
                    Pop.error
                }
            },
            async getVaultByAccount() {
                try {
                    const accountId = route.params.accountId;
                    await accountService.getVaultsByAccount(accountId);
                }
                catch (error) {
                    Pop.error
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped>
.image {
    position: relative;
    width: 100%;
    height: 20rem;
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
    bottom: 75px;

    text-shadow: 2px 2px 5px black;
}
</style>