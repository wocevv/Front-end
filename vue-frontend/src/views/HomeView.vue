<script>

    let myString = "";
    import linegraph from '../../src/components/Linegraph.vue'
    import bargraph from '../../src/components/Barchart.vue'
    import router from '@/router'
    export default {

            components: {
            linegraph,
            bargraph
            },
        data() {
            return {
                test: true,
            }
        },
            methods: {
            Gonext(id) {
                myString = id   
                router.push({ name: 'test', params: { myString } })             
            }
        },

        props: {
            selectedOption: {
                type: String,
                required: true
            },
            selectedChart: {
                type: String,
                required: true
            }
        },
        setup(props) {
            if (props.selectedOption === null ) {
                selectedOption = 'undefined'
            }
            if (props.selectedChart === null ) {
                selectedChart = 'undefined'
            }
        }
    }
</script>

<script setup>


    
</script>

<template>
    <div>
        <button @click="test = !test">+</button>

        <div v-if="!test">
            <img @click="Gonext('line-chart')" src="../../src/images/line-chart.png" width="240" height="128" />
            <img @click="Gonext('bar-chart')" src="../../src/images/barchart.jpg" width="240" height="128" />
        </div>
    </div>

    <div v-if="selectedChart === 'line-chart'">
        <div>{{selectedOption}}</div>
        <linegraph :my-string="selectedOption"></linegraph>
    </div>
    <div v-if="selectedChart === 'bar-chart'">
        <div>{{selectedOption}}</div>
        <bargraph :my-string="selectedOption"></bargraph>
    </div>
</template>
