<template>
    <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header text-end justify-content-end">
                    <button @click="deleteKeep(keeps.id)" class="btn fs-3"><i class="mdi mdi-delete"></i></button>
                </div>

                <div v-if="keeps" class="modal-body">
                    <div class="container-fluid">
                        <div class="row">

                            <div class="col-md-5 col-12">
                                <img class="image box-shadow" :src="keeps.img" alt="">
                            </div>



                            <div class="col-md-7 col-12">
                                <div class="d-flex justify-content-center pb-5">
                                    <p class="p-1 fs-5"> <i class="mdi mdi-eye"></i>{{ keeps.views }}</p>
                                    <p class="p-1 fs-5"><i class="mdi mdi-alpha-k-box-outline"></i>{{ keeps.kept }}</p>
                                </div>


                                <div class="pt-5 pb-5">
                                    <p class="fs-1 text-center">{{ keeps.name }}</p>
                                    <p class="fs-5 text-center">{{ keeps.description }}</p>
                                </div>

                                <div class="pt-5 d-flex justify-content-between">
                                    <div>
                                        <form @submit.prevent="createVaultKeep()">

                                            <select v-model="editable.vaultId" class="form-select"
                                                aria-label="Default select example">
                                                <option selected>Open this select menu</option>

                                                <option v-for="vault in vaults" :key="vault.id" :value="vault.id">
                                                    {{ vault.name }}
                                                </option>
                                            </select>

                                            <button type="submit" class="btn btn-info">save</button>

                                        </form>
                                    </div>

                                    <div class="d-flex">
                                        <img class="profile rounded-circle" :src="keeps.creator?.picture" alt="creator">
                                        <p class="fs-5 p-1">{{ keeps.creator?.name }}</p>
                                    </div>
                                </div>



                            </div>

                        </div>
                    </div>

                </div>

            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { keepsService } from '../services/KeepsService';
import { Modal } from 'bootstrap';
import Pop from '../utils/Pop';
import { vaultKeepsService } from '../services/VaultKeepsService';
import { useRoute } from 'vue-router';

export default {
    setup() {
        const editable = ref({})
        const route = useRoute();
        return {
            keeps: computed(() => AppState.activeKeep),
            vaults: computed(() => AppState.vaults),
            editable,
            async deleteKeep(keepId) {
                try {
                    const yes = await Pop.confirm(`Are you sure you want to delete this keep?`);
                    if (!yes) {
                        return;
                    }
                    await keepsService.destroyKeep(keepId);
                    Modal.getOrCreateInstance('#keepModal').hide();
                }
                catch (error) {
                    Pop.error;
                }
            },
            async createVaultKeep() {
                try {
                    const vaultKeepData = editable.value
                    vaultKeepData.keepId = this.keeps.id;
                    await vaultKeepsService.createVaultKeep(vaultKeepData)

                    editable.value = {}

                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped>
.profile {
    object-fit: cover;
    object-position: center;
    width: 100%;
    height: 6vh;
}

.image {
    position: relative;
    width: 100%;
    height: 30rem;
    object-fit: cover;
}
</style>