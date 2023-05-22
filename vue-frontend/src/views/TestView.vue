<script>

    let myString = "";
    import bargraphtest from '../../src/components/Bargraph.vue'
    import axios from 'axios';

    import router from '@/router'
    import { defineComponent, ref, watchEffect, computed } from 'vue'

</script>

<script setup>

</script>

<template>
<h1>test</h1>
</template>

<style scoped>
    .active {
        border: 2px solid red;
    }
    .popup {
        position: fixed;
        top: 25%;
        left: 0;
        width: 100%;
        height: 100%;
        overflow-x: hidden;
        overflow-y: auto;
        z-index: 1;
    }

    .popup-inner {
        max-width: 30%;
        margin: 2rem auto;
    }

    .popup-content {
        background-color: lightgrey;
        position: relative;
        background-clip: padding-box;
        border-radius: 0.3rem;
        border: 1px solid rgba(0,0,0,0.3);
        padding: 1rem;
    }
</style>