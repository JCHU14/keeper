<template>
    <div class="modal fade" id="AccountModalForm" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content formbg formbrdr">
                <div class="modal-header">
                    <h5 class="modal-title text-dark" id="exampleModalLabel">Edit Account</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="editAccount()">

                        <div class="mb-3">
                            <label for="name" class="form-label">Account Name</label>
                            <input v-model="editable.name" type="name" class="form-control" id="name">
                        </div>

                        <div class="mb-3">
                            <label for="picture" class="form-label">Account Picture</label>
                            <input v-model="editable.picture" type="picture" class="form-control" id="picture">
                        </div>

                        <div class="mb-3">
                            <label for="coverImg" class="form-label">Account Cover Image</label>
                            <input v-model="editable.coverImg" type="coverImg" class="form-control" id="coverImg">
                        </div>
                        <div class="text-end">
                            <button class="btn btn-success" type="submit">Save</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { computed, ref, watchEffect } from 'vue';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { accountService } from '../services/AccountService.js';
import { Modal } from 'bootstrap';


export default {
    setup() {
        const editable = ref({})

        watchEffect(() => {
            editable.value = AppState.account
        })


        return {
            editable,
            account: computed(() => AppState.account),

            async editAccount() {
                try {
                    const accountData = editable.value
                    logger.log(accountData)
                    await accountService.editAccount(accountData)
                    Modal.getOrCreateInstance('#AccountModalForm').hide()
                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped></style>