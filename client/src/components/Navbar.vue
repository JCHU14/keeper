<template>
  <nav class="navbar navbar-expand-sm boxshadow  px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex justify-content-center p-2  align-items-center">
        <img alt="logo"
          src="https://th.bing.com/th/id/R.3e5c857f6e64244e24bfa077f922d104?rik=YbZPUfrFP18BRg&pid=ImgRaw&r=0"
          height="45" />
      </div>
    </router-link>

    <div class="d-flex p-2 justify-content-between">

      <div class="align-items-center p-1">
        <button data-bs-toggle="modal" data-bs-target="#createModal" class="btn glow fs-5">Create Keep</button>
      </div>
      <div class="align-items-center p-1">
        <button data-bs-toggle="modal" data-bs-target="#vaultModal" class="btn glow fs-5">Create Vault</button>
      </div>

    </div>


    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>

        </li>
      </ul>
      <!-- LOGIN COMPONENT HERE -->

      <Login />
    </div>
  </nav>
  <CreateKeep />
  <CreateVault />
</template>

<script>
import { onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import CreateKeep from './CreateKeep.vue';
import CreateVault from './CreateVault.vue';
export default {
  setup() {

    const theme = ref(loadState('theme') || 'light')

    onMounted(() => {
      document.documentElement.setAttribute('data-bs-theme', theme.value)
    })

    return {
      theme,
      toggleTheme() {
        theme.value = theme.value == 'light' ? 'dark' : 'light'
        document.documentElement.setAttribute('data-bs-theme', theme.value)
        saveState('theme', theme.value)
      }
    }
  },
  components: { Login, CreateKeep, CreateVault }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.purple {
  background-color: rgba(219, 49, 219, 0.696);
}

.nav-link {
  text-transform: uppercase;
}

.boxshadow {
  border-bottom: 1px solid black;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.glow:hover {
  box-shadow: 0px 0px 20px darkmagenta;
  transition: ease-in-out 0.3s;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
