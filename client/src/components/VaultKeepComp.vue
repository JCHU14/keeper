<template>
    <div class="container-fluid">
        <div class="row">

            <div>
                <div>
                    <button @click="deleteVaultKeep(vaultKeepProp.vaultKeepId)" class="text-2 btn btn-danger"> X </button>
                    <img class="img-fluid image" :src="vaultKeepProp?.img" alt="">
                    <div class="d-flex justify-content-between p-2 align-items-center">
                        <p class="text text-light fs-2">{{ vaultKeepProp?.name }}</p>
                    </div>
                </div>
            </div>

        </div>
    </div>
</template>


<script>

import { VaultKeep } from '../models/VaultKeep';
import { vaultKeepsService } from '../services/VaultKeepsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';


export default {
    props: { vaultKeepProp: { type: VaultKeep, required: true } },

    setup() {

        return {
            async deleteVaultKeep(vaultKeepId) {
                try {
                    const yes = await Pop.confirm(`Are you sure you want to remove this keep from vault?`);
                    if (!yes) {
                        return;
                    }
                    logger.log(vaultKeepId)
                    await vaultKeepsService.destroyVaultKeep(vaultKeepId)
                }
                catch (error) {
                    Pop.error;
                }
            },
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

.text-2 {
    position: relative;
    top: 15px;
    left: 375px;

    text-shadow: 2px 2px 5px black;
}
</style>