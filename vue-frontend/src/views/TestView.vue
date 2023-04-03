<script setup lang="ts">
    import test1 from '../components/Homepage.vue'
</script>

<template>
    <main>
        <test1 :my-string="myString"></test1>

        <h1>You chose: {{myString}}</h1>
        <div class="dropdown-menu">
            <select name="category" id="category" v-model="selectedCategory">
                <option disabled selected hidden value="">Please choose a category</option>
                <option v-for="item in listItems">{{item}}</option>
            </select>

            <button @click="EnterValue(myString)">Enter</button>
        </div>
    </main>
</template>


<script lang="ts">
    import router from '@/router'
    import { reactive } from 'vue';

    let selectedCategory = "";
    let selectChart = "";
    export default {

        props: {
            myString: {
                type: String,
                required: true,
            }
        },
        data() {
            return {
                test: true,
                listItems: []
            }
        },

        methods: {
            async getData() {
                const res = await fetch("https://localhost:5001/api/widget/category");
                const finalRes = await res.json();
                this.listItems = finalRes;
            },
            EnterValue(value) {
                router.push({ name: 'home', params: { selectedOption: selectedCategory, selectedChart: value} })
            }
        },
        mounted() {
            this.getData()
        },

    }

</script>
