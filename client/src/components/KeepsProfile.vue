<template>
    <div class="container-fluid">
        <div class="row">

            <div class="">
                <div>
                    <img type="button" role="button" data-bs-toggle="modal" data-bs-target="#keepModal"
                        @click="getKeepById(keepProp?.id)" class="img-fluid border glow image" :src="keepProp?.img" alt="">
                    <div class="d-flex justify-content-between p-2 align-items-center">
                        <p class="text text-light fs-2">{{ keepProp?.name }}</p>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <KeepModal />
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Vault } from '../models/Vault';
import { Keep } from '../models/Keep';
import { keepsService } from '../services/KeepsService';
import Pop from '../utils/Pop';
import { Modal } from 'bootstrap';
import KeepModal from './KeepModal.vue';
import { logger } from '../utils/Logger';
export default {
    props: { keepProp: { type: Keep, required: true } },
    setup() {
        return {
            async getKeepById(keepId) {
                try {
                    logger.log(keepId);
                    await keepsService.getKeepById(keepId);
                    Modal.getOrCreateInstance('#keepModal').hide();
                }
                catch (error) {
                    Pop.error;
                }
            }
        };
    },
    components: { KeepModal }
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

.border {
    border: 3px solid black;
    box-shadow: 0 5px 10px black;
}

.glow:hover {
    box-shadow: 0px 0px 50px black;
    transition: ease-in-out 0.3s;
}
</style>