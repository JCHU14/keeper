<template>
    <div class="container-fluid">
        <div class="row">

            <div>
                <div class="">
                    <button @click="deleteVault(vaultProp.id)" class="text-2 btn btn-danger"> X </button>
                    <router-link title="To Vault Details" :to="{ name: 'VaultDetails', params: { vaultId: vaultProp.id } }">

                        <img class="img-fluid glow border image" :src="vaultProp?.img" alt="">
                        <div class="d-flex justify-content-between p-2 align-items-center">
                            <div class="d-flex justify-content-between p-2 align-items-center">
                                <p class="text text-light fs-3">{{ vaultProp?.name }}</p>
                                <p v-if="vaultProp.isPrivate == true" class="text text-light fs-3"><i
                                        class="mdi mdi-lock"></i>
                                </p>
                            </div>
                        </div>
                    </router-link>
                </div>
            </div>

        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Vault } from '../models/Vault';
import { vaultService } from '../services/VaultService';
import Pop from '../utils/Pop';
export default {
    props: { vaultProp: { type: Vault, required: true } },
    setup() {
        return {
            async deleteVault(vaultId) {
                try {
                    const yes = await Pop.confirm(`Are you sure you want to delete this vault?`);
                    if (!yes) {
                        return;
                    }
                    await vaultService.destroyVault(vaultId)
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
    width: 35vh;
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
    bottom: 75px;
    left: 30px;

    text-shadow: 2px 2px 5px black;
}

.text-2 {
    position: relative;
    top: 15px;
    left: 130px;

    text-shadow: 2px 2px 5px black;
}

.border {
    border: 3px solid black;
    box-shadow: 0 5px 10px black;
}

.glow:hover {
    box-shadow: 0px 0px 50px black;
    transition: ease-in-out 0.3s;
}
</style>