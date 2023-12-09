<template>
    <div class="container-fluid">
        <div class="row" type="button" role="button" data-bs-toggle="modal" data-bs-target="#keepModal"
            @click="setActiveKeep(keepProp);">

            <div>
                <div>
                    <img class="img-fluid image" :src="keepProp.img" alt="">
                    <div class="d-flex justify-content-between p-2 align-items-center">
                        <p class="text text-light fs-2">{{ keepProp.name }}</p>
                        <img class="profile text rounded-circle" :src="keepProp.creator.picture" alt="">
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
export default {
    props: { keepProp: { type: Keep, required: true } },

    setup() {

        return {
            async setActiveKeep(keepData) {
                try {
                    logger.log(keepData)
                    AppState.activeKeep = {}
                    AppState.activeKeep = keepData
                    Modal.getOrCreateInstance('#keepModal').hide()
                } catch (error) {
                    Pop.error
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
    width: 10%;
    height: 45px;
}

.text {
    position: relative;
    bottom: 75px;

    text-shadow: 2px 2px 5px black;
}
</style>