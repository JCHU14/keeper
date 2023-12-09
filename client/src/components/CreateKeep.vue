<template>
    <div class="modal fade" id="createModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header ">
                    <p class="fs-1 align-items-center display-2">Create Keep</p>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>

                <div class="container-fluid">

                    <div>
                        <form @submit.prevent="createKeep()">




                            <div>
                                <div class="mb-3">
                                    <label for="name" class="form-label text-center text-dark fs-2">Name</label>
                                    <textarea v-model="editable.name" maxlength="45" class="form-control" id="name" required
                                        rows="1"></textarea>
                                </div>

                                <div class="mb-3">
                                    <label for="instructions"
                                        class="form-label text-center text-dark fs-2">Description</label>
                                    <textarea v-model="editable.description" maxlength="200" class="form-control"
                                        id="description" required rows="1"></textarea>
                                </div>

                                <div class="mb-3">
                                    <label for="img" class="form-label text-center text-dark fs-2">Image</label>
                                    <textarea v-model="editable.img" maxlength="200" class="form-control" id="img" required
                                        rows="1"></textarea>
                                </div>
                            </div>





                            <div class="text-end my-2">
                                <button class="btn btn-success" type="submit">Submit</button>
                            </div>
                        </form>
                    </div>

                </div>

            </div>
        </div>
    </div>
</template>


<script>
import { computed, ref } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { Modal } from 'bootstrap';
import { keepsService } from '../services/KeepsService';





export default {
    setup() {
        const editable = ref({})

        return {
            editable,
            keeps: computed(() => { AppState.keeps }),
            async createKeep() {
                try {
                    const keepData = editable.value
                    await keepsService.createKeep(keepData)

                    editable.value = {}

                    Modal.getOrCreateInstance('#createModal').hide()
                } catch (error) {
                    Pop.error(error)
                }
            }
        };
    },
};
</script>


<style lang="scss" scoped>
.titles {
    background-color: rgba(128, 128, 128, 0.26);
}

.green {
    background-color: darkgreen;
}

.underline {
    text-decoration: 2px underline black;
}

.box-shadow {
    box-shadow: 0 5px 10px black;
}
</style>